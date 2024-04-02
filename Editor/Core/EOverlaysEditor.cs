using System;
using System.Collections.Generic;
using System.Linq;
using EOverlays.Editor.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    public class EOverlaysEditor : EditorWindow
    {
        private static VisualElement _content;
        private static HashSet<VisualElement> _allContents;
        private static VisualElement _navigationBar;
        private static string _selectedNavigationElement;
        private static Action<string> _selectTabAction;

        private void OnEnable()
        {
            Selection.selectionChanged += SelectionChanged;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= SelectionChanged;
        }

        private void SelectionChanged()
        {
            CreateGUI();
        }

        [MenuItem("Window/EOverlays/Show")]
        public static void ShowExample()
        {
            EOverlaysEditor wnd = GetWindow<EOverlaysEditor>();
            wnd.titleContent = new GUIContent("E-Overlays Window");
        }


        public void CreateGUI()
        {
            rootVisualElement.style.flexDirection = FlexDirection.Row;
            _content = new ScrollView()
            {
                verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible,
                mode = ScrollViewMode.Vertical,
                style =
                {
                    flexGrow = 1
                }
            };
            _navigationBar = EOverlayVisualElements.NavigationBar(Repaint);
            _allContents = new HashSet<VisualElement>();


            rootVisualElement.Add(_navigationBar);

            rootVisualElement.Add(_content);

            //Get all registered elements
            Dictionary<VisualElement, EOverlayElementAttribute> allVisualElements = EOverlayMethods.AllVisualElements;


            foreach (var (visualElement, attribute) in allVisualElements)
            {
                var name = attribute.Name;
                var groupBox = _allContents.FirstOrDefault(x => x.name == name);
                groupBox ??= new GroupBox()
                {
                    name = name,
                };
                groupBox.style.backgroundColor = new StyleColor(new Color(0.15f, 0.15f, 0.15f, 0.53f));
                groupBox.Add(visualElement);

                _allContents.Add(groupBox);


                var navigationButton = rootVisualElement.Q<Button>($"{name}");
                if (navigationButton != null) continue;
                navigationButton = new Button(() => SelectTab(name))
                {
                    name = name,
                    text = name,
                };

                _selectTabAction += (n) =>
                {
                    navigationButton.SetEnabled(n != name);
                    navigationButton.style.textShadow = new StyleTextShadow(new TextShadow
                        { blurRadius = 10, color = Color.black, offset = new Vector2(10, 10) });
                    navigationButton.style.color = n == name ? Color.green : Color.white;
                };
                _navigationBar.Add(navigationButton);
            }

            SelectTab(_selectedNavigationElement);
        }

        private static void SelectTab(string name)
        {
            if (_allContents.Count == 0) return;
            var element = _allContents.FirstOrDefault(x => x.name == name);
            if (element == null) element = _allContents.First();
            _content.Clear();
            _content.Add(element);
            _selectedNavigationElement = name;
            _selectTabAction?.Invoke(element.name);
        }
    }
}