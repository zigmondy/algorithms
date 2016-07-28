using System.Collections.Generic;

namespace Lsh
{
    public class Candidate
    {
        public string InputText { get; set; }

        public HashSet<string> Shingles { get; set; }

        public HashSet<int> HashedShingles { get; set; }

        public List<bool> BoolVectorFromUniversalSet { get; set; }

        public List<string> StringVectorFromUniversalSet { get; set; }
    }
}
