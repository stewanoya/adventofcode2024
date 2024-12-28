using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day6
{
    public static class Day6
    {
        public async static Task Solve()
        {
            var tracker = new GuardTracker();

            await tracker.LoadData("./Day6/data.txt");
            tracker.FindGuard();
            // run once to mark original path
            tracker.TrackGuard();
            tracker.TrackGuardLoop();
            //var stepCount = tracker.GetStepCount();

            //Console.WriteLine(stepCount + 1);
        }
    }
}
