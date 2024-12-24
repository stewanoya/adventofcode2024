namespace Solutions.Day1;

public class LocationSolver
{
    private List<int> _leftSide = [];
    private List<int> _rightSide = [];
    private List<int> _differences = [];
    private Dictionary<int, int> _similarityScores = [];
    public async Task LoadRawLocationData(string locationsRawPath)
    {
        var data = await File.ReadAllLinesAsync(locationsRawPath);

        foreach (var line in data)
        {
            var split = line.Split("  ");
            var leftSide = Convert.ToInt32(split[0]);
            var rightSide = Convert.ToInt32(split[1]);

            _leftSide.Add(leftSide);
            _rightSide.Add(rightSide);
        }
    }

    public void SortData()
    {
        _leftSide = [.. _leftSide.OrderBy(x => x)];
        _rightSide = [.. _rightSide.OrderBy(x => x)];
    }

    public void SetSimilarityScore()
    {
        foreach (var num in _leftSide)
        {
            var appearances = _rightSide.Where(i => i == num)?.Count() ?? 0;

            if (_similarityScores.TryGetValue(appearances, out _))
            {
                _similarityScores[num] += appearances;
            }
            else
            {
                _similarityScores.Add(num, appearances);
            }
        }
    }

    public int GetSimilarityScore()
    {
        return _similarityScores.Select(i => i.Key * i.Value).Sum(x => x);
    }

    public void CalculateDifferences()
    {
        for (var i = 0; i < _leftSide.Count; i++)
        {
            var leftSide = _leftSide[i];
            var rightSide = _rightSide[i];

            var difference = Math.Max(leftSide, rightSide) - Math.Min(leftSide, rightSide);

            _differences.Add(difference);
        }
    }
    public int GetSum()
    {
        return _differences.Sum();
    }
}
