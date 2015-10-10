using System;
using System.Collections.Generic;

namespace CardGameServer
{
    /// <summary>
    /// Basic Creature Class
    /// !! Use a base type and create instances of it !!
    /// </summary>
    public class Creature
    {
        // Base Attributes
        public SID UID;
        public string ID;
        public string Name;
        public string Description;
        public int BaseHealth;
        public int DamageTokens;
        public int Damage;
        public int Armor;
        public int Team;
        public Player Owner;
        public string Image;

        // Modifiers 
        public float DamageModifier;
        public int DamageBonus;
        public float HealthModifier;
        public int HealthBonus;
        public float ArmorModifier;
        public int ArmorBonus;

        // Board Properties
        public bool Commander = false;
        public bool SleepSickness = true;
        public bool CanAttack = true;
        public bool Taunt = false;
        public bool MagicImmune = false;
        public bool PhysicalImmune = false;
        public bool MagicTargetable = true;
        public bool PhysicalTargetable = true;
        public bool Stealth = false;
        public bool Dead = false;
        public DamageType AttackDamageType = DamageType.Physical;

        public List<EffectData> EffectData = new List<EffectData>(); 
        public List<IEffect<Creature>> Effects = new List<IEffect<Creature>>();

        public int CalculatedTotalHealth => (int)Math.Floor((BaseHealth + HealthBonus) * HealthModifier);

        public int CalculatedCurrentHealth => CalculatedTotalHealth - DamageTokens;

        public int CalculatedDamage => (int) Math.Floor((Damage + DamageBonus) * DamageModifier);

        public int CalculatedArmor => (int) Math.Floor((Armor + ArmorBonus)*ArmorModifier);

        #region Events
        public event Action<Creature> Attacked;

        protected virtual void OnAttacked(Creature obj)
        {
            var handler = Attacked;
            handler?.Invoke(obj);
        }

        public event Action<Creature> Attack;

        protected virtual void OnAttack(Creature obj)
        {
            var handler = Attack;
            handler?.Invoke(obj);
        }

        public event Action<Creature> Killed;

        protected virtual void OnKilled(Creature obj)
        {
            var handler = Killed;
            handler?.Invoke(obj);
        }

        public event Action<bool, Creature> Targetted;

        protected virtual void OnTargetted(bool arg1, Creature arg2)
        {
            var handler = Targetted;
            handler?.Invoke(arg1, arg2);
        }

        public event Action<int, DamageType, Creature> DamageTaken;

        protected virtual void OnDamageTaken(int arg1, DamageType arg2, Creature arg3)
        {
            var handler = DamageTaken;
            handler?.Invoke(arg1, arg2, arg3);
        }

        public event Action<int, DamageType, Creature> DamageDealt;

        protected virtual void OnDamageDealt(int arg1, DamageType arg2, Creature arg3)
        {
            var handler = DamageDealt;
            handler?.Invoke(arg1, arg2, arg3);
        }
        #endregion

        public void TakeDamage(int damage, DamageType type, Creature source)
        {
            if (MagicImmune && type == DamageType.Magic)
            {
                OnDamageTaken(0, type, source);
                return;
            }
            if (PhysicalImmune && type == DamageType.Physical)
            {
                OnDamageTaken(0, type, source);
                return;
            }

            var calcDamage = damage - CalculatedArmor;
            DamageTokens += calcDamage < 0 ? 0 : calcDamage;
            OnDamageTaken(calcDamage, type, source);

            if (CalculatedCurrentHealth <= 0)
            {
                Kill(source);
            }
        }

        public void AttackTarget(Creature target)
        {
            OnAttack(target);
            Stealth = false;
            target.OnAttacked(this);
            target.TakeDamage(CalculatedDamage, AttackDamageType, this);
        }

        public void Start()
        {
            ResetModifiers();
            // Loop backwards for consistency with update which
            // needs backwards loop
            for (var i = Effects.Count-1; i >= 0; i--)
            {
                Effects[i].Start(this);
            }
        }

        public void Kill(Creature source)
        {
            Dead = true;
            // Call this after setting Dead to true
            // incase the callback wants to keep the creature alive!
            OnKilled(this);
        }

        public void Update()
        {
            // Calculate modifiers each turn on update
            // Pretty easy to do passive effects this way
            // Shouldn't be too intensive considering max creature count
            // Won't be that many effects per creature
            ResetModifiers();

            for (var i = Effects.Count-1; i >= 0; i--)
            {
                if (Effects[i].Remove)
                {
                    Effects[i].End();
                    Effects.RemoveAt(i);
                    continue;
                }
                Effects[i].Update();
                if (Effects[i].Persistent) continue;
                Effects[i].TimeLeft--;
                if (Effects[i].TimeLeft == 0)
                {
                    Effects[i].Remove = true;
                }
            }
        }

        private void ResetModifiers()
        {
            DamageModifier = 1;
            HealthModifier = 1;
            ArmorModifier  = 1;

            DamageBonus = 0;
            HealthBonus = 0;
            ArmorBonus  = 0;
        }

        // Use a template model ?
        //  -> Load creature data externally
        //  -> Create an instance of this creature class for each creature in database
        //  -> Set appropriate attributes and add effects (based on data)
        //  -> Create deep clone using this function when we want to make a usable copy
        public Creature CreateInstance(Player owner, bool commander, int team)
        {
            var newCreature = new Creature
            {
                ID = ID,
                Description = Description,
                Name = Name,
                BaseHealth = BaseHealth,
                Damage = Damage,
                Team = team,
                Commander = commander,
                SleepSickness = SleepSickness,
                CanAttack = CanAttack,
                Taunt = Taunt,
                MagicImmune = MagicImmune,
                PhysicalImmune = PhysicalImmune,
                Stealth = Stealth,
                AttackDamageType = AttackDamageType,
                Owner = owner,
                Image = Image,
                UID = SID.New()
            };

            foreach (var effect in EffectData)
            {
                // Need to create a new instance of effect for every creature!
                newCreature.Effects.Add(Game.CreatureEffects.CreateInstance(effect.Name, effect.Attributes));
            }

            return newCreature;
        }
    }
}