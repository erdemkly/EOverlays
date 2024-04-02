using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    [Overlay(typeof(SceneView), "E-Overlay")]
    public class EOverlaySceneWindow : Overlay
    {
        private static VisualElement _root;
        private static VisualElement _content;

        public static EOverlaySceneWindow Instance;

        public override VisualElement CreatePanelContent()
        {
            _root.Clear();
            _content.Clear();

            _root.Add(EOverlayVisualElements.RefreshButton());
            _root.Add(EOverlayMethods.navigationBar);
            _root.Add(_content);
            //Get all registered elements
            EOverlayMethods.InvokeSelectTab("");
            return _root;
        }

        private static void SelectTab(string name)
        {
            if (EOverlayMethods.allContents.Count == 0) return;
            var element = EOverlayMethods.allContents.FirstOrDefault(x => x.name == name);
            if (element == null) element = EOverlayMethods.allContents.First();
            _content.Clear();
            _content.Add(element);
        }

        public override void OnCreated()
        {
            base.OnCreated();
            Instance = this;
            EOverlayMethods.SelectTabAction += SelectTab;
            EOverlayMethods.RefreshUI += ReloadPanel;
            _root = new GroupBox()
            {
                style =
                {
                    maxHeight = 400,
                    flexDirection = FlexDirection.Row
                }
            };
            _content = new ScrollView()
            {
                verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible,
                mode = ScrollViewMode.Vertical,
                style =
                {
                    width = 300,
                    maxHeight = 400,
                }
            };
        }

        public override void OnWillBeDestroyed()
        {
            base.OnWillBeDestroyed();

            EOverlayMethods.RefreshUI -= ReloadPanel;
            EOverlayMethods.SelectTabAction -= SelectTab;
        }

        private void ReloadPanel()
        {
            CreatePanelContent();
        }
    }
}