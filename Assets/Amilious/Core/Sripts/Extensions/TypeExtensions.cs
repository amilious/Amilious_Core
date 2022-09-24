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
    /// This class is used to add methods to the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions {
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to split a types name using camel case.
        /// </summary>
        /// <param name="type">The type you want to get a name for.</param>
        /// <returns>The name of the type split based on camel case.</returns>
        public static string SplitCamelCase(this Type type) => type.Name.SplitCamelCase();

        /// <summary>
        /// This method is used to see if a type implements another type.
        /// </summary>
        /// <param name="type">The type that you want to check if it implements <typeparamref name="T"/>.</param>
        /// <typeparam name="T">The type that you want to check if is inherited from.</typeparam>
        /// <returns>True if <paramref name="type"/> inherits <typeparamref name="T"/>.</returns>
        public static bool Implements<T>(this Type type) => typeof(T).IsAssignableFrom(type);

        /// <summary>
        /// This method is used to see if a type implements another type.
        /// </summary>
        /// <param name="type">The type that you want to check if it implements <paramref name="implements"/>.</param>
        /// <param name="implements">The type that you want to check if is inherited from.</param>
        /// <returns>True if <paramref name="type"/> inherits <paramref name="implements"/>.</returns>
        public static bool Implements(this Type type, Type implements) => implements.IsAssignableFrom(type);
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}