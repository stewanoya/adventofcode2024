using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day7
{
    public class Calibrator
    {
        private List<(long, List<string>)> _calibrations = [];
        private List<(long, long, string)> _combinationsTried = [];
        private List<long> _calibratedVals = [];
        public long Sum => _calibratedVals.Sum();
        public async Task LoadData(string path)
        {
            var data = await File.ReadAllLinesAsync(path);

            foreach (var line in data)
            {
                var split = line.Split(':');
                var calibration = long.Parse(split[0]);
                var nums = split[1].Trim().Split(" ").ToList();
                _calibrations.Add((calibration, nums));
            }
        }

        public void FindCalibrations()
        {
            string[] operators = { "+", "*", "||" };
            foreach (var entry in _calibrations)
            {
                var calibration = entry.Item1;
                var nums = entry.Item2;
                var operatorCount = nums.Count - 1;

                var totalNumberOfCombinations = Math.Pow(3, operatorCount);

                for (int i = 0; i < totalNumberOfCombinations; i++)
                {
                    string expression = nums[0];
                    int temp = i;

                    for (int j = 0; j < operatorCount; j++)
                    {
                        int operatorIndex = temp % 3; // Get operator for this position (0, 1, or 2)
                        temp /= 3;
                        expression += $" {operators[operatorIndex]} {nums[j + 1]}";
                    }
                    var total = GetEquationTotal(expression);

                    if (total == calibration)
                    {
                        //_combinationsTried.Add((calibration, total, expression));
                        _calibratedVals.Add(calibration);
                        break;
                    }
                    //_combinationsTried.Add((calibration, total, expression));
                }
            }
        }

        private long GetEquationTotal(string equationString)
        {
            long total = 0;
            var nextOperation = "";
            var equation = equationString.Split(' ').ToList();
            foreach (var item in equation)
            {
                if (total == 0)
                {
                    var firstNum = long.Parse(item);
                    total = firstNum;
                    continue;
                }

                if (item == "+" || item == "*" || item == "||")
                {
                    nextOperation = item;
                    continue;
                }

                var num = long.Parse(item);
                if (nextOperation == "*")
                {
                    total *= num;
                }
                else if (nextOperation == "+")
                {
                    total += num;
                } else
                {
                    total = long.Parse($"{total}{item}");
                }

            }
            return total;
        }
    }
}
