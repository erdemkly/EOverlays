using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    internal static class EOverlayVisualElements
    {
        internal static VisualElement NavigationBar()
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
            return navigationBar;
        }

        internal static VisualElement RefreshButton()
        {
            var refreshButton = new Button(EOverlayMethods.InvokeRefreshUI);
            refreshButton.Add(new Image
            {
                image = EditorGUIUtility.IconContent("d_Refresh@2x").image,
                style = { flexGrow = 1},
                focusable = false
            });
            refreshButton.style.width = 25;
            refreshButton.style.height = 25;
            return refreshButton;
        }
    }
}