using System.Collections.Generic;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add methods to the Char class.
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