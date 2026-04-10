using PathFinder.MapGeneration;

namespace PathFinder;

public interface IPathFinder
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination);
    
    
}