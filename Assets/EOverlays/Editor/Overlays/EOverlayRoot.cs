using System;
using System.Linq;
using System.Reflection;
using EOverlays.Editor.Attributes;
using EOverlays.Editor.ScriptableObjects;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;
namespace EOverlays.Editor.Overlays
{
    [Overlay(typeof(SceneView), "E-Overlay", true)]
    public class EOverlayRoot : Overlay
    {
        private EOverlaysSettings _settings;
        private EOverlaysSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Resources.Load<EOverlaysSettings>("EOverlays Settings");
                }
                return _settings;
            }
        }
        public override VisualElement CreatePanelContent()
        {
            FetchAllOverlays();
            var root = new VisualElement();
            var allVisualElements = EOverlayBase.AllVisualElements();
            foreach (var visualElement in allVisualElements)
            {
                root.Add(visualElement);
            }
            return root;
        }

        private void FetchAllOverlays()
        {
            EOverlayBase.AllVisualElements();
        }
    }
}
