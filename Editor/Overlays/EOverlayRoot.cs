using System;
using System.Collections.Generic;
using System.Linq;
using Editor.Attributes;
using EOverlays.Editor.ScriptableObjects;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;
namespace Editor.Overlays
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
        private static VisualElement _root;
        private static VisualElement _content;
        private static HashSet<VisualElement> _allContents;
        private static VisualElement _navigationBar;
        private static string _selectedNavigationElement;
        private static Action<string> _selectTabAction;
        public override VisualElement CreatePanelContent()
        {
            _root.Clear();
            _content.Clear();
            _allContents = new HashSet<VisualElement>();

            var allVisualElements = EOverlayMethods.AllVisualElements();

            
            _root.Add(_navigationBar);

            _root.Add(_content);
            foreach ((var visualElement, var name) in allVisualElements)
            {

                VisualElement groupBox = _allContents.FirstOrDefault(x => x.name == name);
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
        private void OndisplayedChanged(bool obj)
        {
            Debug.LogError(obj);
        }
        private void SelectTab(string name)
        {
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
            Selection.selectionChanged += SelectionChanged;
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
            _navigationBar = new ScrollView()
            {
                focusable = false,
                style =
                {
                    minHeight = 100,
                    maxHeight = 400,
                    flexDirection = FlexDirection.Column,
                },

            };

        }
        private void SelectionChanged()
        {
            CreatePanelContent();
        }

    }
}
