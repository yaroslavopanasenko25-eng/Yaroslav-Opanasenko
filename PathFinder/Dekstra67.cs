namespace PathFinder;
using PathFinder.MapGeneration;

public class Dekstra67
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
    {
        return FindPathInternal(map, start, destination);
    }

    public (List<Point>, int) FindPathThroughWaypoint(string[,] map, Point start, Point waypoint, Point destination)
    {
        var (firstPart, visitedFirst) = FindPathInternal(map, start, waypoint);
        if (firstPart.Count == 0)
        {
            return (new List<Point>(), visitedFirst);
        }

        var (secondPart, visitedSecond) = FindPathInternal(map, waypoint, destination);
        if (secondPart.Count == 0)
        {
            return (new List<Point>(), visitedFirst + visitedSecond);
        }

        // Удаляем дублирующийся waypoint на стыке сегментов.
        var fullPath = new List<Point>(firstPart);
        fullPath.AddRange(secondPart.Skip(1));
        return (fullPath, visitedFirst + visitedSecond);
    }

    private static (List<Point>, int) FindPathInternal(string[,] map, Point start, Point destination)
    {
        MyPriorityQueue queue = new MyPriorityQueue();
        Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
        Dictionary<Point, double> distance = new Dictionary<Point, double>();
        int visitedCounter = 0;

        queue.Enqueue(start, 0.0);
        distance[start] = 0.0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visitedCounter++;

            if (current.Column == destination.Column && current.Row == destination.Row)
            {
                break;
            }

            var neighbors = MapGenerator.GetNeighbours(current.Column, current.Row, map, 1, true, true);
            foreach (var neighbour in neighbors)
            {
                var moveMultiplier = IsDiagonal(current, neighbour) ? 1.4 : 1.0;
                var timeToDrive = GetCellTravelTime(map[neighbour.Column, neighbour.Row]) * moveMultiplier;
                var newDistance = distance[current] + timeToDrive;

                if (!distance.ContainsKey(neighbour) || newDistance < distance[neighbour])
                {
                    distance[neighbour] = newDistance;
                    queue.Enqueue(neighbour, newDistance);
                    cameFrom[neighbour] = current;
                }
            }
        }

        if (!distance.ContainsKey(destination))
        {
            Console.WriteLine("path not find");
            return (new List<Point>(), visitedCounter);
        }

        var path = ReconstructPath(cameFrom, start, destination);
        Console.WriteLine($"all time {distance[destination]} min");
        return (path, visitedCounter);
    }

    private static List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point start, Point destination)
    {
        var path = new List<Point> { destination };
        var current = destination;

        while (current.Column != start.Column || current.Row != start.Row)
        {
            if (!cameFrom.TryGetValue(current, out var previous))
            {
                return new List<Point>();
            }

            current = previous;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private static bool IsDiagonal(Point from, Point to)
    {
        return from.Column != to.Column && from.Row != to.Row;
    }

    private static double GetCellTravelTime(string symbol)
    {
        if (symbol == " ")
        {
            return 1.0;
        }

        int traffic = int.Parse(symbol);
        int speedInKm = 60 - (traffic - 1) * 6;
        return 60.0 / speedInKm;
    }
}
