namespace DP
{
    using System.Collections.Generic;

    public static class LongestCommonSubstring
    {
        public static IEnumerable<char> FindLcs(char[] string1, char[] string2)
        {
            if (string1 == null || string2 == null)
            {
                yield break;
            }

            // Could use a dictionary as an improvement
            var table = new int[string1.Length, string2.Length];
            int maxLengthSoFar = 0;
            int beginIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < string1.Length; i++)
            {
                for (int j = 0; j < string2.Length; j++)
                {
                    if (string1[i] == string2[j])
                    {
                        if (i > 0 && j > 0)
                        {
                            // Grab from diagonal (if exists) and increment
                            table[i, j] = table[i - 1, j - 1] + 1;
                        }
                        else
                        {
                            table[i, j] = 1;
                        }
                    }

                    // Keep track of longest so far and capture indexes
                    if (table[i, j] > maxLengthSoFar)
                    {
                        maxLengthSoFar = table[i, j];
                        beginIndex = i - maxLengthSoFar + 1;
                        endIndex = i;
                    }
                }
            }

            if (maxLengthSoFar > 0)
            {
                for (int i = beginIndex; i < endIndex + 1; i++)
                {
                    yield return string1[i];
                }
            }
        }
    }
}
