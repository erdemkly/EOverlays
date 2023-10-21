using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EOverlays.Editor.Attributes;
using UnityEngine;
using UnityEngine.UIElements;
namespace EOverlays.Editor.Overlays
{
    public abstract class EOverlayBase
    {
        public static List<VisualElement> AllVisualElements()
        {
            //Find classes those inherited from EOverlayBase
            IEnumerable<Type> allEOverlayClasses = typeof(EOverlayBase).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(EOverlayBase)));
            var methods = new List<MethodInfo>();
            foreach (var type in allEOverlayClasses)
            {
                var overlayMethods = type.GetMethods()
                    .Where(method => method.IsStatic && Attribute.IsDefined(method, typeof(EOverlayElementAttribute))).ToList();
                methods.AddRange(overlayMethods);
            }

            //Sort by Order value of own attribute
            IOrderedEnumerable<MethodInfo> orderedMethods = methods
                .OrderBy(x => ((EOverlayElementAttribute)x.GetCustomAttribute(typeof(EOverlayElementAttribute))).Order);

            //Convert methodinfo to visual element
            var result = new List<VisualElement>();
            foreach (var method in orderedMethods)
            {
                if (method.Invoke(new object(), new object[] { }) is not VisualElement visualElement) continue;
                result.Add(visualElement);
            }


            return result;

        }
    }
}
