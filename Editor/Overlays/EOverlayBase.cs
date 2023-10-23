using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EOverlays.Editor.Attributes;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Packages.eoverlays.Editor.Overlays
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
    public abstract class EOverlayBase
    {
        internal static Dictionary<VisualElement, string> AllVisualElements()
        {
            //Find classes those inherited from EOverlayBase
            IEnumerable<Type> allEOverlayClasses = typeof(EOverlayBase).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(EOverlayBase)));
            var methods = new List<MethodInfo>();
            foreach (var type in allEOverlayClasses)
            {
                Debug.LogWarning(type.FullName);
                var overlayMethods = type.GetMethods()
                    .Where(method => method.IsStatic && Attribute.IsDefined(method, typeof(EOverlayElementAttribute))).ToList();
                methods.AddRange(overlayMethods);
            }

            //Sort by Order value of own attribute
            IOrderedEnumerable<MethodInfo> orderedMethods = methods
                .OrderBy(x => ((EOverlayElementAttribute)x.GetCustomAttribute(typeof(EOverlayElementAttribute))).Order);

            //Convert methodinfo to visual element
            var result = new Dictionary<VisualElement, string>();
            foreach (var method in orderedMethods)
            {
                VisualElement visualElement;
                if (method.ReturnType != typeof(VisualElement))
                {
                    visualElement = new GroupBox();
                    var parameters = method.GetParameters();
                    var methodParameterPair = new MethodParameterPair(method, new object[parameters.Length]);
                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var parameter = parameters[i];
                        var paramElement = parameter.ParameterType.GetVisualElementByType(methodParameterPair, i);
                        paramElement.name = $"param-id-{i}";
                        visualElement.Add(paramElement);
                    }

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

                var name = ((EOverlayElementAttribute)method.GetCustomAttribute(typeof(EOverlayElementAttribute))).Name;
                result.Add(visualElement, name);
            }


            return result;

        }
    }
}
