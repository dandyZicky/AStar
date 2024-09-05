namespace AStar;

class Search(int[] startPoint, int[] endPoint)      // Primary constructor
{
    private int[] Start { get; set; } = startPoint;
    private int[] End { get; set; } = endPoint;

    public void GridSearch(int[,] grid)
    {
        // Do check if start and end is valid on the grid
        var gridRowLen = grid.GetLength(0);
        var gridColLen = grid.GetLength(1);

        Console.WriteLine("Rows: " + gridRowLen);
        Console.WriteLine("Cols: " + gridColLen);

        if (Start[0] > gridColLen || Start[1] > gridRowLen || End[0] > gridColLen || End[1] > gridRowLen)
        {
            Console.WriteLine("Invalid start or end positions");
            return;
        }

        Console.WriteLine("Running A-Star search...");
    }
}

// class Grid()
// {
//     public struct Cell 
//     {
//         public int parent_i, parent, j;
//         public double f, g, h;
//     }
// }