using System;
using System.Collections.Generic;
using System.Linq;
using EOverlays.Editor.Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    public class EOverlayEditorWindow : EditorWindow
    {
        public VisualElement _content;
        private Action<string> _selectTabAction;

        private void OnEnable()
        {
            EOverlayMethods.RefreshUI += RefreshUI;
            EOverlayMethods.SelectTabAction += SelectTab;
        }

        private void RefreshUI()
        {
            Init();
        }

        private void OnDisable()
        {
            EOverlayMethods.RefreshUI -= RefreshUI;
            EOverlayMethods.SelectTabAction -= SelectTab;
        }

        [MenuItem("Window/EOverlays/Show")]
        public static void ShowExample()
        {
            EOverlayEditorWindow wnd = GetWindow<EOverlayEditorWindow>();
            wnd.Init();
            wnd.titleContent = new GUIContent("E-Overlays Window");
        }


        private void Init()
        {
            rootVisualElement.Clear();
            rootVisualElement.style.flexGrow = 1;
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
            rootVisualElement.Add(EOverlayVisualElements.RefreshButton());
            rootVisualElement.Add(EOverlayMethods.navigationBar);
            rootVisualElement.Add(_content);
            rootVisualElement.style.flexDirection = FlexDirection.Row;
            EOverlayMethods.InvokeSelectTab("");
        }

        private void SelectTab(string n)
        {
            if (EOverlayMethods.allContents.Count == 0) return;
            var element = EOverlayMethods.allContents.FirstOrDefault(x => x.name == n);
            if (element == null) element = EOverlayMethods.allContents.First();
            _content.Clear();
            _content.Add(element);
            _selectTabAction?.Invoke(element.name);
        }
    }
}