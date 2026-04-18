using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THADotNetTrainingBatch1_LINQ_Data;

public class LINQ2
{
    public static List<string> GetNames(int count = 10)
    {
        string letters = "abcdefghijklmnopqrstuvwxyz";
        string[] suffixes = { "an", "alex",
                            "obby", "ice",
                            "ara", "ith",
                            "ony", "elia",
                            "en", "is" };

        Random random = new Random();

        return Enumerable.Range(0, count)
                        .Select(x =>
                        {
                            char firstLetter = letters[random.Next(letters.Length)];
                            string suffix = suffixes[random.Next(suffixes.Length)];

                            return (firstLetter + suffix).ToLower();
                        }).ToList();
    }
}
