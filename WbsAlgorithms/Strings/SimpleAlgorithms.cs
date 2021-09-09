namespace WbsAlgorithms.Strings
{
    public class SimpleAlgorithms
    {
        /// <summary>
        /// Checks whether two strings are circular rotations of one another.
        /// 
        /// A string s1 is a circular rotation of a string s2 if it matches
        /// when the characters are circularly shifted by any number of positions.
        /// 
        /// Example: ACTGACG is a circular shift of TGACGAC and vice versa.
        /// 
        /// [Sedgewick] 1.2.6 p.114 - Write a program that checks whether two 
        /// strings are circular shifts of one another.
        /// </summary>
        /// <param name="s1">The first string</param>
        /// <param name="s2">The second string</param>
        /// <returns>True if the two strings are circular rotations of one another. False, otherwise.</returns>
        public static bool AreStringsCircularRotations(string s1, string s2) =>
            s1.Length == s2.Length && (s1 + s1).IndexOf(s2) != -1;
    }
}
