namespace PathFinder;
using PathFinder.MapGeneration;

public class Dekstra67
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
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
            var neighbors = MapGenerator.GetNeighbours(current.Column, current.Row, map, 1, true);
            foreach (var neighbour in neighbors)
            {
                string symbol = map[neighbour.Column, neighbour.Row];
                double timetodrive = 0;
                if (symbol == " ")
                {
                    timetodrive = 1;
                }
                else
                {
                    int traffic = int.Parse(symbol);
                    int speedInKM = 60 - (traffic - 1) * 6;
                    timetodrive = 60.0 / speedInKM;
                }
                double newDistance = distance[current] + timetodrive;
                if (distance.ContainsKey(neighbour) == false || newDistance < distance[neighbour])
                {
                    distance[neighbour] = newDistance;
                    queue.Enqueue(neighbour, newDistance);
                    cameFrom[neighbour] = current;
                }
            }

        }
        var path = new List<Point>();
        var current2 = destination;
        while (current2.Column != start.Column || current2.Row != start.Row)
        {
            path.Add(current2);
            current2 = cameFrom[current2];

        }
        path.Add(start);
        if (distance.ContainsKey(destination))
        {
            Console.WriteLine($"all time {distance[destination]} min");
        }
        else
        {
            Console.WriteLine("path not find"); 
        }
        return (path, visitedCounter);

    }
}
