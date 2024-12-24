namespace Day1Proj;

public class LocationSolver
{
  public async Task LoadRawLocationData(string locationsRawPath) 
  {
    var data = await File.ReadAllLinesAsync(locationsRawPath);

    foreach (var line in data)  
    {
      var split = line.Split("  ");
      var leftSide = Convert.ToInt32(split[0]);
      var rightSide = Convert.ToInt32(split[1]);

      LeftSide.Add(leftSide);
      RightSide.Add(rightSide);
    }
  }

  public void SortData() 
  {
    LeftSide = [.. LeftSide.OrderBy(x => x)];
    RightSide = [.. RightSide.OrderBy(x => x)];
  }

  public void SetSimilarityScore() 
  {
    foreach (var num in LeftSide) 
    {
      var appearances = RightSide.Where(i => i == num)?.Count() ?? 0;

      if (SimilarityScores.TryGetValue(appearances, out _)) 
      {
        SimilarityScores[num] += appearances;
      } else {
        SimilarityScores.Add(num, appearances);
      }
    }
  }

  public int GetSimilarityScore() 
  {
    return SimilarityScores.Select(i => i.Key * i.Value).Sum(x => x);
  }

  public void CalculateDifferences() 
  {
    for (var i = 0; i < LeftSide.Count; i++) 
    {
      var leftSide = LeftSide[i];
      var rightSide = RightSide[i];

      var difference = Math.Max(leftSide,rightSide) - Math.Min(leftSide,rightSide);

      Differences.Add(difference);
    }
  }
  public int GetSum() 
  {
    return Differences.Sum();
  }
  private List<int> LeftSide = [];
  private List<int> RightSide = [];
  private List<int> Differences = [];
  private Dictionary<int, int> SimilarityScores = new();
}
