namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add methods to the <see cref="int"/> class.
    /// </summary>
    public static class IntExtensions {

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
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
        public static string ToSpString(this int i, string singular, string plural = null) {
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