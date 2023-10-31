using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
namespace EOverlays.Editor.Core
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
            if (type == typeof(Object))
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
            if (type == typeof(Color))
            {
                var field = new ColorField();
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
            if (type == typeof(Object))
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
            if (type == typeof(Color))
            {
                var field = new ColorField()
                {
                    value = (Color)value
                };

                return field;
            }
            return null;
        }
    }
}
