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
using Amilious.Core.Extensions;

namespace Amilious.Core {
    
    /// <summary>
    /// This class is used to add some common and basic unity helper methods.
    /// </summary>
    public static class Amilious {
        
        /// <summary>
        /// This method is used to find a <see cref="GameObject"/> by its name.  This method should not be used in any
        /// update methods.
        /// </summary>
        /// <param name="name">The name of the <see cref="GameObject"/> that you want to find.</param>
        /// <param name="parent">The parent <see cref="GameObject"/> or null for scene root.</param>
        /// <returns>The found <see cref="GameObject"/> or null.</returns>
        public static GameObject FindGameObjectByName(string name, GameObject parent = null) {
            if(parent == null) { // if no parent is given
                var activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                var sceneRoots = activeScene.GetRootGameObjects();
                foreach(var root in sceneRoots) {
                    if(root.name.Equals(name)) return root;
                    var result = FindGameObjectByName(name, root);
                    if(result != null) return result;
                }
                return null;
            }
            if(parent.name.Equals(name)) return parent;
            foreach(Transform child in parent.transform) {
                var result = FindGameObjectByName(name, child.gameObject);
                if(result != null) return result;
            }
            return null;
        }
        
        /// <summary>
        /// This method is used to make a title for a log message.
        /// </summary>
        /// <param name="title">The text that you want in the title.</param>
        /// <returns>The generated title string.</returns>
        public static string MakeTitle(string title) {
            return title.PadText('#', 60, 10).SetColor("ffff88");
        }
        
    }
    
}