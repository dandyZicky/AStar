namespace AStar;

internal class Cell(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

internal class CellDetail
{
    public int ParentRow { get; set; } = 0;
    public int ParentColumn { get; set; } = 0;
    public double TotalCost { get; set; } = double.MaxValue;
    public double CostSoFar { get; set; } = double.MaxValue;
    public double Heuristic { get; set; } = 0.0;
}

internal interface IHeuristic
{
    public double GetHeuristicValue();
}

internal class Euclidean(Cell currentCell, Cell destinationCell) : IHeuristic
{
    public double GetHeuristicValue()
    {
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
    private int GridHeight { get; set; } = grid.GetLength(0);
    private int GridLength { get; set; } = grid.GetLength(1);

    private bool _isUnblocked(Cell gridCell)
    {
        return grid[gridCell.X, gridCell.Y] == 1;
    }

    private bool _isDestination(Cell gridCell)
    {
        return gridCell.X == endPose.X && gridCell.Y == endPose.Y;
    }

    private static double _calculateHeuristic(IHeuristic heuristic)
    {
        return heuristic.GetHeuristicValue();
    }

    private bool _isValid(Cell gridCell)
    {
        if (gridCell.X >= 0 && gridCell.X < GridHeight && gridCell.Y >= 0 && gridCell.Y < GridLength) return true;
        // Console.WriteLine("Invalid start or end positions");
        return false;
    }

    private void _tracePath(CellDetail[,] cellDetails)
    {
        var path = new List<Cell>();
        var currentCell = new Cell(endPose.X, endPose.Y);

        while (!(cellDetails[currentCell.X, currentCell.Y].ParentRow == currentCell.X &&
                 cellDetails[currentCell.X, currentCell.Y].ParentColumn == currentCell.Y))
        {
            path.Add(new Cell(currentCell.X, currentCell.Y));
            var tempRow = cellDetails[currentCell.X, currentCell.Y].ParentRow;
            var tempColumn = cellDetails[currentCell.X, currentCell.Y].ParentColumn;
            currentCell.X = tempRow;
            currentCell.Y = tempColumn;
            Console.WriteLine($"[{currentCell.X}, {currentCell.Y}]");
        }

        path.Reverse();

        foreach (var i in path)
        {
            Console.Write($"{i.X},{i.Y} -> ");
        }

        Console.WriteLine();
    }

    public void GridSearch()
    {
        if (!(_isValid(startPose) && _isValid(endPose))) return;

        if (!(_isUnblocked(startPose) && _isUnblocked(endPose))) return;

        if (_isDestination(startPose))
        {
            Console.WriteLine("Reached Destination Cell: " + endPose.X + ", " + endPose.Y);
            return;
        }

        var closedList = new bool[GridHeight, GridLength];
        var cellDetails = new CellDetail[GridHeight, GridLength];
        for (var i = 0; i < GridHeight; i++)
        {
            for (var j = 0; j < GridLength; j++)
            {
                closedList[i, j] = false;
                cellDetails[i, j] = new CellDetail();
            }
        }

        var currentPose = startPose;
        cellDetails[currentPose.X, currentPose.Y].TotalCost = 0;
        cellDetails[currentPose.X, currentPose.Y].CostSoFar = 0;
        cellDetails[currentPose.X, currentPose.Y].Heuristic = 0;
        cellDetails[currentPose.X, currentPose.Y].ParentRow = currentPose.X;
        cellDetails[currentPose.X, currentPose.Y].ParentColumn = currentPose.Y;

        var openList =
            new SortedSet<(double, Cell)>(Comparer<(double, Cell)>.Create((a, b) => a.Item1.CompareTo(b.Item1)))
                { (0.0, new Cell(currentPose.X, currentPose.Y)) };

        var foundDest = false;

        while (openList.Count > 0)
        {
            var p = openList.Min;
            openList.Remove(p);

            currentPose.X = p.Item2.X;
            currentPose.Y = p.Item2.Y;
            closedList[currentPose.X, currentPose.Y] = true;

            var directions = new[,]
                { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

            for (var i = 0; i < directions.GetLength(0); i++)
            {
                var newCell = new Cell(currentPose.X + directions[i, 0], currentPose.Y + directions[i, 1]);

                if (_isValid(newCell) && _isUnblocked(newCell) && !closedList[newCell.X, newCell.Y])
                {
                    if (_isDestination(newCell))
                    {
                        cellDetails[newCell.X, newCell.Y].ParentRow = currentPose.X;
                        cellDetails[newCell.X, newCell.Y].ParentColumn = currentPose.Y;
                        Console.WriteLine("Reached Destination Cell: " + newCell.X + ", " + newCell.Y);
                        // Found
                        _tracePath(cellDetails);
                        foundDest = true;
                        return;
                    }

                    var gNew = cellDetails[currentPose.X, currentPose.Y].TotalCost + 1.0;
                    var hNew = _calculateHeuristic(new Euclidean(newCell, endPose));
                    var fNew = gNew + hNew;

                    if (Math.Abs(cellDetails[newCell.X, newCell.Y].TotalCost - double.MaxValue) < 0.000000000001 ||
                        cellDetails[newCell.X, newCell.Y].TotalCost > fNew)
                    {
                        openList.Add((fNew, newCell));
                        cellDetails[newCell.X, newCell.Y].TotalCost = fNew;
                        cellDetails[newCell.X, newCell.Y].CostSoFar = gNew;
                        cellDetails[newCell.X, newCell.Y].Heuristic = hNew;
                        cellDetails[newCell.X, newCell.Y].ParentRow = currentPose.X;
                        cellDetails[newCell.X, newCell.Y].ParentColumn = currentPose.Y;
                    }
                }
            }
        }

        if (!foundDest)
        {
            Console.WriteLine("No Destination Cells Found");
        }
    }
}