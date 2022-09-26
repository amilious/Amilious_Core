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
using System.Text;
using UnityEngine;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Amilious.Core.Extensions {
    
    /// <summary>
    /// This class is used to add methods to the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions {

        #region Fields /////////////////////////////////////////////////////////////////////////////////////////////////
        
        private static readonly Queue<StringBuilder> StringBuilders = new Queue<StringBuilder>(MAX_STRING_BUILDERS);
        private const int MAX_STRING_BUILDERS = 20;
        public const string APPEND_C_FORMAT = "<color=#{0}>{1}</color>";
        public const string PADDING_FORMAT = "{0}{1} {2}";
        public const string LINK_FORMAT = "<link=\"{0}\">{1}</link>";
        #if UNITY_2019 || UNITY_2020
        private static readonly string[] Splitter = new string[1];
        #endif
        /// <summary>
        /// This regex is used for parsing command line arguments.
        /// </summary>
        private static readonly Regex ArgumentParser = new Regex(
            @"(?=\S)[^""\s]*(?:""[^\\""]*(?:\\[\s\S][^\\""]*)*""[^""\s]*)*");
        
        /// <summary>
        /// This regex is used for parsing numbers out of a string.
        /// </summary>
        private static readonly Regex NumberParser = new Regex(@"([+-]?([0-9]+\.?[0-9]*|\.[0-9]+))");
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to rent out a string builder.
        /// </summary>
        /// <returns>A string builder.</returns>
        private static StringBuilder RentStringBuilder() =>
            StringBuilders.TryDequeue(out var builder) ? builder.Clear() : new StringBuilder();

        /// <summary>
        /// This method is used to return a string builder to the queue.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        private static void ReturnStringBuilder(StringBuilder builder) {
            if(StringBuilders.Count > MAX_STRING_BUILDERS) return;
            StringBuilders.Enqueue(builder.Clear());
        }
        
        /// <summary>
        /// This method is used to add spaces to a string based on capital letters.
        /// </summary>
        /// <param name="value">The value that you want to spit based on camel case.</param>
        /// <param name="spitNumbers">If true numbers will be spit as well.</param>
        /// <returns>The newly formatted string split using camel case.</returns>
        public static string SplitCamelCase(this string value, bool spitNumbers = false) {
            var sb = RentStringBuilder();
            try{
                var chars = value.ToCharArray();
                for(var i = 0; i < chars.Length; i++) {
                    var c = chars[i];
                    if(char.IsDigit(c) && i != 0 && !char.IsDigit(chars[i - 1]) && spitNumbers) sb.Append(' ');
                    else if(char.IsUpper(c) && i != 0 && !char.IsUpper(chars[i - 1])) sb.Append(' ');
                    if(i == 0) c = char.ToUpper(c);
                    sb.Append(c);
                }
                return sb.ToString();
            }finally{ ReturnStringBuilder(sb); }
        }

        /// <summary>
        /// This method is used to format a string with the given color.
        /// </summary>
        /// <param name="text">The text that you want to apply the color formatting to.</param>
        /// <param name="color">The color that you want to apply to the given text.</param>
        /// <returns>The string formatted in the given color.</returns>
        public static string SetColor(this string text, string color) {
            color = color.TrimStart(' ', '#');
            return string.Format(APPEND_C_FORMAT, color, text);
        }

        /// <summary>
        /// This method is used to format a string with the given color.
        /// </summary>
        /// <param name="text">The text that you want to apply the color formatting to.</param>
        /// <param name="color">The color that you want to apply to the given text.</param>
        /// <returns>The string formatted in the given color.</returns>
        public static string SetColor(this string text, Color color) {
            return string.Format(APPEND_C_FORMAT, color.HtmlRGBA(), text);
        }

        /// <summary>
        /// This method is used to make the given text a link.
        /// </summary>
        /// <param name="str">The string that you want to make a link.</param>
        /// <param name="id">The id or id segments for the link.</param>
        /// <returns>The string as a link.</returns>
        public static string MakeLink(this string str, params string[] id) {
            if(id == null || id.Length == 0) return $"<link>{str}</link>";
            if(id.Length == 1) return string.Format(LINK_FORMAT, id, str);
            var sb = RentStringBuilder().Clear();
            sb.Append("<link=\"");
            sb.Append(id[0]);
            for(var i = 1; i < id.Length; i++) sb.Append('|').Append(id[i]);
            sb.Append("\">").Append(str).Append("</link>");
            var result = sb.ToString();
            ReturnStringBuilder(sb);
            return result;
        }

        /// <summary>
        /// This method is used to pad a text with a character on both sides of a string.
        /// </summary>
        /// <param name="text">The text that you want to pad with a character.</param>
        /// <param name="character">The padding character.</param>
        /// <param name="length">The total length to make the text.</param>
        /// <param name="startLength">The start padding length.</param>
        /// <returns>The padded text.</returns>
        public static string PadText(this string text, char character, int length, int startLength = 0) {
            var builder = RentStringBuilder();
            try {
                if(startLength > 0) {
                    builder.Append(character, startLength);
                    builder.Append(' ');
                }
                builder.Append(text.ToUpperInvariant());
                if(builder.Length + 1 >= length) return builder.ToString();
                builder.Append(' ');
                builder.Append(character, length - builder.Length);
                return builder.ToString();
            }
            finally { ReturnStringBuilder(builder); }
        }

        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str) => GUIUtility.systemCopyBuffer = str;
        
        /// <summary>
        /// This method is used to get the command text from a string.
        /// </summary>
        /// <param name="str">The string containing the command text.</param>
        /// <returns>The command text.</returns>
        public static string GetCommandText(this string str) => ArgumentParser.Match(str).Value;
        
        /// <summary>
        /// This method is used to get the arguments from the string.
        /// </summary>
        /// <param name="str">The input text that you want to get the
        /// arguments from.</param>
        /// <returns>A string array of the found parameters or null.</returns>
        public static IEnumerable<string> GetArguments(this string str) {
            //process the text
            var first = true;
            foreach(Match m in ArgumentParser.Matches(str)){
                if(first){ first = false; continue; }
                yield return m.Value.TrimStart('"').TrimEnd('"').Replace("\\\"", "\"");
            }
        }

        /// <summary>
        /// This method is used to read the parameter at the given index to the end of the string.
        /// </summary>
        /// <param name="str">The command string.</param>
        /// <param name="index">The parameter index.</param>
        /// <param name="value">The parameter string.</param>
        /// <returns>True if able to get the parameter for the given index, otherwise false.</returns>
        public static bool TryReadToEndParameter(this string str, int index, out string value) {
            //process the text
            var first = true;
            var param = -2;
            foreach(Match m in ArgumentParser.Matches(str)) {
                param++;
                if(first){ first = false; continue; }
                if(param != index) continue;
                //read to end
                value = str.Substring(m.Index).TrimStart('"').TrimEnd('"').Replace("\\\"", "\"");
                return true;
            }
            value = string.Empty;
            return false;
        }
        
        /// <summary>
        /// This method is used to try parse floats from a string.
        /// </summary>
        /// <param name="str">The string you want to parse floats from.</param>
        /// <param name="numberStyles">The number style that you want to use to try parse the floats.</param>
        /// <param name="cultureInfo">The culture info that you want to use to try parse the floats.</param>
        /// <param name="numbers">The parsed floats.</param>
        /// <returns>True if the string contains floats, otherwise false.</returns>
        public static bool TryGetFloats(this string str, NumberStyles numberStyles, CultureInfo cultureInfo, out float[] numbers ) {
            var values = new List<float>();
            foreach(Match m in NumberParser.Matches(str)){
                if(float.TryParse(m.Value, numberStyles, cultureInfo, out var f)) values.Add(f);
            }
            numbers = values.ToArray();
            return values.Count > 0;
        }
        
        /// <summary>
        /// This method is used to try parse ints from a string.
        /// </summary>
        /// <param name="str">The string you want to parse ints from.</param>
        /// <param name="numberStyles">The number style that you want to use to try parse the ints.</param>
        /// <param name="cultureInfo">The culture info that you want to use to try parse the ints.</param>
        /// <param name="numbers">The parsed ints.</param>
        /// <returns>True if the string contains ints, otherwise false.</returns>
        public static bool TryGetInts(this string str, NumberStyles numberStyles, CultureInfo cultureInfo, out int[] numbers) {
            var values = new List<int>();
            foreach(Match m in NumberParser.Matches(str)){
                if(int.TryParse(m.Value,numberStyles, cultureInfo,out var f)) values.Add(f);
            }
            numbers = values.ToArray();
            return values.Count > 0;
        }
        
        #if UNITY_2019 || UNITY_2020
        
        /// <summary>
        /// This method is used to add the split by string option to string.Split for older versions of C#.
        /// </summary>
        /// <param name="str">The string that you want to split.</param>
        /// <param name="separator">The string separator.</param>
        /// <returns>The split string.</returns>
        public static string[] Split(this string str, string separator) {
            Splitter[0] = separator;
            return str.Split(Splitter, StringSplitOptions.None);
        }
        #endif
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}