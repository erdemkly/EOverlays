using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EOverlays.Editor.Attributes;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace EOverlays.Editor.Core
{
    public class MethodParameterPair
    {
        public MethodInfo MethodInfo;
        public object[] Parameters;

        public MethodParameterPair(MethodInfo methodInfo, object[] parameters)
        {
            MethodInfo = methodInfo;
            Parameters = parameters;
        }
    }

    public static class EOverlayMethods
    {
        private static Dictionary<VisualElement, EOverlayElementAttribute> _allVisualElements;

        public static Dictionary<VisualElement, EOverlayElementAttribute> AllVisualElements
        {
            get
            {
                if (_allVisualElements == null) AdjustAllVisualElements();
                return _allVisualElements;
            }
        }

        private static void AdjustAllVisualElements()
        {
            if (_allVisualElements != null) return;
            _allVisualElements = new Dictionary<VisualElement, EOverlayElementAttribute>();

            //Find classes those their methods have EOverlayElementAttribute
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var methods = new List<MethodInfo>();

            foreach (var assembly in allAssemblies)
            {
                var typesWithAttribute = assembly.GetTypes()
                    .Where(type => type.GetMethods()
                        .Any(method => method.IsStatic && Attribute.IsDefined(method, typeof(EOverlayElementAttribute)))
                    );

                foreach (var type in typesWithAttribute)
                {
                    var overlayMethods = type.GetMethods()
                        .Where(method =>
                            method.IsStatic && Attribute.IsDefined(method, typeof(EOverlayElementAttribute)))
                        .ToList();
                    methods.AddRange(overlayMethods);
                }
            }

            //Sort by Order value of own attribute
            IOrderedEnumerable<MethodInfo> orderedMethods = methods
                .OrderBy(x => ((EOverlayElementAttribute)x.GetCustomAttribute(typeof(EOverlayElementAttribute))).Order);

            //Convert methodinfo to visual element
            foreach (var method in orderedMethods)
            {
                VisualElement visualElement;
                var attribute = ((EOverlayElementAttribute)method.GetCustomAttribute(typeof(EOverlayElementAttribute)));

                //Method visualization
                if (method.ReturnType != typeof(VisualElement))
                {
                    visualElement = new GroupBox();

                    //Method parameters visualization
                    var parameters = method.GetParameters();
                    var methodParameterPair = new MethodParameterPair(method, new object[parameters.Length]);
                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var parameter = parameters[i];
                        var paramGroup = new GroupBox()
                        {
                            style =
                            {
                                backgroundColor = new StyleColor(new Color(0.49f, 0.49f, 0.49f, 0.47f)),
                                flexDirection = FlexDirection.Column,
                                flexGrow = 0,
                                flexShrink = 1,
                            },
                        };
                        paramGroup.Add(new Label(parameter.Name));
                        var paramElement = parameter.ParameterType.GetVisualElementByType(methodParameterPair, i);
                        paramGroup.Add(paramElement);
                        visualElement.Add(paramGroup);
                    }

                    //Create button to invoke method
                    var invokeButton = new Button();
                    invokeButton.text = method.Name;
                    invokeButton.RegisterCallback<ClickEvent>((_) =>
                    {
                        var returnMethod = method.Invoke(new object(), methodParameterPair.Parameters);
                        if (method.ReturnType != typeof(void))
                        {
                            var resultElement = visualElement.Children().FirstOrDefault(x => x.name == "return-value");
                            if (resultElement != null)
                            {
                                visualElement.Remove(resultElement);
                            }

                            resultElement = method.ReturnType.GetVisualElementByTypeWithValue(returnMethod);
                            resultElement.name = "return-value";
                            resultElement.SetEnabled(false);
                            visualElement.Add(resultElement);
                        }
                    });


                    visualElement.Add(invokeButton);
                }
                else
                {
                    visualElement = method.Invoke(new object(), new object[]
                        { }) as VisualElement;
                }

                //Hide disabled methods
                if (attribute.EnableCondition != null && method.DeclaringType != null)
                {
                    var property = method.DeclaringType.GetProperty(attribute.EnableCondition);
                    if (property != null && property.GetMethod.ReturnType == typeof(bool))
                    {
                        attribute.Disabled = !(bool)property.GetMethod.Invoke(new object(), new object[] { });
                    }

                    var field = method.DeclaringType.GetField(attribute.EnableCondition);
                    if (field != null && field.FieldType == typeof(bool))
                    {
                        attribute.Disabled = !(bool)field.GetValue(new object());
                    }
                }

                if (attribute.Disabled) continue;


                if (visualElement == null) continue;


                _allVisualElements.Add(visualElement, attribute);
            }

            if (!_allVisualElements.Any())
            {
                _allVisualElements.Add(new Label("All elements hidden or there is no elements here."),
                    new EOverlayElementAttribute("NONE", 0, false));
            }
        }
    }
}