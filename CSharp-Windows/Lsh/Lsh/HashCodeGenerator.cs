﻿namespace Lsh
{
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public static class HashCodeGenerator
    {
        public static int[] RandomNumbers { get; } = new[]
        {
            094, 516, 805, 782, 433, 677, 604, 889, 620, 119,
            956, 425, 429, 467, 849, 955, 610, 816, 771, 88,
            990, 363, 828, 118, 744, 335, 533, 065, 982, 836,
            538, 301, 215, 015, 795, 565, 597, 361, 354, 798,
            123, 101, 703, 292, 545, 438, 257, 752, 406, 382,
            604, 440, 226, 717, 579, 462, 664, 299, 039, 863,
            798, 972, 933, 161, 592, 629, 579, 256, 186, 145,
            124, 190, 480, 090, 998, 920, 964, 788, 710, 928,
            089, 190, 009, 899, 595, 765, 324, 660, 624, 310,
            746, 417, 118, 789, 127, 525, 243, 130, 829, 456,
            818, 779, 618, 012, 533, 628, 442, 363, 198, 471,
            831, 382, 702, 093, 759, 787, 350, 152, 212, 817,
            641, 543, 357, 536, 575, 833, 072, 556, 797, 053,
            324, 011, 206, 108, 512, 074, 339, 061, 672, 896,
            929, 948, 215, 932, 268, 794, 699, 469, 665, 415,
            395, 454, 673, 952, 864, 120, 657, 637, 262, 596,
            423, 510, 384, 525, 093, 030, 517, 264, 330, 777,
            326, 551, 499, 211, 640, 233, 520, 859, 903, 270,
            306, 150, 150, 149, 628, 518, 433, 508, 486, 040,
            106, 779, 103, 572, 992, 481, 481, 349, 768, 545,
        };

        public static List<int> GetHashes(string input)
        {
            List<int> hashes = new List<int>();
            int currentHash = input.GetHashCode();
            foreach (int randomNumber in RandomNumbers)
            {
                hashes.Add(currentHash ^ randomNumber);
            }

            return hashes;
        }
    }
}
