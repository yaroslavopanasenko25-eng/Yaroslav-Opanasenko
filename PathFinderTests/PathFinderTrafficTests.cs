using PathFinder.MapGeneration;

// Буквально неймспейс з "затичками", щоб ваш код копілювався.
// Видаліть це і замініть на власний, де знаходяться реалізації алгоритмів
using Plugs;

using Point = PathFinder.MapGeneration.Point;

namespace PathFinderTests;

public class PathFinderTrafficTests
{
    private readonly Point _start = new(0, 0);
    private readonly Point _destination = new(18, 18);
    private string[,]? _map;

    [SetUp]
    public void Setup()
    {
        var optionsToGenerate = new MapGeneratorOptions()
        {
            Height = 20,
            Width = 20,
            Noise = 0.2f,
            Seed = 1000,
            AddTraffic = true,
            TrafficSeed = 13
        };

        var generator = new MapGenerator(optionsToGenerate);
        _map = generator.Generate();
    }

    [Test]
    [Category("WithTraffic")]
    public void TestDijkstraWeighted()
    {
        // Replace this with your actual implementation of Dijkstra
        var dijkstra = new Dijkstra();
        var (shortestPath, nodesVisited) = dijkstra.FindPath(_map!, _start, _destination);

        var expectedPath = Paths.DijkstraPathWeighted;

        Assert.That(nodesVisited, Is.EqualTo(216));
        Assert.That(shortestPath, Is.EqualTo(expectedPath).AsCollection);
    }
    
    [Test]
    [Category("WithTraffic")]
    public void TestAStarWeighted()
    {
        // Replace this with your actual implementation of AStar
        var aStar = new AStar();
        var (shortestPath, nodesVisited) = aStar.FindPath(_map!, _start, _destination);
    
        var expectedPath = Paths.AStarPathWeighted;
    
        Assert.That(nodesVisited, Is.EqualTo(198));
        Assert.That(shortestPath, Is.EqualTo(expectedPath).AsCollection);
    }
}