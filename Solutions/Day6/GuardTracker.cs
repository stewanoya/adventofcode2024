using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Solutions.Day6
{
    public class GuardTracker
    {
        private List<List<char>> _map = [];
        private (int, int) _guardLocation = (0, 0);
        private (int, int) _originalGuardLocation = (0, 0);
        private List<List<char>> _originalMap = [];
        private List<(int, int)> _originalPath = [];
        private char _guard => _map[_guardLocation.Item2][_guardLocation.Item1];
        private Dictionary<char, string> _guardDirectionMap = new()
        {
            { '^', "N" },
            { '>', "E" },
            { '<', "W" },
            { 'V', "S" },
        };

        // add bstacle along path

        public async Task LoadData(string path)
        {
            var data = await File.ReadAllLinesAsync(path);

            _map = data.Select(i => i.ToCharArray().ToList()).ToList();
        }

        public void FindGuard()
        {
            var guardRow = _map.FirstOrDefault(i => i.Contains('^')) ?? throw new ArgumentNullException($"guardRow not found");
            var y = _map.IndexOf(guardRow);
            var x = guardRow.IndexOf('^');

            _guardLocation.Item1 = x;
            _guardLocation.Item2 = y;
            _originalGuardLocation.Item1 = x;
            _originalGuardLocation.Item2 = y;
        }

        public void TrackGuard()
        {
            var direction = ArrayUtil.GetNextMove(GetDirection());
            var moveX = direction.Item1;
            var moveY = direction.Item2;
            while (ArrayUtil.MoveIsPossible(_guardLocation.Item1, _guardLocation.Item2, moveX, moveY, _map))
            {
                if (IsObstructed(_guardLocation.Item1 + moveX, _guardLocation.Item2 + moveY))
                {
                    TurnGuard();
                }
                else
                {
                    _originalPath.Add((_guardLocation.Item1, _guardLocation.Item2));
                    MoveGuard(moveX, moveY);
                }

                direction = ArrayUtil.GetNextMove(GetDirection());
                moveX = direction.Item1;
                moveY = direction.Item2;
            }

            _originalPath.Add((_guardLocation.Item1, _guardLocation.Item2));

            _originalMap = new (_map.Select(i => new List<char>(i.ToList())));
        }

        public void TrackGuardLoop()
        {
            ResetMapToOriginalPath();
            ResetGuard();
            var successfulLocations = new List<(int, int)>();
            foreach (var location in _originalPath.Distinct())
            {
                var movementDirection = ArrayUtil.GetNextMove(GetDirection());
                var moveX = movementDirection.Item1;
                var moveY = movementDirection.Item2;
                AddObstruction(location);
                var visitedLocations = new List<(string, int, int)>();
                while (ArrayUtil.MoveIsPossible(_guardLocation.Item1, _guardLocation.Item2, moveX, moveY, _map))
                {
                    if (IsObstructed(_guardLocation.Item1 + moveX, _guardLocation.Item2 + moveY))
                    {
                        TurnGuard();
                    }
                    else
                    {
                        if (visitedLocations.Contains((GetDirection(), _guardLocation.Item1, _guardLocation.Item2)))
                        {
                            successfulLocations.Add(location);
                            break;
                        }

                        visitedLocations.Add((GetDirection(), _guardLocation.Item1, _guardLocation.Item2));
                        MoveGuardWithNewMarker(moveX, moveY);
                    }


                    movementDirection = ArrayUtil.GetNextMove(GetDirection());
                    moveX = movementDirection.Item1;
                    moveY = movementDirection.Item2;
                }
                ResetMapToOriginalPath();
                ResetGuard();
            }

            Console.WriteLine(successfulLocations.Count);
        }

        private void ResetGuard()
        {
            _guardLocation.Item1 = _originalGuardLocation.Item1;
            _guardLocation.Item2 = _originalGuardLocation.Item2;

            _map[_originalGuardLocation.Item2][_originalGuardLocation.Item1] = '^';
        }

        private void AddObstruction((int, int) location)
        {
            if (location.Item1 == _originalGuardLocation.Item1 && location.Item2 == _originalGuardLocation.Item2)
            {
                return;
            }

            _map[location.Item2][location.Item1] = 'O';
        }

        private void ResetMapToOriginalPath()
        {
            _map = new(_originalMap.Select(i => new List<char>(i.ToList())));
        }

        public int GetStepCount()
        {
            return _map.Select(i => i.Where(x => x == 'X').Count()).Sum();
        }

        private void MoveGuard(int moveX, int moveY)
        {
            var newY = _guardLocation.Item2 + moveY;
            var newX = _guardLocation.Item1 + moveX;
            _map[newY][newX] = _guard;
            _map[_guardLocation.Item2][_guardLocation.Item1] = 'X';
            UpdateGuardLocation(newX, newY);
        }
        private void MoveGuardWithNewMarker(int moveX, int moveY)
        {
            var newY = _guardLocation.Item2 + moveY;
            var newX = _guardLocation.Item1 + moveX;
            _map[newY][newX] = _guard;
            _map[_guardLocation.Item2][_guardLocation.Item1] = '*';
            UpdateGuardLocation(newX, newY);
        }

        private void UpdateGuardLocation(int newX, int newY)
        {
            _guardLocation.Item1 = newX;
            _guardLocation.Item2 = newY;
        }

        private string GetDirection()
        {
            return _guardDirectionMap[_guard];
        }

        private void TurnGuard()
        {
            char newVal = '^';
            switch (_guard)
            {
                case '^':
                    newVal = '>';
                    break;
                case '>':
                    newVal = 'V';
                    break;
                case 'V':
                    newVal = '<';
                    break;
                case '<':
                    newVal = '^';
                    break;
            }

            _map[_guardLocation.Item2][_guardLocation.Item1] = newVal;
        }

        private bool IsObstructed(int x, int y)
        {
            if (_map[y][x] == '#' || _map[y][x] == 'O')
            {
                return true;
            }

            return false;
        }
    }
}
