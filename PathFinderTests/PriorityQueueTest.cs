using System.Drawing;
namespace PathFinderTests;

public class PriorityQueueTests
{
    /// <summary>
    /// Replace this type with your custom PriorityQueue
    /// </summary>
    private PriorityQueue<Point, int> _points;
    
    [SetUp]
    public void Setup()
    {
        // also do it here
        _points = new PriorityQueue<Point, int>();
        _points.Enqueue(new Point(1, 1), 1);
        _points.Enqueue(new Point(2, 2), 4);
        _points.Enqueue(new Point(3, 3), 3);
        _points.Enqueue(new Point(4, 4), 2);
    }

    [Test]
    public void TestCount()
    {
        Assert.That(_points.Count, Is.EqualTo(4));
    }

    [Test]
    public void TestDequeued()
    {
        Assert.That(_points.Dequeue(), Is.EqualTo(new Point(1, 1)));
    }
    
    [Test]
    public void TestEnqueue()
    {
        _points.Enqueue(new Point(5,5), 2);
        _points.Dequeue();
        Assert.That(_points.Dequeue(), Is.EqualTo(new Point(5, 5)));
    }
    
    [Test]
    public void TestPeek()
    {
        _points.Enqueue(new Point(5,5), 1);
        _points.Dequeue();
        Assert.That(_points.Peek(), Is.EqualTo(new Point(5, 5)));
    }
}
