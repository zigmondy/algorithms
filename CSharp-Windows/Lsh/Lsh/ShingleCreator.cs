namespace Lsh
{
    using System;
    using System.Collections.Generic;

    public static class ShingleCreator
    {
        private const int ShingleLength = 8;

        public static void CreateShingles(Candidate candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentException("candidate");
            }

            if (candidate.InputText == null)
            {
                throw new ArgumentException("candidate.InputText");
            }

            int documentLength = candidate.InputText.Length;
            candidate.Shingles = new HashSet<string>();
            candidate.HashedShingles = new HashSet<int>();

            int beginIndex = 0;
            int endIndex = ShingleLength - 1;
            while (endIndex < documentLength)
            {
                string shingle = candidate.InputText.Substring(beginIndex, ShingleLength);
                candidate.Shingles.Add(shingle);
                candidate.HashedShingles.Add(shingle.GetHashCode());
                beginIndex++;
                endIndex = beginIndex + ShingleLength - 1;
            }
        }
    }
}
