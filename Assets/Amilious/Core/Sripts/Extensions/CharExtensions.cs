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
    /// This class is used to add methods to the <see cref="char"/> class.
    /// </summary>
    public static class CharExtensions {

        #region Private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        private static readonly HashSet<char> Vowels = new HashSet<char>{ 'a', 'e', 'i', 'o', 'u' };
        private static readonly HashSet<char> Consonants = new HashSet<char>
            { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to check if the character is a vowel.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>True if the character is a vowel, otherwise false.</returns>
        public static bool IsVowel(this char c) => Vowels.Contains(char.ToLowerInvariant(c));

        /// <summary>
        /// This method is used to check if the character is a consonant.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>True if the character is a consonant, otherwise false.</returns>
        public static bool IsConsonant(this char c) => Consonants.Contains(char.ToLowerInvariant(c));

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}