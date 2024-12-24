namespace Solutions.Day1;

public static class Day1
{
  public static async Task Solve() 
  {
    LocationSolver locationsSolver = new LocationSolver();

    await locationsSolver.LoadRawLocationData("./data.txt");
    locationsSolver.SortData();
    locationsSolver.CalculateDifferences();
    var sum = locationsSolver.GetSum();
    Console.WriteLine(sum);
    
    locationsSolver.SetSimilarityScore();
    var score = locationsSolver.GetSimilarityScore();
    Console.WriteLine(score);
  }

}
