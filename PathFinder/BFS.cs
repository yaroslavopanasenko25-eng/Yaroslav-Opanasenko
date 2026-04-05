namespace PathFinder;
using PathFinder.MapGeneration;

public class BFS
{
    public List<Point> FindPath(string[,] map, Point start, Point end)
    {
        var queue = new Queue<Point>();
        queue.Enqueue(start);
        var cameFrom = new Dictionary<Point, Point?>();
        cameFrom[start] = null;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Equals(end)) break;
            foreach (var next in GetNeighbors(current, map))
            {
                if (!cameFrom.ContainsKey(next))
                {
                    queue.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }
        return ReconstructPath(cameFrom, start, end);
    }
    private IEnumerable<Point> GetNeighbors(Point p, string[,] map)
    {
        var directions = new[]
        {
            new Point(p.Column, p.Row - 1), new Point(p.Column, p.Row + 1),
            new Point(p.Column - 1, p.Row), new Point(p.Column + 1, p.Row)
        };
        foreach (var dir in directions)
        {
            if (dir.Column >= 0 && dir.Column < map.GetLength(0) &&
                dir.Row >= 0 && dir.Row < map.GetLength(1))
            {
                if (map[dir.Column, dir.Row] != "█") 
                {
                    yield return dir;
                }
            }
        }
    }
    private List<Point> ReconstructPath(Dictionary<Point, Point?> cameFrom, Point start, Point end)
    {
        var path = new List<Point>();
        if (!cameFrom.ContainsKey(end)) return path; 
        var current = end;
        while (!current.Equals(start))
        {
            path.Add(current);
            current = cameFrom[current]!.Value;
        }
        path.Add(start);
        path.Reverse(); 
        return path;
    }
}
