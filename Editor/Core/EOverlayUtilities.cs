using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
namespace EOverlays.Editor.Core
{
    public static class EOverlayUtilities
    {
        public static VisualElement GetVisualElementByType(this Type type, MethodParameterPair pair, int index, Action<object> onChangedValue = null)
        {
            if (type == typeof(int))
            {
                var field = new IntegerField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(float))
            {
                var field = new FloatField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(double))
            {
                var field = new DoubleField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(long))
            {
                var field = new LongField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Enum))
            {
                var field = new EnumField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Object))
            {
                var field = new ObjectField();
                field.objectType = typeof(Object);
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Vector2))
            {
                var field = new Vector2Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Vector3))
            {
                var field = new Vector3Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Vector2Int))
            {
                var field = new Vector2IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Vector3Int))
            {
                var field = new Vector3IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(string))
            {
                var field = new TextField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type == typeof(Color))
            {
                var field = new ColorField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (pair != null) pair.Parameters[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }
            if (type.IsValueType && !type.IsPrimitive || type.IsClass)
            {
                var fields = type.GetFields();
                var fieldValues = new object[fields.Length];
                var root = new Foldout()
                {
                    text = "Properties"
                };
                for (var i = 0; i < fields.Length; i++)
                {
                    var i1 = i;
                    var fieldInfo = fields[i];
                    root.Add(new Label(fieldInfo.Name));
                    root.Add(fieldInfo.FieldType.GetVisualElementByType(null, -1, (newValue) =>
                    {
                        fieldValues[i1] = newValue;
                        var genericType = Activator.CreateInstance(type, fieldValues);
                        pair.Parameters[index] = genericType;
                    }));
                }
                return root;
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
                    objectType = typeof(Object),
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
            if (type.IsValueType && !type.IsPrimitive || type.IsClass)
            {
                var fields = type.GetFields();
                var root = new Foldout()
                {
                    text = "Properties"
                };
                for (var i = 0; i < fields.Length; i++)
                {
                    var fieldInfo = fields[i];
                    root.Add(new Label(fieldInfo.Name));
                    root.Add(fieldInfo.FieldType.GetVisualElementByTypeWithValue(fieldInfo.GetValue(value)));
                }
                return root;
            }
            return null;
        }
    }
}
