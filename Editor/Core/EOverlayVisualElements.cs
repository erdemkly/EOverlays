using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    internal static class EOverlayVisualElements
    {
        internal static VisualElement NavigationBar(Action refreshAction)
        {
            var navigationBar = new ScrollView()
            {
                focusable = false,
                style =
                {
                    minWidth = 100,
                    flexDirection = FlexDirection.Column,
                },
            };
            var refreshButton = new Button(()=>refreshAction?.Invoke());
            refreshButton.Add(new Image
            {
                image = EditorGUIUtility.IconContent("d_Refresh@2x").image,
                style = { flexGrow = 1},
                focusable = false
            });
            refreshButton.style.width = 25;
            refreshButton.style.height = 25;
            navigationBar.Add(refreshButton);
            return navigationBar;
        }
    }
}