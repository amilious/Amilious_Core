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
    /// This method is used to add methods to the <see cref="long"/> struct.
    /// </summary>
    public static class LongExtension {

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to get the base 64 string from a long.
        /// </summary>
        /// <param name="value">The long.</param>
        /// <returns>The base64 string for the long.</returns>
        public static string ToBase64String(this long value) => 
            Convert.ToBase64String(BitConverter.GetBytes(value));

        /// <summary>
        /// This method is used to print a string that containing the number of something in singular or
        /// plural form.
        /// </summary>
        /// <param name="i">The number of something.</param>
        /// <param name="singular">The thing that the number represents in singular form.</param>
        /// <param name="plural">(optional)The thing that the number represents in plural form, if null
        /// the method will try to create the plural form based on the given singular form.</param>
        /// <returns>A string representation of the number and singular or plural label.</returns>
        /// <example>10L.ToSpString("cat") will return "10 cats"</example>
        public static string ToSpString(this long i, string singular, string plural = null) {
            if(string.IsNullOrWhiteSpace(singular)) return i.ToString();
            if(i == 1) return $"{i} {singular}"; //<- return singular string.
            if(plural != null) return $"{i} {plural}"; //<- return given plural string.
            //try generate the plural form.  This will be incorrect for uncountable
            //es
            if(singular.EndsWith("ch") || singular.EndsWith("s") || singular.EndsWith("x") ||
               singular.EndsWith("o") || singular.EndsWith("sh") || singular.EndsWith("z")) {
                return $"{i} {singular}es";
            }
            //ends in f
            if(singular.EndsWith("f")) {
                return $"{i} {singular.Remove(singular.Length - 1, 1)}ves";
            }
            //ends in y
            if(singular.EndsWith("y") && singular.Length > 1 && singular[singular.Length - 2].IsConsonant()) {
                return $"{i} {singular.Remove(singular.Length - 1, 1)}ies";
            }
            //just add an s
            return $"{i} {singular}s";
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}