using System;
using System.Text;

namespace Amilious.Core.Extensions {
    
    public static class TimeSpanExtensions {
        
        #region Private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to create an easy to read string from the time span.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="lowestLevel">The lowest level that you want displayed in the string.</param>
        /// <param name="abbreviations">If true abbreviations will be used.</param>
        /// <returns>An easy to read string for the give time span.</returns>
        public static string ToEasyReadString(this TimeSpan timeSpan, TimeLevel lowestLevel = TimeLevel.Millisecond,
            bool abbreviations = false) {
            StringBuilder.Clear();
            var level = TimeLevel.Day;
            while(true) {
                if(timeSpan.Get(level) != 0) {
                    //only add the value if it is not zero
                    if(StringBuilder.Length != 0) {
                        StringBuilder.Append(' ');
                        if(level.NeedAndBefore(timeSpan, lowestLevel)) StringBuilder.Append("and ");
                    }
                    var val = timeSpan.Get(level);
                    StringBuilder.AppendFormat("{0} {1}",val, level.EasyStringLabel(val,abbreviations));
                }
                if(level == lowestLevel) {
                    if(StringBuilder.Length == 0) 
                        StringBuilder.AppendFormat("{0} {1}",0, level.EasyStringLabel(0,abbreviations));
                    break;
                }
                level--;
            }
            return StringBuilder.ToString();
        }
        
        /// <summary>
        /// This method is used to get a value from a time span
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="timeLevel">The level you want to get the value of.</param>
        /// <returns>The value of the time span at the given level. </returns>
        public static int Get(this TimeSpan timeSpan, TimeLevel timeLevel) {
            switch(timeLevel) {
                case TimeLevel.Millisecond: return timeSpan.Milliseconds;
                case TimeLevel.Second: return timeSpan.Seconds;
                case TimeLevel.Minute: return timeSpan.Minutes;
                case TimeLevel.Hour: return timeSpan.Hours;
                case TimeLevel.Day: return timeSpan.Days;
                default: return -1;
            }
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}