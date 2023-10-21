using EOverlays.Editor.Attributes;
using UnityEditor.Overlays;
using UnityEngine.UIElements;
namespace EOverlays.Editor.Overlays
{
    public class PlayerPrefsOverlay : EOverlayBase
    {
        [EOverlayElement()]
        public static VisualElement CreateOverlay()
        {
            var root = new VisualElement();
            root.Add(new Label("Player Prefs"));
            return root;
        }
    }public class PlayerPrefsOverlay2 : EOverlayBase
    {
        [EOverlayElement(11)]
        public static VisualElement CreateOverlay()
        {
            var root = new VisualElement();
            root.Add(new Label("Player Prefs 2"));
            return root;
        }
    }
}
