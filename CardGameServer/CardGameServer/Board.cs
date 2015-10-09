using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CardProtocolLibrary;

namespace CardGameServer
{
    /// <summary>
    /// Handles the input of players and game mechanics.
    /// 
    /// Holds the data on the players, what turn it is, whose turn it is,
    /// if the game has ended, who has won.
    /// </summary>
    public class Board
    {
        public List<Player> Players;
        public int TurnCounter = 0;
        public int ActivePlayerID = 0;
        public Player ActivePlayer => Players[ActivePlayerID];
        public event Action<Board, Player> TurnEnd;
        public Boolean GameOver = false;
        public Player Winner = null;
        public Timer TurnTimer;

        /// <summary>
        /// Handles a GameAction command sent by a player.
        /// </summary>
        /// <param name="player">The player that sent the command</param>
        /// <param name="action">What type of action it is</param>
        /// <param name="data">The data, such as who to target or the player's name</param>
        public void RecieveCommand(Player player, GameDataAction dataAction)
        {
            // TODO: Finish the command recieved events
            var data = dataAction.Data;
            var action = dataAction.Action;
            if (player == ActivePlayer)
            {
                switch (action)
                {
                    case GameAction.TurnEnd:
                        EndTurn(null);
                        break;
                    case GameAction.PlayCard:
                        Card card;
                        // Attempt to get the card matching the UID
                        if (player.Cards.TryGetValue(new Guid(data["uid"]), out card))
                        {
                            card.Play(this);
                        }
                        break;
                    case GameAction.GameStart:
                        break;
                    case GameAction.TurnStart:
                        break;
                    case GameAction.DrawCard:
                        break;
                    case GameAction.SetMana:
                        break;
                    case GameAction.SetHealth:
                        break;
                    case GameAction.Attack:
                        break;
                    case GameAction.Trigger:
                        break;
                    case GameAction.Kill:
                        break;
                    case GameAction.MinionSummoned:
                        break;
                    case GameAction.SpellCast:
                        break;
                    case GameAction.GameOver:
                        break;
                    case GameAction.Meta:
                        break;
                    case GameAction.Ping:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, null);
                }
            }
        }

        /// <summary>
        /// Sends a command to all of the players.
        /// </summary>
        /// <param name="action">Type of action to be sent</param>
        /// <param name="data">Data for action</param>
        public void SendToAllPlayers(GameAction action, Dictionary<string, GameData> data)
        {
            foreach (var player in Players)
            {
                player.DataWriter.SendAction(action, data);
            }
        }

        /// <summary>
        /// Sends a command to the players whose turn it ISN'T
        /// </summary>
        /// <param name="action">Type of action to be sent</param>
        /// <param name="data">Data for action</param>
        public void SendToInactivePlayers(GameAction action, Dictionary<string, GameData> data)
        {
            foreach (var player in Players)
            {
                if (player != ActivePlayer)
                    player.DataWriter.SendAction(action, data);
            }
        }

        /// <summary>
        /// Create a new instance of Board class, with the given player list.
        /// </summary>
        /// <param name="players">The list of players who will be playing the game</param>
        public Board(List<Player> players)
        {
            Players = players;

            // Assign each player an ID for this game and send it to them.
            for (var i = 0; i < players.Count; i++)
            {
                players[i].ID = i;
                players[i].DataWriter.SendAction(GameAction.GameStart, new Dictionary<string, GameData>
                {
                    {"players", players.Count}, {"number", i},
                });
            }

            // Start the game!
            DoTurn();
            StartTurnTimer();
        }

        public void StartTurnTimer()
        {
            // Currently this timer will run every two minutes.
            TurnTimer = new Timer(EndTurn, null, new TimeSpan(0, 2, 0), new TimeSpan(0, 2, 0));
        }

        protected virtual void OnTurnEnd(Board arg1, Player arg2)
        {
            var handler = TurnEnd;
            handler?.Invoke(arg1, arg2);
        }

        public event Action<Board, Player> TurnStart;

        protected virtual void OnTurnStart(Board arg1, Player arg2)
        {
            var handler = TurnStart;
            handler?.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Called when the turn ends.
        /// </summary>
        /// <param name="state">Needed for the timer callback</param>
        public void EndTurn(object state)
        {
            // Make sure events trigger first.
            OnTurnEnd(this, ActivePlayer);
            // Send the turn end message to each player.
            SendToAllPlayers(GameAction.TurnEnd, new Dictionary<string, GameData> {{"player", ActivePlayerID}});

            // Start the next turn!
            TurnCounter++;
            DoTurn();
            StartTurnTimer();
        }

        /// <summary>
        /// Handles all the logic for running an actual turn.
        /// </summary>
        public void DoTurn()
        {
            // Determine whose turn it is
            ActivePlayerID = TurnCounter%Players.Count;
            // Cache active player
            var activePlayer = ActivePlayer;

            // Make sure event is fired as it's the start of the turn
            OnTurnStart(this, activePlayer);

            SendToAllPlayers(GameAction.TurnStart, new Dictionary<string, GameData> {{"player", ActivePlayerID}});

            // TODO: Refactor this into separate "DrawCard" method.

            // Draw a card!
            var drawnCard = activePlayer.Deck.Pop();
            activePlayer.Cards.Add(drawnCard.UID, drawnCard);

            // Show the player what card they have drawn
            activePlayer.DataWriter.SendAction(GameAction.DrawCard, new Dictionary<string, GameData>()
            {
                {"player", ActivePlayerID}, {"card", drawnCard.ID}, {"uid", drawnCard.UID.ToString()}
            });

            // Tell the other players that a card has been drawn
            SendToInactivePlayers(GameAction.DrawCard, new Dictionary<string, GameData>
            {
                {"player", ActivePlayerID}, {"card", "-1"}
            });
        }

        // TODO: Finish the actual game logic!
        public void Summon(Player owner, Creature creature)
        {
            Console.WriteLine("SUMMONING: " + creature.Name);
        }

        /// <summary>
        /// Builds up a list of possible targets for the given spell and player.
        /// </summary>
        /// <param name="owner">Owner of the spell</param>
        /// <param name="spell">Spell in question</param>
        /// <returns></returns>
        public List<Creature> GetPossibleTargets(Player owner, Spell spell)
        {
            var creatures = new List<Creature>();
            if (spell.TargetGroup.IsFlagSet(TargetGroup.None))
            {
                return creatures;
            }
            if (spell.TargetGroup.IsFlagSet(TargetGroup.Champions))
            {
                if (spell.TargetGroup.IsFlagSet(TargetGroup.Allies))
                {
                    creatures.Add(owner.Commander);
                }
                if (spell.TargetGroup.IsFlagSet(TargetGroup.Enemies))
                {
                    creatures.AddRange(from p in Players where p != owner select p.Commander);
                }
            }

            if (!spell.TargetGroup.IsFlagSet(TargetGroup.Minions)) return creatures;

            if (spell.TargetGroup.IsFlagSet(TargetGroup.Allies))
            {
                creatures.AddRange(owner.Creatures.Where(c => !c.Commander));
            }
            if (spell.TargetGroup.IsFlagSet(TargetGroup.Enemies))
            {
                creatures.AddRange(from p in Players from c in p.Creatures where !c.Commander select c);
            }
            return creatures;
        } 

        public void Cast(Player owner, Spell spell, List<Creature> targets)
        {
            Console.WriteLine("CASTING: " + spell.ID);
            spell.Cast(targets);
        }
    }
}
