namespace Lsh
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var hashedShingles = ShingleCreator.GetShingles("The quick brown fox jumped over the lazy dog.");
            Console.WriteLine(string.Join("\n", hashedShingles));
        }
    }
}
