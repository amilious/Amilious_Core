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
    /// This modifier is used to hide a property if a condition is met.
    /// </summary>
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfModifier : AmiliousPropertyModifier<HideIfAttribute> {

        #region Public Override Methods ////////////////////////////////////////////////////////////////////////////////
        
        /// <inheritdoc />
        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;
        
        /// <inheritdoc />
        public override bool ShouldCancelDraw(SerializedProperty property) =>Hide(property);
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Protected Methods //////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is used to check if the property should be hidden.
        /// </summary>
        /// <param name="property">The property that you want to check.</param>
        /// <returns>True if the property should be hidden, otherwise false.</returns>
        protected bool Hide(SerializedProperty property) {
            var castedAttribute = (HideIfAttribute)attribute;
            if(castedAttribute.ComparisonProperty == null) {
                castedAttribute.ComparisonProperty = 
                    property.FindSiblingProperty(castedAttribute.PropertyName) ??
                    property.serializedObject.FindProperty(castedAttribute.PropertyName) ??
                    property.FindPropertyRelative(castedAttribute.PropertyName);
            }
            var hiderProperty = castedAttribute.ComparisonProperty;
            if(castedAttribute.ComparisonProperty != null) {
                switch(castedAttribute.ComparisonProperty.propertyType) {
                    case SerializedPropertyType.Generic: return false;
                    case SerializedPropertyType.Integer: return castedAttribute.Validate(hiderProperty.intValue);
                    case SerializedPropertyType.Boolean: return castedAttribute.Validate(hiderProperty.boolValue);
                    case SerializedPropertyType.Float: return castedAttribute.Validate(hiderProperty.floatValue);
                    case SerializedPropertyType.String: return castedAttribute.Validate(hiderProperty.stringValue);
                    case SerializedPropertyType.Color: return castedAttribute.Validate(hiderProperty.colorValue);
                    case SerializedPropertyType.ObjectReference:
                        return castedAttribute.Validate(hiderProperty.objectReferenceValue);
                    case SerializedPropertyType.Enum:
                        #if UNITY_2021_1_OR_NEWER
                        return castedAttribute.ValidateEnumValue(hiderProperty.enumValueIndex,
                        hiderProperty.enumValueFlag);
                        #else
                        return castedAttribute.ValidateEnumValue(hiderProperty.enumValueIndex, -1);
                        #endif
                    case SerializedPropertyType.Vector2:
                        return castedAttribute.Validate(hiderProperty.vector2Value);
                    case SerializedPropertyType.Vector3:
                        return castedAttribute.Validate(hiderProperty.vector3Value);
                    case SerializedPropertyType.Vector4:
                        return castedAttribute.Validate(hiderProperty.vector4Value);
                    case SerializedPropertyType.Rect:
                        return castedAttribute.Validate(hiderProperty.rectValue);
                    case SerializedPropertyType.ArraySize:
                        return castedAttribute.Validate(hiderProperty.arraySize);
                    case SerializedPropertyType.Character:
                        return castedAttribute.Validate(hiderProperty.stringValue[0]);
                    case SerializedPropertyType.AnimationCurve:
                        return castedAttribute.Validate(hiderProperty.animationCurveValue);
                    case SerializedPropertyType.Bounds:
                        return castedAttribute.Validate(hiderProperty.boundsValue);
                    case SerializedPropertyType.Quaternion:
                        return castedAttribute.Validate(hiderProperty.quaternionValue);
                    case SerializedPropertyType.ExposedReference:
                        return castedAttribute.Validate(hiderProperty.exposedReferenceValue);
                    case SerializedPropertyType.FixedBufferSize:
                        return castedAttribute.Validate(hiderProperty.fixedBufferSize);
                    case SerializedPropertyType.Vector2Int:
                        return castedAttribute.Validate(hiderProperty.vector2IntValue);
                    case SerializedPropertyType.Vector3Int:
                        return castedAttribute.Validate(hiderProperty.vector3IntValue);
                    case SerializedPropertyType.RectInt:
                        return castedAttribute.Validate(hiderProperty.rectIntValue);
                    case SerializedPropertyType.BoundsInt:
                        return castedAttribute.Validate(hiderProperty.boundsIntValue);
                    #if UNITY_2021_2_OR_NEWER
                    case SerializedPropertyType.ManagedReference:
                        return castedAttribute.Validate(hiderProperty.managedReferenceValue);
                    #endif
                    #if UNITY_2021_1_OR_NEWER
                    case SerializedPropertyType.Hash128:
                        return castedAttribute.Validate(hiderProperty.hash128Value);
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