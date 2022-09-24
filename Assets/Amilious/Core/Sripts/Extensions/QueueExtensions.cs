/*//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                                                                                                                    //
//    _____            .__ .__   .__                             _________  __              .___.__                   //
//   /  _  \    _____  |__||  |  |__|  ____   __ __  ______     /   _____/_/  |_  __ __   __| _/|__|  ____   ______   //
//  /  /_\  \  /     \ |  ||  |  |  | /  _ \ |  |  \/  ___/     \_____  \ \   __\|  |  \ / __ | |  | /  _ \ /  ___/   //
// /    |    \|  Y Y  \|  ||  |__|  |(  <_> )|  |  /\___ \      /        \ |  |  |  |  // /_/ | |  |(  <_> )\___ \    //
// \____|__  /|__|_|  /|__||____/|__| \____/ |____//____  >    /_______  / |__|  |____/ \____ | |__| \____//____  >   //
//         \/       \/                                  \/             \/                    \/                 \/    //
//                                                                                                                    //
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Website:        http://www.amilious.com         Unity Asset Store: https://assetstore.unity.com/publishers/62511  //
//  Discord Server: https://discord.gg/SNqyDWu            Copyright© Amilious since 2022                              //                    
//  This code is part of an asset on the unity asset store. If you did not get this from the asset store you are not  //
//  using it legally. Check the asset store or join the discord for the license that applies for this script.         //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/

using System.Collections.Generic;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add extension methods to queues.
    /// </summary>
    public static class QueueExtensions {

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        #if UNITY_2019 || UNITY_2020
        
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
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}