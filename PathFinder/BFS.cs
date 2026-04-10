namespace PathFinder;
using PathFinder.MapGeneration;

public class BFS
{
    public List<Point> FindPath(string[,] map, Point start, Point end) // звичайний фифо приоритет не надо 1 крок 1 кг 
    {
        var queue = new Queue<Point>();
        queue.Enqueue(start);
        var cameFrom = new Dictionary<Point, Point?>(); // словник для відстеження шляху Ключ  точка    звідки ми прийшли.
        cameFrom[start] = null;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue(); // беремо першу точку 
            if (current.Equals(end)) break; // якщо знайшли фініш стоп 
            foreach (var next in GetNeighbors(current, map)) // тест всіх сусдів 
            {
                if (!cameFrom.ContainsKey(next)) // якщо туди не дійшли то в словнику немає 
                {
                    queue.Enqueue(next); // + ЧЕРГА 
                    cameFrom[next] = current; // зберігаємо шлях
                }
            }
        }
        return ReconstructPath(cameFrom, start, end); // збераємо шлях
    }
    private IEnumerable<Point> GetNeighbors(Point p, string[,] map)
    {
        var directions = new[]
        {
            new Point(p.Column, p.Row - 1),
            new Point(p.Column, p.Row + 1),
            new Point(p.Column - 1, p.Row),
            new Point(p.Column + 1, p.Row),
            new Point(p.Column - 1, p.Row - 1),
            new Point(p.Column + 1, p.Row - 1),
            new Point(p.Column - 1, p.Row + 1),
            new Point(p.Column + 1, p.Row + 1)
        };
        foreach (var dir in directions)
        {
            if (dir.Column >= 0 && dir.Column < map.GetLength(0) &&
                dir.Row >= 0 && dir.Row < map.GetLength(1))
            {
                if (map[dir.Column, dir.Row] != "█")  // тест на краї 
                {
                    yield return dir;
                }
            }
        }
    }
    private List<Point> ReconstructPath(Dictionary<Point, Point?> cameFrom, Point start, Point end)
    {
        var path = new List<Point>();
        if (!cameFrom.ContainsKey(end)) return path;  // немає фінішу шлях пустий
        var current = end; // по словнику йдемо від фінішу до старту 
        while (!current.Equals(start))
        {
            path.Add(current);
            current = cameFrom[current]!.Value; // перехід до бітька
        }
        path.Add(start);
        path.Reverse();  // переворот для маршруту 
        return path;
    }
}
