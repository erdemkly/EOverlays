using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
namespace Packages.eoverlays.Editor.Overlays
{
    public static class EOverlayUtilities
    {
        public static VisualElement GetVisualElementByType(this Type type, MethodParameterPair pair, int index)
        {
            if (type == typeof(int))
            {
                var field = new IntegerField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(float))
            {
                var field = new FloatField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(double))
            {
                var field = new DoubleField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(long))
            {
                var field = new LongField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(Enum))
            {
                var field = new EnumField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(object))
            {
                var field = new ObjectField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(Vector2))
            {
                var field = new Vector2Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(Vector3))
            {
                var field = new Vector3Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(Vector2Int))
            {
                var field = new Vector2IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(Vector3Int))
            {
                var field = new Vector3IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            if (type == typeof(string))
            {
                var field = new TextField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    pair.Parameters[index] = callback.newValue;
                });
                return field;
            }
            return null;
        }
        public static VisualElement GetVisualElementByTypeWithValue(this Type type, object value)
        {
            if (type == typeof(int))
            {
                var field = new IntegerField
                {
                    value = (int)value
                };

                return field;
            }
            if (type == typeof(float))
            {
                var field = new FloatField
                {
                    value = (float)value
                };

                return field;
            }
            if (type == typeof(double))
            {
                var field = new DoubleField
                {
                    value = (double)value
                };

                return field;
            }
            if (type == typeof(long))
            {
                var field = new LongField
                {
                    value = (long)value
                };

                return field;
            }
            if (type == typeof(Enum))
            {
                var field = new EnumField
                {
                    value = (Enum)value
                };

                return field;
            }
            if (type == typeof(object))
            {
                var field = new ObjectField
                {
                    value = (Object)value
                };

                return field;
            }
            if (type == typeof(Vector2))
            {
                var field = new Vector2Field
                {
                    value = (Vector2)value
                };

                return field;
            }
            if (type == typeof(Vector3))
            {
                var field = new Vector3Field
                {
                    value = (Vector3)value
                };

                return field;
            }
            if (type == typeof(Vector2Int))
            {
                var field = new Vector2IntField
                {
                    value = (Vector2Int)value
                };

                return field;
            }
            if (type == typeof(Vector3Int))
            {
                var field = new Vector3IntField
                {
                    value = (Vector3Int)value
                };

                return field;
            }
            if (type == typeof(string))
            {
                var field = new TextField
                {
                    value = (string)value
                };

                return field;
            }
            return null;
        }
        public static Type GetVisualElementTypeByObjectType(this Type type)
        {
            if (type == typeof(int))
            {
                return typeof(IntegerField);
            }
            if (type == typeof(float))
            {
                return typeof(FloatField);
            }
            if (type == typeof(double))
            {
                return typeof(DoubleField);
            }
            if (type == typeof(long))
            {
                return typeof(LongField);
            }
            if (type == typeof(Enum))
            {
                return typeof(EnumField);
            }
            if (type == typeof(object))
            {
                return typeof(ObjectField);
            }
            if (type == typeof(Vector2))
            {
                return typeof(Vector2Field);
            }
            if (type == typeof(Vector3))
            {
                return typeof(Vector3Field);
            }
            if (type == typeof(Vector2Int))
            {
                return typeof(Vector2IntField);
            }
            if (type == typeof(Vector3Int))
            {
                return typeof(Vector3IntField);
            }
            if (type == typeof(string))
            {
                return typeof(TextField);
            }
            return null;
        }
    }
}
