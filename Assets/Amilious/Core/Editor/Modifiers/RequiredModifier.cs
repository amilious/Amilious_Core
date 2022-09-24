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
using UnityEditor;
using Amilious.Core.Attributes;

namespace Amilious.Core.Editor.Modifiers {
    
    /// <summary>
    /// This modifier is used to display a warning message if an inspector field is left blank.
    /// </summary>
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredModifier : AmiliousPropertyModifier<RequiredAttribute> {

        #region Private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        private RequiredAttribute _attribute;
        private bool _displayedWarning;

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Properties /////////////////////////////////////////////////////////////////////////////////////////////
        
        public RequiredAttribute RequiredAttribute {
            get { return _attribute ?? (_attribute = (RequiredAttribute)attribute); }
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Public Methods /////////////////////////////////////////////////////////////////////////////////////////
        
        /// <inheritdoc />
        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;
        
        /// <inheritdoc />
        public override void BeforeOnGUI(SerializedProperty property, GUIContent label, bool hidden) {
            if(ShowMessage(property)) {
                EditorGUILayout.HelpBox(RequiredAttribute.Message, MessageType.Error);
                if(_displayedWarning) return;
                _displayedWarning = true;
                //select the property in the inspector
                GUI.FocusControl(property.name);
                //display warning
                Debug.LogWarningFormat("The {0} field is required but not present!\n{1}", property.name, 
                    RequiredAttribute.Message);
            }
            else _displayedWarning = false;
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Protected Methods //////////////////////////////////////////////////////////////////////////////////////
        
        protected bool ShowMessage(SerializedProperty property) {
            if(property == null) return true;
            switch(property.propertyType) {
                case SerializedPropertyType.String: return string.IsNullOrWhiteSpace(property.stringValue);
                case SerializedPropertyType.ObjectReference: return property.objectReferenceValue == null;
                case SerializedPropertyType.Character: return string.IsNullOrEmpty(property.stringValue);
                case SerializedPropertyType.AnimationCurve: return property.animationCurveValue == null;
                #if UNITY_2021_2_OR_NEWER
                case SerializedPropertyType.ManagedReference: return property.managedReferenceValue == null;
                #else
                case SerializedPropertyType.ManagedReference: return property.objectReferenceValue == null;
                #endif
                default: return false;
            }
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}