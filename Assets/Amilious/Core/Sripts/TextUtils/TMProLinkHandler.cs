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

using TMPro;
using UnityEngine;
using Amilious.Core.Input;
using Amilious.Core.Extensions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Amilious.Core.TextUtils {
    
    /// <summary>
    /// This component is used to handle links within text mesh pro text.
    /// </summary>
    [AddComponentMenu("Amilious/Text/TMPro Link Handler")]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TMProLinkHandler : AmiliousBehavior, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

        #region Private Fields /////////////////////////////////////////////////////////////////////////////////////////
        
        private TextMeshProUGUI _tmpText;
        private Canvas _canvas;
        private Camera _camera;

        // Flags
        private bool _isHoveringObject;
        private int _selectedLink = -1;
        private readonly Dictionary<int, Color32> _colors = new Dictionary<int, Color32>();
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Delegates //////////////////////////////////////////////////////////////////////////////////////////////
        
        public delegate void LinkHoverDelegate(string text, string[] id);
        public delegate void LinkClickDelegate(PointerEventData clickEvent, string text, string[] id);
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Events /////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This event is triggered when the used starts hovering over a link.
        /// </summary>
        public static event LinkHoverDelegate OnLinkEnter;
        
        /// <summary>
        /// This event is triggered when the user stops hovering over a link.
        /// </summary>
        public static event LinkHoverDelegate OnLinkExit;
        
        /// <summary>
        /// This event is triggered when the user clicks on a link.
        /// </summary>
        public static event LinkClickDelegate OnLinkClick;
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Handler Methods ////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// This method is called when the pointer enters this game object.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) => _isHoveringObject = true;

        /// <summary>
        /// This method is called when the pointer exits this game object.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => _isHoveringObject = false;

        /// <summary>
        /// This method is called when the pointer clicks on the screen.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
            var linkId = TMP_TextUtilities.FindIntersectingLink(_tmpText, eventData.pressPosition, _camera);
            //check if the link is valid
            if(linkId < 0) return;
            var linkInfo = _tmpText.textInfo.linkInfo[linkId];
            OnLinkClick?.Invoke(eventData,linkInfo.GetLinkText(),linkInfo.GetLinkID().Split('|'));
        }
        
        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Monobehavior Methods ///////////////////////////////////////////////////////////////////////////////////
        
        private void Awake() {
            _tmpText = gameObject.GetComponent<TextMeshProUGUI>();
            _canvas = gameObject.GetComponentInParent<Canvas>();
            // Get a reference to the camera if Canvas Render Mode is not ScreenSpace Overlay.
            _camera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
        }

        private void OnEnable() {
            // Subscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTmpTextChanged);
        }

        private void OnDisable() {
            // UnSubscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTmpTextChanged);
        }

        private void LateUpdate() {
            if(!_isHoveringObject&&_selectedLink==-1) return;
            
            //Check if Mouse intersects any words and if so assign a random color to that word.
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_tmpText, InputHelper.PointerPosition, _camera);

            // Clear previous word selection.
            if (_selectedLink != -1 && (linkIndex == -1 || linkIndex != _selectedLink)) {
                if(ModifyLinkTint(_selectedLink, 1.33333f, out var info, true)) {
                    OnLinkExit?.Invoke(info.GetLinkText(),info.GetLinkID().Split('|'));
                    _selectedLink = -1;
                }
            }

            // Word Selection Handling
            if (linkIndex != -1 && linkIndex != _selectedLink) {
                _selectedLink = linkIndex;
                if(ModifyLinkTint(_selectedLink, 0.75f, out var info)) {
                    OnLinkEnter?.Invoke(info.GetLinkText(),info.GetLinkID().Split('|'));
                }
            }

        }

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
                   
        #region Private Methods ////////////////////////////////////////////////////////////////////////////////////////
        
        private void OnTmpTextChanged(Object obj) {
            if(obj != _tmpText) return;
            _selectedLink = -1;
        }
        
        private bool ModifyLinkTint(int linkIndex, float tint, out TMP_LinkInfo info, bool restoreColors = false) {
            info = default;
            //check if link id is valid
            if(linkIndex == -1 || linkIndex >= _tmpText.textInfo.linkInfo.Length) return false;
            //get the link info
            info = _tmpText.textInfo.linkInfo[linkIndex];
            // Iterate through each of the characters of the word.
            for (var i = 0; i < info.linkTextLength; i++) {
                var charIndex = info.linkTextfirstCharacterIndex + i;
                if(_tmpText.textInfo.characterInfo[charIndex].character == ' ') continue;
                var meshIndex = _tmpText.textInfo.characterInfo[charIndex].materialReferenceIndex;
                var vertexIndex = _tmpText.textInfo.characterInfo[charIndex].vertexIndex;
                var vertexColors = _tmpText.textInfo.meshInfo[meshIndex].colors32;
                if(!restoreColors) _colors[charIndex] = vertexColors[vertexIndex];
                vertexColors.ModifyValues(vertexIndex,4,color=>restoreColors?_colors[charIndex]:color.Tint(tint));
            }
            // Update Geometry
            _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            return true;
        }

        #endregion /////////////////////////////////////////////////////////////////////////////////////////////////////
        
    }
    
}