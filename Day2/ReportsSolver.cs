using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public class ReportsSolver
    {
        public async Task LoadRawData()
        {
            foreach (var line in await File.ReadAllLinesAsync("./data.txt"))
            {
                _reports.Add(line.Split(' ').Select(i => Convert.ToByte(i)).ToList());
            }
        }

        private bool IsSafe(List<byte> report)
        {

            var isOrdered = Enumerable.SequenceEqual(report.OrderBy(i => i), report) || Enumerable.SequenceEqual(report.OrderByDescending(i => i), report);

            if (!isOrdered)
            {
                return false;
            }

            for (var i = 0; i < report.Count; i++)
            {
                if (i == report.Count)
                {
                    break;
                }

                var curr = report[i];
                var next = report[i + 1];
                var difference = (curr - next) < 0 ? (curr - next) * -1 : (curr - next);
                if (difference < 1 && difference > 3)
                {
                    return false;
                }
            }

            return true;
        }

        public int GetNumberOfSafeReport()
        {
            return _reports.Where(i => IsSafe(i)).Count();
        }

        private List<List<byte>> _reports = [];
    }
}
