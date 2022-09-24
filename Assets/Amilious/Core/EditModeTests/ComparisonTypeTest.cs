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

namespace Amilious.Core.EditModeTests {
    
    /// <summary>
    /// This class is used to test the extension methods from <see cref="ComparisonTypeExtensions"/>.
    /// </summary>
    public class ComparisonTypeTest {

        #region Test Methods ///////////////////////////////////////////////////////////////////////////////////////////
        
        [Test]
        public void Equal() {
            Assert.True(ComparisonType.Equal.Compare(1, 1));
            Assert.False(ComparisonType.Equal.Compare(1, 0));
        }

        [Test]
        public void NotEqual() {
            Assert.True(ComparisonType.NotEqual.Compare(1,0));
            Assert.False(ComparisonType.NotEqual.Compare(1,1));
        }

        [Test]
        public void ApproximatelyEqual() {
            Assert.True(ComparisonType.ApproximatelyEqual.Compare(1,1));
            Assert.True(ComparisonType.ApproximatelyEqual.Compare(1,0,1));
            Assert.True(ComparisonType.ApproximatelyEqual.Compare(-1,0,1));
            Assert.False(ComparisonType.ApproximatelyEqual.Compare(1,0));
            Assert.False(ComparisonType.ApproximatelyEqual.Compare(2,0,1));
        }

        [Test]
        public void LessThan() {
            Assert.True(ComparisonType.LessThan.Compare(0,1));
            Assert.False(ComparisonType.LessThan.Compare(1,0));
            Assert.False(ComparisonType.LessThan.Compare(1,1));
        }

        [Test]
        public void LessThanOrEqual() {
            Assert.True(ComparisonType.LessThanOrEqual.Compare(0,1));
            Assert.False(ComparisonType.LessThanOrEqual.Compare(1,0));
            Assert.True(ComparisonType.LessThanOrEqual.Compare(1,1));
        }

        [Test]
        public void GreaterThan() {
            Assert.True(ComparisonType.GreaterThan.Compare(1,0));
            Assert.False(ComparisonType.GreaterThan.Compare(0,1));
            Assert.False(ComparisonType.GreaterThan.Compare(1,1));
        }

        [Test]
        public void GreaterThanOrEqual() {
            Assert.True(ComparisonType.GreaterThanOrEqual.Compare(1,0));
            Assert.False(ComparisonType.GreaterThanOrEqual.Compare(0,1));
            Assert.True(ComparisonType.GreaterThanOrEqual.Compare(1,1));
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}