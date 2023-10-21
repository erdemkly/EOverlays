using System;
namespace EOverlays.Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EOverlayElementAttribute : Attribute
    {
        public readonly int Order;
        public EOverlayElementAttribute(int order = 0)
        {
            Order = order;
        }
    }
}
