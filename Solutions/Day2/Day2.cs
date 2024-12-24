using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day2
{
    public static class Day2
    {
        public static async Task Solve()
        {
            var solver = new ReportsSolver();
            await solver.LoadRawData("./day2/data.txt");
            var count = solver.GetNumberOfSafeReport();

            Console.WriteLine(count);
        }
    }
}
