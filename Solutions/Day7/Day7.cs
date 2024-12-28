using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day7
{
    public static class Day7
    {
        public static async Task Solve()
        {
            var calibrator = new Calibrator();

            await calibrator.LoadData("./Day7/data.txt");
            calibrator.FindCalibrations();
            Console.WriteLine(calibrator.Sum);
        }
    }
}
