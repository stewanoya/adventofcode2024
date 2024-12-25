using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Solutions.Day3
{
    public class Decoder
    {
        private readonly string pattern = @"mul\(\d+,\d+\)|do\(\)|don't\(\)";
        private readonly string numberMatcher = "\\d+";
        private string _corruptedMemory = string.Empty;
        private Dictionary<int, bool> _modifiers = [];
        private List<string> _fixedMemory = [];
        public async Task LoadRawData(string path)
        {
            _corruptedMemory = await File.ReadAllTextAsync(path);
        }

        public void FixMemory()
        {
            var regex = new Regex(pattern);

            _fixedMemory = regex.Matches(_corruptedMemory).Select(i => i.Value).ToList();
        }

        public int RunCalculation()
        {
            var regex = new Regex(numberMatcher);
            var total = 0;
            var shouldDo = true;
            foreach (var memory in _fixedMemory)
            {
                if (memory == "do()")
                {
                    shouldDo = true;
                    continue;
                }

                if (memory == "don't()")
                {
                    shouldDo = false;
                    continue;
                }

                if (shouldDo)
                {
                    var numbers = regex.Matches(memory);

                    total += Convert.ToInt32(numbers[0].Value) * Convert.ToInt32(numbers[1].Value);
                }
            }

            return total;
        }
    }
}
