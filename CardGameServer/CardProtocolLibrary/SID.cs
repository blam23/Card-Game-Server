namespace CardGameServer
{
    public class SID
    {
        public ulong ID;
        private static ulong _lastID;

        public static SID New()
        {
            return new SID { ID = _lastID++ };
        }

        public static implicit operator ulong (SID i)
        {
            return i.ID;
        }
    }
}
