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
using System.Collections.Generic;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add extensions to the <see cref="IDictionary{TKey,TValue}"/> class.
    /// </summary>
    public static class DictionaryExtensions {
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method will try to get a value from the dictionary using the provided key then cast
        /// the object as the <see cref="value"/> type.
        /// </summary>
        /// <param name="dictionary">The dictionary the value is in.</param>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The casted value.</param>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <returns>Ture if the value for the given key exists and can be
        /// cast to the provided type, otherwise returns false.</returns>
        public static bool TryGetCastValue<T>(this IDictionary<string, object> dictionary, string key, out T value) {
            if(dictionary == null) {
                value = default(T);
                return false;
            }
            if(dictionary.TryGetValue(key, out var dicValue)) {
                if(dicValue is T value1){ 
                    value = value1;
                    return true;
                }
                try {
                    value = (T) Convert.ChangeType(dicValue, typeof(T));
                    return true;
                }catch(InvalidCastException) {}
            }
            value = default(T);
            return false;
        }
        
        #if UNITY_2019 || UNITY_2020
        
        /// <summary>
        /// This method is used to add the TryAdd method to a dictionary for older versions of C#
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The dictionary key.</param>
        /// <param name="value">The dictionary value.</param>
        /// <typeparam name="T">The dictionary key type.</typeparam>
        /// <typeparam name="T2">The dictionary value type.</typeparam>
        /// <returns>True if the key and value were added to the dictionary, otherwise returns false if
        /// the dictionary already contains the given key.</returns>
        public static bool TryAdd<T, T2>(this Dictionary<T, T2> dictionary, T key, T2 value) {
            if(dictionary.ContainsKey(key)) return false;
            dictionary[key] = value;
            return true;
        }
        
        #endif
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}