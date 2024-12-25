using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.Day4
{
    public class WordFinder
    {
        public WordFinder(string wordToFind)
        {
            _chars = wordToFind.ToCharArray();
        }
        private char[] _chars = [];
        private byte _currentLetterIndex = 0;
        private int _wordCount = 0;
        private int _crossWordCount = 0;
        // tuple is (x,y)
        private Dictionary<string, (int, int)> _directions = new()
        {
            { "N", (0, -1) },
            { "S", (0, 1) },
            { "W", (-1, 0) },
            { "E", (1, 0) },
            { "NW", (-1, -1) },
            { "NE", (1, -1) },
            { "SW", (-1, 1) },
            { "SE", (1, 1) },
        };

        private Dictionary<string, (int, int)> _crossDirections = new()
        {
            { "NW", (-1, -1) },
            { "NE", (1, -1) },
            { "SW", (-1, 1) },
            { "SE", (1, 1) },
        };

        private char[] _crossMasValidChars = ['M', 'S']; 

        private (int, int) _nw = (-1, -1);
        private (int, int) _ne = (1, -1);
        private (int, int) _sw = (-1, 1);
        private (int, int) _se = (1, 1);

        private Dictionary<string, (int, int)> _directionsRetained = new();
        private List<List<char>> _searchBoard = [];
        public async Task LoadData(string path)
        {
            var data = await File.ReadAllTextAsync(path);
            _searchBoard = data.Split("\n").Select(i => i.ToCharArray().ToList()).ToList();
        }

        public int GetWordCount()
        {
            for (var y = 0; y < _searchBoard.Count; y++)
            {
                for (var x = 0; x < _searchBoard[y].Count; x++)
                {
                    if (_searchBoard[y][x] == _chars[_currentLetterIndex])
                    {
                        _currentLetterIndex++;
                        TraverseDirections(x, y);
                    }
                }
            }

            return _wordCount;
        }

        public int GetCrossMasWordCount()
        {
            for (var y = 0; y < _searchBoard.Count; y++)
            {
                for (var x = 0; x < _searchBoard[y].Count; x++)
                {
                    if (_searchBoard[y][x] == 'A' && CheckCorners(x, y))
                    {
                         var pairedChars1 = $"{_searchBoard[y + _nw.Item2][x + _nw.Item1]}{_searchBoard[y + _se.Item2][x + _se.Item1]}".OrderBy(i => i);
                         var pairedChars2 = $"{_searchBoard[y + _ne.Item2][x + _ne.Item1]}{_searchBoard[y + _sw.Item2][x + _sw.Item1]}".OrderBy(i => i);

                        if (Enumerable.SequenceEqual(pairedChars1, pairedChars2) && _crossMasValidChars.All(i => pairedChars1.Contains(i)))
                        {
                            _crossWordCount++;
                        }
                    }
                }
            }

            return _crossWordCount;
        }

        private bool CheckCorners(int x, int y)
        {
            var moveNw = MoveIsPossible(x, y, _nw.Item1, _nw.Item2);
            var moveNe = MoveIsPossible(x, y, _ne.Item1, _ne.Item2);
            var moveSw = MoveIsPossible(x, y, _sw.Item1, _sw.Item2);
            var moveSe = MoveIsPossible(x, y, _se.Item1, _se.Item2);

            return moveNe && moveNw && moveSw && moveSe;
        }

        private void ResetDirectionsRetained()
        {
            _directionsRetained.Clear();
            _directionsRetained = new Dictionary<string, (int, int)>(_directions);
        }
        private void TraverseDirections(int x, int y)
        {
            ResetDirectionsRetained();
            for (; _currentLetterIndex < _chars.Length; _currentLetterIndex++) 
            { 
                foreach (var entry in _directions)
                {
                    var moveX = entry.Value.Item1 * _currentLetterIndex;
                    var moveY = entry.Value.Item2 * _currentLetterIndex;
                    if (!MoveIsPossible(x, y, moveX, moveY))
                    {
                        _directionsRetained.Remove(entry.Key);
                        continue;
                    }

                    if (!IsNextLetter(x, y, moveX, moveY))
                    {
                        _directionsRetained.Remove(entry.Key);
                    }
                }


            }

            _currentLetterIndex = 0;
            _wordCount += _directionsRetained.Count;
        }

        private bool IsNextLetter(int x, int y, int moveX, int moveY)
        {
            return _searchBoard[y + moveY][x + moveX] == _chars[_currentLetterIndex];
        }

        private bool MoveIsPossible(int x, int y, int moveX, int moveY)
        {

            var result = _searchBoard.ElementAtOrDefault(y + moveY)?.ElementAtOrDefault(x + moveX);
            return result != null && result != default(char);
        }
    }
}
