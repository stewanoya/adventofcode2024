using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day4
{
    public static class Day4
    {
        public static async Task Solve()
        {
            var wordFinder = new WordFinder("XMAS");
            await wordFinder.LoadData("./Day4/data.txt");
            var count = wordFinder.GetCrossMasWordCount();
            Console.WriteLine(count);
        }
    }
}
