using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day5
{
    public class ManualChecker
    {
        private Dictionary<string, List<string>> _rules = [];
        private List<List<string>> _manuals = [];
        private List<List<string>> _incorrectManuals = [];
        private List<string> _middles = [];
        public async Task LoadData(string path)
        {
            var data = await File.ReadAllLinesAsync(path);
            var indexOfManuals = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if (!data[i].Contains("|"))
                {
                    indexOfManuals = i + 1;
                    break;
                }
                var split = data[i].Split('|');
                if (_rules.TryGetValue(split[0], out _))
                {
                    _rules[split[0]].Add(split[1]);
                }
                else
                {
                    _rules[split[0]] = [split[1]];
                }
            }

            for (var i = indexOfManuals; i < data.Length; i++)
            {
                _manuals.Add([.. data[i].Split(",")]);
            }
        }

        public void FixBrokenManuals()
        {
            foreach (var manual in _incorrectManuals)
            {
                var rules = _rules.Where(i => manual.Contains(i.Key));
                var occuranceScore = new Dictionary<string, int>();
                foreach (var page in manual)
                {
                    occuranceScore[page] = rules.Where(i => i.Value.Contains(page)).Count();
                }

                var fixedManual = occuranceScore.OrderBy(i => i.Value).Select(i => i.Key).ToList();
                if (IsManualCorrect(fixedManual))
                {
                    _middles.Add(fixedManual[fixedManual.Count / 2]);
                };
            }
        }

        public bool IsManualCorrect(List<string> manual)
        {
            var manualIsCorrect = true;
            for (var i = 0; i < manual.Count; i++)
            {
                if (!_rules.TryGetValue(manual[i], out _))
                {
                    continue;
                }

                var rules = _rules[manual[i]];

                //Console.WriteLine($"Checking num: {manual[i]}, manual: {string.Join(", ", manual)}  rules: {string.Join(", ", rules)}");

                if (!rules.All(x => manual.IndexOf(x) == -1 || manual.IndexOf(x) > i))
                {
                    manualIsCorrect = false;
                    break;
                }
            }

            return manualIsCorrect;
        }



        public void CheckManuals()
        {
            foreach (var manual in _manuals)
            {
                var manualIsCorrect = true;
                for (var i = 0; i < manual.Count; i++)
                {
                    if (!_rules.TryGetValue(manual[i], out _))
                    {
                        continue;
                    }

                    var rules = _rules[manual[i]];

                    //Console.WriteLine($"Checking num: {manual[i]}, manual: {string.Join(", ", manual)}  rules: {string.Join(", ", rules)}");

                    if (!rules.All(x => manual.IndexOf(x) == -1 || manual.IndexOf(x) > i))
                    {
                        _incorrectManuals.Add(manual);
                        break;
                    }
                }

                //if (manualIsCorrect)
                //{
                //    var middle = manual[manual.Count / 2];
                //    //Console.WriteLine(middle);
                //    _middles.Add(middle);
                //}
            }
        }

        public int GetMiddleSum()
        {
            return _middles.Select(i => Convert.ToInt32(i)).Sum();
        }
    }
}
