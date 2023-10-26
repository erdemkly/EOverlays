using System;
namespace Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EOverlayElementAttribute : Attribute
    {
        public readonly int Order;
        public readonly string Name;
        public EOverlayElementAttribute(string name, int order = 0)
        {
            Name = name;
            Order = order;
        }
    }
}
