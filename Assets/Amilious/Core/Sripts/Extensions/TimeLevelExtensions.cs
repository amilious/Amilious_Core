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
    /// This class is used to add extension methods to the <see cref="TimeLevel"/> enum.
    /// </summary>
    public static class TimeLevelExtensions {
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to get a label for the time level.
        /// </summary>
        /// <param name="timeLevel">The time level that you want to get a label for.</param>
        /// <param name="num">The number value for the given level.</param>
        /// <param name="abbreviations">If true abbreviations will be used.</param>
        /// <returns>A label for the given time level.</returns>
        public static string EasyStringLabel(this TimeLevel timeLevel, int num, bool abbreviations = false) {
            switch(timeLevel) {
                case TimeLevel.Millisecond: return abbreviations ? "ms" : num == 1 ? "millisecond" : "milliseconds";
                case TimeLevel.Second: return abbreviations ? num == 1 ? "sec" : "secs" : num == 1 ? "second" : "seconds";
                case TimeLevel.Minute: return abbreviations ? num == 1 ? "min" : "mins" : num == 1 ? "minute" : "minutes";
                case TimeLevel.Hour: return abbreviations ? num == 1 ? "hr" : "hrs" : num == 1 ? "hour" : "hours";
                case TimeLevel.Day: return num == 1 ? "day" : "days";
                default: return "";
            }
        }
        
        public static bool NeedAndBefore(this TimeLevel timeLevel, TimeSpan timeSpan, 
            TimeLevel lowest = TimeLevel.Millisecond) {
            var count = 0;
            var level = lowest;
            while(level<=timeLevel) {
                if(timeSpan.Get(level) > 0) count++;
                level++;
            }
            return count == 1;
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}