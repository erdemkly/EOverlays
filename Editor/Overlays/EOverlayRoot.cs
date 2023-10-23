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
            var root = new GroupBox()
            {
                style =
                {
                    width = 300,
                    flexGrow = 1,
                    flexShrink = 1,
                    minHeight = 1000,
                }
            };
            var allVisualElements = EOverlayBase.AllVisualElements();
            foreach ((var visualElement, var name) in allVisualElements)
            {
                var foldOut = root.Q<Foldout>($"{name}");
                foldOut ??= new Foldout()
                {
                    name = name,
                    text = name,
                };
                var groupBox = new GroupBox();
                groupBox.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.53f));
                groupBox.Add(visualElement);
                foldOut.Add(groupBox);
                root.Add(foldOut);
            }
            
            return root;
        }

    }
}
