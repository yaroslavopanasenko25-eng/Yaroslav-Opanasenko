using PathFinder.MapGeneration;

namespace PathFinder.MapGeneration;

public class MapPrinter
{
    public void Print(string[,] maze, List<Point> path)
    {
        var pathSet = new HashSet<(int, int)>();
        foreach (var p in path) pathSet.Add((p.Column, p.Row));

        PrintTopLine(maze);
        for (var row = 0; row < maze.GetLength(1); row++)
        {
            Console.Write($"{row}\t");
            for (var column = 0; column < maze.GetLength(0); column++)
            {
                if (path.Count > 0 && column == path.Last().Column && row == path.Last().Row) Console.Write("A");
                else if (path.Count > 0 && column == path.First().Column && row == path.First().Row) Console.Write("B");
                else if (pathSet.Contains((column, row))) Console.Write(".");
                else Console.Write(maze[column, row]);
            }
            Console.WriteLine();
        }
    }

    private void PrintTopLine(string[,] maze)
    {
        Console.Write(" \t");
        for (int i = 0; i < maze.GetLength(0); i++) Console.Write(i % 10 == 0 ? i / 10 : " ");
        Console.Write("\n \t");
        for (int i = 0; i < maze.GetLength(0); i++) Console.Write(i % 10);
        Console.WriteLine("\n");
    }
}