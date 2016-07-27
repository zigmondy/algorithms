namespace Lsh
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Input Candidates
            List<Candidate> candidates = new List<Candidate>
            {
                new Candidate
                {
                    InputText = "Brown fox jumped over the lazy dog."
                },
                new Candidate
                {
                    InputText = "Brown fox jumped over a tap."
                },
                new Candidate
                {
                    InputText = "Brown fox over the jumped man."
                },
                new Candidate
                {
                    InputText = "Brown fox jumped over the lazy sheep."
                },
                new Candidate
                {
                    InputText = "Brown fox jumped late."
                },
                new Candidate
                {
                    InputText = "Brown fox jumped over the lazy donkey."
                },
            };

            // Prepare universal set of shingles
            HashSet<string> universalShingles = new HashSet<string>();
            HashSet<int> universalHashedShingles = new HashSet<int>();
            foreach (var candidate in candidates)
            {
                ShingleCreator.CreateShingles(candidate);
                universalShingles.UnionWith(candidate.Shingles);
                universalHashedShingles.UnionWith(candidate.HashedShingles);
            }

            // Generate string and bool vectors from universal set
            List<string> universalShinglesList = universalShingles.ToList();
            List<int> universalHashedShinglesList = universalHashedShingles.ToList();
            foreach (var candidate in candidates)
            {
                candidate.StringVectorFromUniversalSet = new List<string>();
                // Vector length should be equal to length of universal set
                foreach (var universalShingle in universalShinglesList)
                {
                    candidate.StringVectorFromUniversalSet.Add(candidate.Shingles.Contains(universalShingle) ? universalShingle : "--------------------");
                }

                candidate.BoolVectorFromUniversalSet = new List<bool>();
                // Vector length should be equal to length of universal set
                foreach (var universalHashedShingle in universalHashedShinglesList)
                {
                    candidate.BoolVectorFromUniversalSet.Add(candidate.HashedShingles.Contains(universalHashedShingle));
                }
            }

            int rows = universalHashedShingles.Count;
            int columns = candidates.Count;

            bool[,] boolMatrix = new bool[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Candidate candidate = candidates[column];
                    boolMatrix[row, column] = candidate.BoolVectorFromUniversalSet[row];
                    Console.Write(string.Format("{0} ", boolMatrix[row, column]));
                }

                Console.Write(Environment.NewLine);
            }

            string[,] stringMatrix = new string[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Candidate candidate = candidates[column];
                    stringMatrix[row, column] = candidate.StringVectorFromUniversalSet[row];
                    Console.Write(string.Format("{0} ", stringMatrix[row, column]));
                }

                Console.Write(Environment.NewLine);
            }

            // With n hash functions, compute the column signatures
            int numberOfHashFunctions = HashCodeGenerator.RandomNumbers.Length;
            int[,] signatureMatrix = new int[numberOfHashFunctions, columns];
            for (int row = 0; row < numberOfHashFunctions; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    signatureMatrix[row, column] = Int32.MaxValue;
                }
            }

            for (int row = 0; row < rows; row++)
            {
                var hashes = HashCodeGenerator.GetHashes(row.ToString());
                for (int column = 0; column < columns; column++)
                {
                    if (boolMatrix[row, column])
                    {
                        for (int signatureRow = 0; signatureRow < numberOfHashFunctions; signatureRow++)
                        {
                            signatureMatrix[signatureRow, column] = Math.Min(signatureMatrix[signatureRow, column], hashes[signatureRow]);
                        }
                    }
                }
            }

            for (int row = 0; row < numberOfHashFunctions; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Console.Write(string.Format("{0} ", signatureMatrix[row, column]));
                }

                Console.Write(Environment.NewLine);
            }

            // Now do the naive LSH
            int rowsInABand = 20;
            int numberOfBands = numberOfHashFunctions / rowsInABand;
            List<SimilarValue> reverseMapping = new List<SimilarValue>();

            int currentRow = 0;
            for (int band = 0; band < numberOfBands; band++)
            {
                int upperRowLimit = currentRow + rowsInABand;
                for (; currentRow < upperRowLimit; currentRow++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        int currentValue = signatureMatrix[currentRow, column];
                        reverseMapping.Add(new SimilarValue
                        {
                            HashCode = currentValue,
                            BandNumber = band,
                            ColumnIndex = column,
                        });
                    }
                }
            }

            var mostSimilarColumns = reverseMapping
                        .GroupBy(_ => _.HashCode)
                        .ToDictionary(_ => _.Key, _ => _.ToList())
                        .OrderByDescending(_ => _.Value.Count);

            Console.WriteLine(
                string.Join(
                    ",",
                    mostSimilarColumns.FirstOrDefault()
                        .Value
                        .Select(_ => _.ColumnIndex)
                        .Distinct()));
        }

        private class SimilarValue
        {
            public int HashCode { get; set; }
            public int BandNumber { get; set; }
            public int ColumnIndex { get; set; }
        }
    }
}
