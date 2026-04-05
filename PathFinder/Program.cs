using PathFinder;
using PathFinder.MapGeneration;

var optionsToGenerate = new MapGeneratorOptions()
{
    Height = 10,
    Width = 100,
    AddTraffic = true,
    TrafficSeed = 123456789
};

var generator = new MapGenerator(optionsToGenerate);
string[,]? map = generator.Generate();
if (map == null) return;

Point start = new Point(1, 1);
Point end = new Point(98, 8);

var solver = new Dekstra67();
var (path, visitedCount) = solver.FindPath(map, start, end);

var printer = new MapPrinter();
printer.Print(map, path); 

Console.WriteLine($"Visited: {visitedCount}");