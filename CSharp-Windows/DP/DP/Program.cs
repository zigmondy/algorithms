namespace DP
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            PrintAndAssert("", "", "");
            PrintAndAssert(null, null, "");
            PrintAndAssert("a", "abc", "a");
            PrintAndAssert("ab", "abc", "ab");
            PrintAndAssert("abc", "abcd", "abc");
            PrintAndAssert("abcd", "bc", "bc");
            PrintAndAssert("abcd", "quick brown fox", "b");
            PrintAndAssert("lazy", "brown fox", "");
            PrintAndAssert("quick brown fox jumped", "brown fox", "brown fox");
        }

        private static void PrintAndAssert(string string1, string string2, string expectation)
        {
            string lcs = new string(LongestCommonSubstring.FindLcs(string1?.ToCharArray(), string2?.ToCharArray()).ToArray());
            Console.WriteLine("{0}, {1} - {2}", string1, string2, expectation);
            if (!string.Equals(expectation, lcs))
            {
                throw new Exception();
            }
        }
    }
}
