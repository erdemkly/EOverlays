using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EOverlays.Editor.Attributes;
using UnityEngine;
using UnityEngine.UIElements;
namespace Packages.eoverlays.Editor.Overlays
{
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
                if (method.Invoke(new object(), new object[]
                        { }) is not VisualElement visualElement) continue;
                var name = ((EOverlayElementAttribute)method.GetCustomAttribute(typeof(EOverlayElementAttribute))).Name;
                result.Add(visualElement, name);
            }


            return result;

        }
    }
}