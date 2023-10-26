using System;
namespace Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EOverlayElementAttribute : Attribute
    {
        public readonly int Order;
        public readonly string Name;
        public string EnableCondition;
        public EOverlayElementAttribute(string name, int order = 0, string enableCondition = null)
        {
            EnableCondition = enableCondition;
            Name = name;
            Order = order;
        }
    }
}
