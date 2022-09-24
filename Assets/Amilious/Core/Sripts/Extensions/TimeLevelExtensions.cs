using System;

namespace Amilious.Core.Extensions {
    
    public static class TimeLevelExtensions {
        
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
        
        public static bool NeedAndBefore(this TimeLevel timeLevel, TimeSpan timeSpan, TimeLevel lowest = TimeLevel.Millisecond) {
            var count = 0;
            var level = lowest;
            while(level<=timeLevel) {
                if(timeSpan.Get(level) > 0) count++;
                level++;
            }
            return count == 1;
        }
        
    }
}