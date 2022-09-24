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

using NUnit.Framework;
using Amilious.Core.Extensions;

namespace Amilious.Core.EditModeTests.Extensions {
    
    /// <summary>
    /// This class is used to test the extension methods from <see cref="CharExtensions"/>.
    /// </summary>
    public class CharExtensionTests {

        #region Private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        private static readonly char[] Vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

        private static readonly char[] Consonants = new char[] {
            'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z',
            'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z'
        };

        private static readonly char[] Neither = new char[] { ' ', '.', '@', '#', '*', '\t', '\n' };
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Test Methods ///////////////////////////////////////////////////////////////////////////////////////////
        
        [Test]
        public void IsVowel() {
            foreach(var c in Vowels) Assert.True(c.IsVowel());
            foreach(var c in Consonants) Assert.False(c.IsVowel());
            foreach(var c in Neither) Assert.False(c.IsVowel());
        }

        [Test]
        public void IsConsonant() {
            foreach(var c in Consonants) Assert.True(c.IsConsonant());
            foreach(var c in Vowels) Assert.False(c.IsConsonant());
            foreach(var c in Neither) Assert.False(c.IsConsonant());
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}