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
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Amilious.Core.Editor {

    /// <summary>
    /// Defines the ODIN_INSPECTOR symbol.
    /// </summary>
    public static class Defines {

        #region Private Static Fields //////////////////////////////////////////////////////////////////////////////////

        private static readonly string[] RemoveSymbols = { };
        
        private static readonly string[] DefineSymbols = {
            "AMILIOUS_CORE", "AMILIOUS_CORE_1_0", "AMILIOUS_CORE_1_0_OR_NEWER"
        };
        
        private static readonly StringBuilder StringBuilder = new StringBuilder();
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to make sure that the given define symbols are present.
        /// </summary>
        /// <param name="name">The name of the project the define symbols are for.</param>
        /// <param name="defineSymbols">The define symbols that you want to be present.</param>
        /// <returns>The number of added define symbols.</returns>
        public static int TryAdd(string name, params string[] defineSymbols) {
            var added = 0;
            var currentTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            if(currentTarget == BuildTargetGroup.Unknown) return added;
            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentTarget).Trim();
            var defines = definesString.Split(';');
            var changed = false;
            foreach(var define in defineSymbols) {
                if(defines.Contains(define)) continue;
                if(!definesString.EndsWith(";", StringComparison.InvariantCulture)) definesString += ";";
                definesString += define;
                changed = true;
                added++;
            }
            if(!changed) return added;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentTarget, definesString);
            Debug.Log(Amilious.MakeTitle($"Added {added} {name} Define Symbols"));
            return added;
        }

        /// <summary>
        /// This method is used to make sure that the give define symbols are not present.
        /// </summary>
        /// <param name="name">The name of the project the define symbols are for.</param>
        /// <param name="defineSymbols">The define symbols that you want to not be present.</param>
        /// <returns>The number of removed define symbols.</returns>
        public static int TryRemove(string name, params string[] defineSymbols) {
            var removed = 0;
            var currentTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            if(currentTarget == BuildTargetGroup.Unknown) return removed;
            StringBuilder.Clear();
            var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(currentTarget).Trim();
            var defines = definesString.Split(';');
            var changed = false;
            foreach(var define in defines) {
                if(defineSymbols.Contains(define)) {
                    changed = true;
                    removed++;
                    continue;
                }
                if(StringBuilder.Length > 0) StringBuilder.Append(';');
                StringBuilder.Append(define);
            }
            if(!changed) return removed;
            PlayerSettings.SetScriptingDefineSymbolsForGroup(currentTarget, StringBuilder.ToString());
            Debug.Log(Amilious.MakeTitle($"Removed {removed} {name} Define Symbols"));
            return removed;
        }

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Private Methods ////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to add the amilious core defines.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void AddCoreDefineSymbols() {
            TryRemove("Amilious Core", RemoveSymbols);
            TryAdd("Amilious Core", DefineSymbols);
        }

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}
