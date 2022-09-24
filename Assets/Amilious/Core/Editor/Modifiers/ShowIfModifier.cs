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

using UnityEditor;
using Amilious.Core.Attributes;
using Amilious.Core.Editor.Extensions;

namespace Amilious.Core.Editor.Modifiers {
    
    /// <summary>
    /// This modifier is used to show a property only if a condition is met.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfModifier : AmiliousPropertyModifier<ShowIfAttribute> {
        
        #region Public Override Methods ////////////////////////////////////////////////////////////////////////////////
        
        /// <inheritdoc />
        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        /// <inheritdoc />
        public override bool ShouldCancelDraw(SerializedProperty property) {
            var cancel = !Show(property);
            return cancel;
        }

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Protected Methods //////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to check if the property should be shown.
        /// </summary>
        /// <param name="property">The property that you want to check.</param>
        /// <returns>True if the property should be shown, otherwise false.</returns>
        protected bool Show(SerializedProperty property) {
            var castedAttribute = (ShowIfAttribute)attribute;
            if(castedAttribute.ComparisonProperty == null) {
                castedAttribute.ComparisonProperty = 
                    property.FindSiblingProperty(castedAttribute.PropertyName) ??
                    property.serializedObject.FindProperty(castedAttribute.PropertyName)?? 
                    property.FindPropertyRelative(castedAttribute.PropertyName);
            }
            var showerProperty = castedAttribute.ComparisonProperty;
            if(showerProperty != null) {
                switch(showerProperty.propertyType) {
                    case SerializedPropertyType.Generic:
                        return false;
                    case SerializedPropertyType.Integer: return castedAttribute.Validate(showerProperty.intValue);
                    case SerializedPropertyType.Boolean: return castedAttribute.Validate(showerProperty.boolValue);
                    case SerializedPropertyType.Float: return castedAttribute.Validate(showerProperty.floatValue);
                    case SerializedPropertyType.String: return castedAttribute.Validate(showerProperty.stringValue);
                    case SerializedPropertyType.Color: return castedAttribute.Validate(showerProperty.colorValue);
                    case SerializedPropertyType.ObjectReference:
                        return castedAttribute.Validate(showerProperty.objectReferenceValue);
                    case SerializedPropertyType.Enum:
                        #if UNITY_2021_1_OR_NEWER
                        return castedAttribute.ValidateEnumValue(showerProperty.enumValueIndex,
                        showerProperty.enumValueFlag);
                        #else
                        return castedAttribute.ValidateEnumValue(showerProperty.enumValueIndex, -1);
                        #endif
                    case SerializedPropertyType.Vector2: return castedAttribute.Validate(showerProperty.vector2Value);
                    case SerializedPropertyType.Vector3: return castedAttribute.Validate(showerProperty.vector3Value);
                    case SerializedPropertyType.Vector4: return castedAttribute.Validate(showerProperty.vector4Value);
                    case SerializedPropertyType.Rect: return castedAttribute.Validate(showerProperty.rectValue);
                    case SerializedPropertyType.ArraySize: return castedAttribute.Validate(showerProperty.arraySize);
                    case SerializedPropertyType.Character:
                        return castedAttribute.Validate(showerProperty.stringValue[0]);
                    case SerializedPropertyType.AnimationCurve:
                        return castedAttribute.Validate(showerProperty.animationCurveValue);
                    case SerializedPropertyType.Bounds: return castedAttribute.Validate(showerProperty.boundsValue);
                    case SerializedPropertyType.Quaternion:
                        return castedAttribute.Validate(showerProperty.quaternionValue);
                    case SerializedPropertyType.ExposedReference:
                        return castedAttribute.Validate(showerProperty.exposedReferenceValue);
                    case SerializedPropertyType.FixedBufferSize:
                        return castedAttribute.Validate(showerProperty.fixedBufferSize);
                    case SerializedPropertyType.Vector2Int:
                        return castedAttribute.Validate(showerProperty.vector2IntValue);
                    case SerializedPropertyType.Vector3Int:
                        return castedAttribute.Validate(showerProperty.vector3IntValue);
                    case SerializedPropertyType.RectInt:
                        return castedAttribute.Validate(showerProperty.rectIntValue);
                    case SerializedPropertyType.BoundsInt:
                        return castedAttribute.Validate(showerProperty.boundsIntValue);
                    #if UNITY_2021_2_OR_NEWER
                    case SerializedPropertyType.ManagedReference:
                        return castedAttribute.Validate(showerProperty.managedReferenceValue);
                    #endif
                    #if UNITY_2021_1_OR_NEWER
                    case SerializedPropertyType.Hash128:
                        return castedAttribute.Validate(showerProperty.hash128Value);
                    #endif
                    default:
                        return false;
                }
            }

            var field = property.serializedObject?.GetType().GetField(castedAttribute.PropertyName);
            if(field != null) { return castedAttribute.Validate(field.GetValue(property.serializedObject.context)); }
            var prop = property.serializedObject?.GetType().GetProperty(castedAttribute.PropertyName);
            if(prop != null) { return castedAttribute.Validate(prop.GetValue(property.serializedObject.context)); }
            var method = property.serializedObject?.GetType().GetMethod(castedAttribute.PropertyName);
            if(method == null || method.GetParameters().Length >= 1 || method.ReturnParameter == null) return false;
            var result = method.Invoke(property.serializedObject.context, null);
            return castedAttribute.Validate(result);
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}