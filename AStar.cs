namespace AStar;

internal class Cell(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

internal interface IHeuristic
{
    public double GetHeuristicValue();
}

internal class Euclidean(Cell currentCell, Cell destinationCell) : IHeuristic
{
    public double GetHeuristicValue()
    {
        Console.WriteLine($"Euclidean, Destination Cell: {destinationCell.X}, {destinationCell.Y}");
        return Math.Sqrt(
            Math.Pow(currentCell.X - destinationCell.X, 2) + Math.Pow(currentCell.Y - destinationCell.Y, 2)
        );
    }
}

internal class Manhattan(Cell currentCell, Cell destinationCell) : IHeuristic
{
    public double GetHeuristicValue()
    {
        return Math.Abs(currentCell.X - destinationCell.X) + Math.Abs(currentCell.Y - destinationCell.Y);
    }
}

internal class Diagonal(Cell currentCell, Cell destinationCell) : IHeuristic
{
    public double GetHeuristicValue()
    {
        var dx = Math.Abs(destinationCell.X - currentCell.X);
        var dy = Math.Abs(destinationCell.Y - currentCell.Y);

        throw new NotImplementedException();
        // var heur = 
    }
}

internal class Search(int[,] grid, Cell startPose, Cell endPose) // Primary constructor
{
    private int GridLength { get; set; } = grid.GetLength(0);
    private int GridHeight { get; set; } = grid.GetLength(1);

    private bool _isUnblocked(Cell gridCell)
    {
        return grid[gridCell.X, gridCell.Y] == 0;
    }

    private bool _isDestination(Cell gridCell)
    {
        return gridCell.X == endPose.X && gridCell.Y == endPose.Y;
    }

    private static double _calculateHeuristic(IHeuristic heuristic)
    {
        return heuristic.GetHeuristicValue();
    }

    public bool IsValid(Cell gridCell)
    {
        if (gridCell.X <= GridLength && startPose.Y <= GridHeight && endPose.X <= GridLength &&
            endPose.Y <= GridHeight) return true;
        Console.WriteLine("Invalid start or end positions");
        return false;
    }

    public void GridSearch()
    {
        Console.WriteLine("Rows: " + GridLength);
        Console.WriteLine("Cols: " + GridHeight);

        Console.WriteLine("Running A-Star search...");

        if (!IsValid(startPose) && !IsValid(endPose)) return;

        IHeuristic euclidean = new Euclidean(startPose, endPose);
        // IHeuristic manhattan = new Manhattan(startPose, endPose);
        var heuristicVal = _calculateHeuristic(euclidean);

        Console.WriteLine("Starting Heuristic: " + heuristicVal);
    }
}