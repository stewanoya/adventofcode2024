using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day3
{
    public static class Day3
    {
        public async static Task Solve()
        {
            var decoder = new Decoder();

            await decoder.LoadRawData("./Day3/data.txt");
            decoder.FixMemory();
            var total = decoder.RunCalculation();
            Console.WriteLine(total);
        }
    }
}
