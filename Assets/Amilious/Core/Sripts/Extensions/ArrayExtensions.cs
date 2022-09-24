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

using System;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add methods to the <see cref="Array"/> class.
    /// </summary>
    public static class ArrayExtensions {
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to fill an array with the given value for the given indices.
        /// </summary>
        /// <param name="ary">The array that you want to fill.</param>
        /// <param name="value">The value that you want to fill the array with.</param>
        /// <param name="startIndex">The starting index that you want to fill.</param>
        /// <param name="indices">The number of indices that you want to fill.</param>
        /// <typeparam name="T">The type contained in the array.</typeparam>
        /// <exception cref="InvalidOperationException">thrown if the array is null
        /// or if the indices are out of range.</exception>
        public static void Fill<T>(this T[] ary, T value, int startIndex, int indices) {
            if(ary == null || ary.Length < startIndex + indices || startIndex < 0) throw new 
                InvalidOperationException("Either the indices went out of bounds or the array was null.");
            for(var i = 0; i < indices; i++) {
                var index = startIndex + i;
                ary[index] = value;
            }
        }

        /// <summary>
        /// This method is used to fill values in an array from another array.
        /// </summary>
        /// <param name="to">The array you want to copy values to.</param>
        /// <param name="from">The array you want to copy values from.</param>
        /// <param name="startIndex">The starting index that you want to fill.</param>
        /// <param name="indices">The number of indices that you want to fill.</param>
        /// <param name="modifier">This is an optional function that can be used to modify
        /// values as they are copied.</param>
        /// <param name="offset">This is an optional offset that can be used.  The offset
        /// will be applied to the from array.</param>
        /// <typeparam name="T">The type contained in the array.</typeparam>
        /// <exception cref="InvalidOperationException">thrown if an array is null
        /// or if the indices are out of range.</exception>
        public static void CopyValues<T>(this T[] to, T[] from, int startIndex, int indices, 
            Func<T, T> modifier = null, int offset = 0) {
            if(to == null || to.Length <= startIndex + indices || from == null || startIndex < 0 || 
               from.Length <= startIndex + offset + indices || startIndex + offset < 0) throw new 
                InvalidOperationException("Either the indices went out of bounds or an array was null.");
            for(var i = 0; i < indices; i++) {
                var index = startIndex + i;
                to[index] = modifier is null ? from[index] : modifier(from[index]);
            }
        }

        /// <summary>
        /// This method is used to modify values in an array.
        /// </summary>
        /// <param name="ary">The array whose values you want to modify.</param>
        /// <param name="startIndex">The starting index that you want to fill.</param>
        /// <param name="indices">The number of indices that you want to fill.</param>
        /// <param name="modifier">This method will be applied to the values.</param>
        /// <typeparam name="T">The type contained in the array.</typeparam>
        /// <exception cref="InvalidOperationException">thrown if the array is null
        /// or if the indices are out of range.</exception>
        public static void ModifyValues<T>(this T[] ary, int startIndex, int indices, Func<T, T> modifier) {
            if(ary == null || ary.Length <= startIndex + indices || startIndex < 0) throw new 
                InvalidOperationException("Either the indices went out of bounds or the array was null.");
            for(var i = 0; i < indices; i++) {
                var index = startIndex + i;
                ary[index] = modifier(ary[index]);
            }
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}
