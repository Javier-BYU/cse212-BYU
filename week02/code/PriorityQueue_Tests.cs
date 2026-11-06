using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities (e.g., Low, Medium, High) and dequeue them.
    // Expected Result: Items should be removed in order of highest priority (highest number first).
    // Defect(s) Found: The loop range stopped before the last element and priority comparison used '>=' instead of '>', and items were not removed from the queue.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("LowPriority", 1);
        priorityQueue.Enqueue("HighPriority", 10);
        priorityQueue.Enqueue("MediumPriority", 5);

        
        Assert.AreEqual("HighPriority", priorityQueue.Dequeue());
        Assert.AreEqual("MediumPriority", priorityQueue.Dequeue());
        Assert.AreEqual("LowPriority", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with the exact same highest priority.
    // Expected Result: FIFO strategy must be preserved; the first one added with that priority should be dequeued first.
    // Defect(s) Found: Using '>=' instead of '>' overwrote the first inserted item, violating FIFO order for ties.
    public void TestPriorityQueue_EqualPriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstInLine", 5);
        priorityQueue.Enqueue("SecondInLine", 5);
        priorityQueue.Enqueue("ThirdInLine", 5);

        // Como todos tienen prioridad 5, debe salir estrictamente el primero que llegó (FIFO)
        Assert.AreEqual("FirstInLine", priorityQueue.Dequeue());
        Assert.AreEqual("SecondInLine", priorityQueue.Dequeue());
        Assert.AreEqual("ThirdInLine", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: None (handled correctly by the exception check).
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}