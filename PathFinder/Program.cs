using PathFinder;
using PathFinder.MapGeneration;

Console.WriteLine("67~67~67~67~67~^67~67~67~67~67~^");
var optionsToGenerate = new MapGeneratorOptions()
{
    Height = 100,
    Width = 100,
    AddTraffic = true,
    TrafficSeed = 1234,
    Seed = 1,
    Noise = 0.3f
};

var generator = new MapGenerator(optionsToGenerate);
string[,]? map = generator.Generate();
if (map == null) return;

Point requestedStart = new Point(1, 1);
Point requestedWaypoint = new Point(50, 50);
Point requestedEnd = new Point(98, 67);

Point start = FindNearestDriveablePoint(map, requestedStart);
Point waypoint = FindNearestDriveablePoint(map, requestedWaypoint);
Point end = FindNearestDriveablePoint(map, requestedEnd);

var solver = new Dekstra67();
var (path, visitedCount) = solver.FindPathThroughWaypoint(map, start, waypoint, end);

var printer = new MapPrinter();
printer.Print(map, path, start, waypoint, end);

Console.WriteLine($"Visited: {visitedCount}");

static Point FindNearestDriveablePoint(string[,] map, Point requested)
{
    if (IsDriveable(map, requested))
    {
        return requested;
    }

    var radiusLimit = Math.Max(map.GetLength(0), map.GetLength(1));
    for (int radius = 1; radius <= radiusLimit; radius++)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                if (Math.Abs(dx) != radius && Math.Abs(dy) != radius)
                {
                    continue;
                }

                var candidate = new Point(requested.Column + dx, requested.Row + dy);
                if (IsDriveable(map, candidate))
                {
                    return candidate;
                }
            }
        }
    }

    return requested;
}

static bool IsDriveable(string[,] map, Point point)
{
    if (point.Column < 0 || point.Row < 0 || point.Column >= map.GetLength(0) || point.Row >= map.GetLength(1))
    {
        return false;
    }

    var symbol = map[point.Column, point.Row];
    return symbol == " " || int.TryParse(symbol, out _);
}
