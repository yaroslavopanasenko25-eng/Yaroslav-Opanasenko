namespace PathFinder;
using PathFinder.MapGeneration;
public class QueueItem
{
    public Point PointDetails;
    public double PriorityValue;
}

public class MyPriorityQueue
{
    private List<QueueItem> elements = new List<QueueItem>();

    public void Enqueue(Point item, double priority)
    {
        QueueItem newItem = new QueueItem();
        newItem.PointDetails = item;
        newItem.PriorityValue = priority;

        elements.Add(newItem);
        Bubbleup(elements.Count - 1);
    }

    public Point Dequeue()
    {
        Point first = elements[0].PointDetails;
        if (elements.Count > 1)
        {
            int lastIndex = elements.Count - 1;
            var last = elements[lastIndex];
            elements[0] = last;
            elements.RemoveAt(lastIndex);
            BubbleDown(0);
        }
        else
        {
            elements.RemoveAt(0);
        }
        return first;
    }

    private void Bubbleup(int index)
    {
        while (index > 0)
        {
            int parentindex = (index - 1) / 2;
            double mypriority = elements[index].PriorityValue;
            double parentpriority = elements[parentindex].PriorityValue;

            if (mypriority < parentpriority)
            {
                var difference = elements[index];
                elements[index] = elements[parentindex];
                elements[parentindex] = difference;
                index = parentindex;
            }
            else
            {
                break;
            }
        }
    }
    public int Count
    {
        get
        {
            return elements.Count;
        }
    }
    public Point Peek()
    {
        return elements[0].PointDetails;
    }
    private void BubbleDown(int index)
    {
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallestIndex = index;
            if (leftChildIndex < elements.Count)
            {
                double leftChildPriority = elements[leftChildIndex].PriorityValue;
                double smallestPriority = elements[smallestIndex].PriorityValue;
                if (leftChildPriority < smallestPriority)
                {
                    smallestIndex = leftChildIndex;
                }
            }
            if (rightChildIndex < elements.Count)
            {
                double rightChildPriority = elements[rightChildIndex].PriorityValue;
                double smallestPriority = elements[smallestIndex].PriorityValue;
                if (rightChildPriority < smallestPriority)
                {
                    smallestIndex = rightChildIndex;
                }
            }
            if (smallestIndex == index)
            {
                break;
            }
            var difference = elements[index];
            elements[index] = elements[smallestIndex];
            elements[smallestIndex] = difference;
            index = smallestIndex;
        }
    }
}
