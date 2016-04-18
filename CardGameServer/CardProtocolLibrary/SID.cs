namespace CardGameServer
{
    public struct SID
    {
        private  ulong _id;
        public ulong ID => _id;
        private static ulong _lastID;

        public static SID New()
        {
            return new SID { _id = _lastID++ };
        }
        
        public SID(ulong id) {
            _id = id;
        }

        public static implicit operator ulong (SID i)
        {
            return i._id;
        }
    }
}
