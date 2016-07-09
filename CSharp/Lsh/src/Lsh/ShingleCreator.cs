namespace Lsh
{
    using System;
    using System.Collections.Generic;

    public static class ShingleCreator
    {
        private const int ShingleLength = 10;

        public static HashSet<int> GetShingles(string document)
        {
            if (document == null)
            {
                throw new ArgumentException();
            }

            int documentLength = document.Length;
            HashSet<string> shingles = new HashSet<string>();
            HashSet<int> hashedShingles = new HashSet<int>();

            int beginIndex = 0;
            int endIndex = ShingleLength - 1;

            while (endIndex < documentLength)
            {
                string shingle = document.Substring(beginIndex, ShingleLength);
                shingles.Add(shingle);
                hashedShingles.Add(shingle.GetHashCode());
                beginIndex++;
                endIndex = beginIndex + ShingleLength - 1;
            }

            Console.WriteLine(string.Join("\n", shingles));
            return hashedShingles;
        }
    }
}
