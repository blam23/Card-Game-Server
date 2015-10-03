namespace CardGameServer
{
    /// <summary>
    /// Holds useful extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Determines if the Target Group has a specific target set.
        /// </summary>
        /// <param name="value">Input TargetGroup value</param>
        /// <param name="flag">TargetGroup to check for</param>
        /// <returns>If the specified flag is set in the input enum</returns>
        public static bool IsFlagSet(this TargetGroup value, TargetGroup flag)
        {
            return (value & flag) == flag;
        }
    }
}
