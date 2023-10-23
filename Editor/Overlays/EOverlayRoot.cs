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
            foreach ((var visualElement, var name) in allVisualElements)
            {
                var foldOut = root.Q<Foldout>($"{name}");
                foldOut ??= new Foldout();
                foldOut.name = name;
                foldOut.text = name;
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
