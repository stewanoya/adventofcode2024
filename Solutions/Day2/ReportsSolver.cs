using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day2
{
    public class ReportsSolver
    {
        private List<List<byte>> _reports = [];

        public async Task LoadRawData(string path)
        {
            foreach (var line in await File.ReadAllLinesAsync(path))
            {
                _reports.Add(line.Split(' ').Select(i => Convert.ToByte(i)).ToList());
            }
        }

        private bool IsSafe(List<byte> report, bool dampen = false)
        {
            var isOrdered = Enumerable.SequenceEqual(report, report.OrderBy(i => i)) || Enumerable.SequenceEqual(report, report.OrderByDescending(i => i));
            var safe = true;
            for (var i = 0; i < report.Count; i++)
            {
                if (i == report.Count - 1)
                {
                    if (safe)
                    {
                        break;
                    }
                    else
                    {
                        if (dampen)
                        {
                            return false;
                        } else
                        {
                            var copy = report.ToList();
                            copy.RemoveAt(i);
                            return IsSafe(copy, true);
                        }
                    }
                }

                var curr = report[i];
                var next = report[i + 1];
                var difference = Math.Abs(curr - next);
                if (!isOrdered || difference < 1 || difference > 3)
                {
                    if (dampen)
                    {
                        safe = false;
                    } else
                    {

                        var copy = report.ToList();
                        copy.RemoveAt(i);
                        safe = IsSafe(copy, true);

                        if (safe)
                        {
                            return safe;
                        }
                    }
                }
            }
            return safe;
        }

        
        private List<byte> GetCopy(List<byte> report, int index)
        {
            var copy = report.ToList();
            copy.RemoveAt(index);
            return copy;
        }

        public int GetNumberOfSafeReport()
        {
            return _reports.Where(i => IsSafe(i)).Count();
        }
    }
}
