using System;
using System.Collections.Generic;
using System.Linq;
using EOverlays.Editor.Attributes;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    [Overlay(typeof(SceneView), "E-Overlay", true)]
    public class EOverlayRoot : Overlay
    {
        private static VisualElement _root;
        private static VisualElement _content;
        private static HashSet<VisualElement> _allContents;
        private static VisualElement _navigationBar;
        private static string _selectedNavigationElement;
        private static Action<string> _selectTabAction;

        public override VisualElement CreatePanelContent()
        {
            _root.Clear();

            _root.Add(EOverlayVisualElements.RefreshButton());
            _content.Clear();
            _allContents = new HashSet<VisualElement>();


            _root.Add(_navigationBar);

            _root.Add(_content);

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


                var navigationButton = _root.Q<Button>($"{name}");
                if (navigationButton != null) continue;
                navigationButton = new Button(() => SelectTab(name))
                {
                    name = name,
                    text = name,
                };

                _selectTabAction += (n) =>
                {
                    navigationButton.SetEnabled(n != name);
                    navigationButton.style.color = n == name ? Color.green : Color.white;
                };
                _navigationBar.Add(navigationButton);
            }

            SelectTab(_selectedNavigationElement);
            return _root;
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

        public override void OnCreated()
        {
            base.OnCreated();
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
            _navigationBar = EOverlayVisualElements.NavigationBar();
        }

        public override void OnWillBeDestroyed()
        {
            base.OnWillBeDestroyed();

            EOverlayMethods.RefreshUI -= ReloadPanel;
        }

        private void ReloadPanel()
        {
            CreatePanelContent();
        }
    }
}