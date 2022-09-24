using System.Collections.Generic;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add extension methods to queues.
    /// </summary>
    public static class QueueExtensions {

        #if UNITY_2019
        
        /// <summary>
        /// This method is used to add a TryDequeue method to older versions of C#.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="value">The dequeued value.</param>
        /// <typeparam name="T">The type of object in the queue.</typeparam>
        /// <returns>True if an item was dequeued, otherwise false if the queue is empty.</returns>
        public static bool TryDequeue<T>(this Queue<T> queue, out T value) {
            value = default;
            if(queue == null) return false;
            if(queue.Count < 1) return false;
            value = queue.Dequeue();
            return true;
        }

        /// <summary>
        /// This method is used to add a TryPeek method to older versions of C#.
        /// </summary>
        /// <param name="queue">The queue.</param>
        /// <param name="value">The next item in the queue.</param>
        /// <typeparam name="T">The type of object in the queue.</typeparam>
        /// <returns>True if the queue is not empty, otherwise false.</returns>
        public static bool TryPeek<T>(this Queue<T> queue, out T value) {
            value = default;
            if(queue == null) return false;
            if(queue.Count < 1) return false;
            value = queue.Peek();
            return true;
        }
        
        #endif
        
    }
}