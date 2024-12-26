using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day5
{
    public static class Day5
    {
        public static async Task Solve()
        {
            var checker = new ManualChecker();
            await checker.LoadData("./Day5/data.txt");
            checker.CheckManuals();
            checker.FixBrokenManuals();
            var sum = checker.GetMiddleSum();

            Console.WriteLine(sum);
        }
    }
}
