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
    /// This class is used to test the extension methods from <see cref="ArrayExtensions"/>.
    /// </summary>
    public class ArrayExtensionsTest {

        #region Test Methods ///////////////////////////////////////////////////////////////////////////////////////////
        
        [Test]
        public void Modify() {
            var ary = new int[] { 1, 1, 1, 1, 1 };
            ary.ModifyValues(1,3,x=>x+1);
            Assert.AreEqual(1,ary[0]);
            Assert.AreEqual(2,ary[1]);
            Assert.AreEqual(2,ary[2]);
            Assert.AreEqual(2,ary[3]);
            Assert.AreEqual(1,ary[4]);
        }

        [Test]
        public void Fill() {
            var ary = new int[5];
            ary.Fill(5,0,5);
            Assert.AreEqual(5,ary[0]);
            Assert.AreEqual(5,ary[1]);
            Assert.AreEqual(5,ary[2]);
            Assert.AreEqual(5,ary[3]);
            Assert.AreEqual(5,ary[4]);
        }

        [Test]
        public void CopyValues() {
            var ary1 = new int[] { 1, 1, 1, 1, 1 };
            var ary2 = new int[] { 2, 2, 2, 2, 2 };
            ary1.CopyValues(ary2, 1, 3);
            Assert.AreEqual(1,ary1[0]);
            Assert.AreEqual(2,ary1[1]);
            Assert.AreEqual(2,ary1[2]);
            Assert.AreEqual(2,ary1[3]);
            Assert.AreEqual(1,ary1[4]);
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}