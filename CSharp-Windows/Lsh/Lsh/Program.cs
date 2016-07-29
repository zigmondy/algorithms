namespace Lsh
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Program
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
                    InputText = "Brown fox jumped over the lazy cow."
                },
                new Candidate
                {
                    InputText = "Brown fox jumped over the lazy pig."
                },
                new Candidate
                {
                    InputText = "Red fox jumped the lazy cat."
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
                    Console.Write("{0} ", boolMatrix[row, column]);
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
                    Console.Write("{0} ", stringMatrix[row, column]);
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

            Console.WriteLine(string.Format(
                "Jaccard Similarity = {0}, {1}, {2}",
                GetJaccardSimilarity(boolMatrix.GetColumns(0), boolMatrix.GetColumns(1)),
                GetJaccardSimilarity(boolMatrix.GetColumns(0), boolMatrix.GetColumns(2)),
                GetJaccardSimilarity(boolMatrix.GetColumns(0), boolMatrix.GetColumns(3))));

            Console.WriteLine(string.Format(
                "Signature Similarity = {0}, {1}, {2}",
                GetSignatureSimilarity(signatureMatrix.GetColumns(0), signatureMatrix.GetColumns(1)),
                GetSignatureSimilarity(signatureMatrix.GetColumns(0), signatureMatrix.GetColumns(2)),
                GetSignatureSimilarity(signatureMatrix.GetColumns(0), signatureMatrix.GetColumns(3))));

            // Now do the naive LSH
            int rowsInABand = 4;
            int numberOfBands = numberOfHashFunctions / rowsInABand;
            List<SimilarValue> similarValues = new List<SimilarValue>();

            int currentRow = 0;
            for (int band = 0; band < numberOfBands; band++)
            {
                int upperRowLimitForBand = currentRow + rowsInABand;
                for (; currentRow < upperRowLimitForBand; currentRow++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        int currentValue = signatureMatrix[currentRow, column];
                        similarValues.Add(new SimilarValue
                        {
                            HashCode = currentValue,
                            BandIndex = band,
                            ColumnIndex = column,
                        });
                    }
                }
            }

            Dictionary<string, List<SimilarValue>> bandValues = new Dictionary<string, List<SimilarValue>>();
            foreach (IEnumerable<SimilarValue> currentBandValues in similarValues.GroupBy(_ => _.BandIndex))
            {
                foreach (IEnumerable<SimilarValue> columnsInCurrentBand in currentBandValues.GroupBy(_ => _.ColumnIndex))
                {
                    string columnSignature = string.Join(",", columnsInCurrentBand.Select(_ => _.HashCode).OrderByDescending(_ => _));
                    List<SimilarValue> similarColumns;
                    bandValues.TryGetValue(columnSignature, out similarColumns);
                    if (similarColumns == null)
                    {
                        similarColumns = new List<SimilarValue>();
                    }

                    similarColumns.AddRange(columnsInCurrentBand);
                    bandValues[columnSignature] = similarColumns;
                }
            }

            HashSet<string> finalSet = new HashSet<string>();
            foreach (KeyValuePair<string, List<SimilarValue>> bandValue in bandValues)
            {
                List<int> uniqueColumns = bandValue.Value.Select(_ => _.ColumnIndex).Distinct().OrderBy(_ => _).ToList();
                if (uniqueColumns.Count > 1)
                {
                    finalSet.Add(string.Join(", ", uniqueColumns));
                }
            }

            Console.WriteLine("Similar Columns:");
            foreach (string finalItem in finalSet)
            {
                Console.WriteLine(finalItem);
            }
        }

        private static IList<T> GetColumns<T>(this T[,] array, int index)
        {
            int rows = array.GetLength(0);
            List<T> list = new List<T>();
            for (int i = 0; i < rows; i++)
            {
                list.Add(array[i, index]);
            }

            return list;
        }

        private static double GetSignatureSimilarity(IList<int> setA, IList<int> setB)
        {
            int matches = 0;
            int total = setA.Count;

            for (int row = 0; row < setA.Count; row++)
            {
                if (setA[row] == setB[row])
                {
                    matches++;
                }
            }

            return Math.Round((double)matches * 100 / total, 2);
        }

        private static double GetJaccardSimilarity(IList<bool> setA, IList<bool> setB)
        {
            int intersection = 0;
            int union = 0;

            for (int row = 0; row < setA.Count; row++)
            {
                if (setA[row] == setB[row] && setA[row])
                {
                    intersection++;
                }

                if (setA[row] || setB[row])
                {
                    union++;
                }
            }


            return Math.Round((double)intersection * 100 / union, 2);
        }

        private class SimilarValue
        {
            public int HashCode { get; set; }
            public int BandIndex { get; set; }
            public int ColumnIndex { get; set; }
        }
    }
}
