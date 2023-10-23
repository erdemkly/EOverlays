using EOverlays.Editor.ScriptableObjects;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;
namespace Packages.eoverlays.Editor.Overlays
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
            var root = new VisualElement();
            root.style.width = 300;
            var allVisualElements = EOverlayBase.AllVisualElements();
            foreach (var visualElement in allVisualElements)
            {
                var foldOut = new Foldout();
                foldOut.text = visualElement.Value;
                foldOut.Add(visualElement.Key);
                root.Add(foldOut);
            }
            return root;
        }

    }
}