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

using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;
#endif

namespace Amilious.Core.Input {
    
    /// <summary>
    /// This class is used to handle input using both the old and new input systems.
    /// </summary>
    public static class InputHelper {
        
        #region private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        #if ENABLE_INPUT_SYSTEM
        private static Dictionary<KeyCode, Key> _lookup;
        #endif

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to get the pointer position using both the old and new input systems.
        /// </summary>
        public static Vector2 PointerPosition {
            get {
                #if ENABLE_INPUT_SYSTEM
                return Mouse.current.position.ReadValue();
                #else
                return UnityEngine.Input.mousePosition;
                #endif
            }
        }
        
        /// <summary>
        /// This method is used to check if a key is pressed using both the old and new input systems.
        /// </summary>
        /// <param name="key">The key that you want to check.</param>
        /// <returns>True if the key is pressed, otherwise false.</returns>
        public static bool GetKey(KeyCode key) {
            #if ENABLE_LEGACY_INPUT_MANAGER
            return UnityEngine.Input.GetKey(key);
            #else
            BuildKeyLookup();
            return Keyboard.current[_lookup[key]].isPressed;
            #endif
        }
        
        #endregion
        
        #region Private Methods ////////////////////////////////////////////////////////////////////////////////////////
        
        #if ENABLE_INPUT_SYSTEM
        private static void BuildKeyLookup() {
            if(_lookup != null) return;
            _lookup = new Dictionary<KeyCode, Key>();
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode))) {
                var textVersion = keyCode.ToString();
                if (Enum.TryParse<Key>(textVersion, true, out var value)) _lookup[keyCode] = value;
            }
            _lookup[KeyCode.Return] = Key.Enter;
            _lookup[KeyCode.KeypadEnter] = Key.NumpadEnter;
        }
        #endif

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}