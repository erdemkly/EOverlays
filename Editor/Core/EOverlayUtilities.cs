using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace EOverlays.Editor.Core
{
    public static class EOverlayUtilities
    {
        public static bool EqualsWithBaseType(this Type type, Type targetType)
        {
            var baseType = type;
            do
            {
                baseType = baseType.BaseType;
                if (baseType == targetType) return true;
            } while (baseType.BaseType != null);

            return false;
        }

        private static VisualElement GetArrayVisualElement(Type type, object[] parameterValues, int index,
            Action<object[]> onValueChanged)
        {
            var root = new VisualElement();
            var elements = new Foldout
            {
                text = "elements"
            };
            var intField = new IntegerField();
            intField.RegisterValueChangedCallback((callback) =>
            {
                var array = new object[callback.newValue];

                elements.Clear();
                for (int i = 0; i < callback.newValue; i++)
                {
                    var i1 = i;
                    elements.Add(GetVisualElementByType(type.GetElementType(), parameterValues, index, (value) =>
                    {
                        array[i1] = value;
                        onValueChanged?.Invoke(array);
                    }));
                }
            });
            root.Add(intField);
            root.Add(elements);
            return root;
        }

        public static VisualElement GetVisualElementByType(this Type type, object[] parameterValues, int index,
            Action<object> onChangedValue = null)
        {
            if (type.IsArray)
            {
                var root = GetArrayVisualElement(type, parameterValues, index, (array) =>
                {
                    Array arr = Array.CreateInstance(type.GetElementType()!, array.Length);
                    for (var i = 0; i < arr.Length; i++)
                    {
                        arr.SetValue(array[i], i);
                    }

                    parameterValues[index] = arr;
                });
                return root;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                ////////todo
            }

            if (type == typeof(bool))
            {
                var field = new Toggle();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(int))
            {
                var field = new IntegerField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(float))
            {
                var field = new FloatField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(double))
            {
                var field = new DoubleField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(long))
            {
                var field = new LongField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Enum))
            {
                var field = new EnumField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type.EqualsWithBaseType(typeof(Object)))
            {
                var field = new ObjectField();
                field.objectType = type;
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Vector2))
            {
                var field = new Vector2Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Vector3))
            {
                var field = new Vector3Field();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Vector2Int))
            {
                var field = new Vector2IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Vector3Int))
            {
                var field = new Vector3IntField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(string))
            {
                var field = new TextField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type == typeof(Color))
            {
                var field = new ColorField();
                field.RegisterValueChangedCallback((callback) =>
                {
                    if (parameterValues != null) parameterValues[index] = callback.newValue;
                    onChangedValue?.Invoke(callback.newValue);
                });
                return field;
            }

            if (type.IsStructOrClass())
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
                        var genericType = Activator.CreateInstance(type);
                        for (int j = 0; j < fields.Length; j++)
                        {
                            fields[j].SetValue(genericType, fieldValues[j]);
                        }

                        parameterValues[index] = genericType;
                    }));
                }


                return root;
            }

            return null;
        }

        public static bool IsStructOrClass(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive || type.IsClass;
        }

        public static VisualElement GetVisualElementByTypeWithValue(this Type type, object value)
        {
            if (type == typeof(bool))
            {
                var field = new Toggle()
                {
                    value = (bool)value
                };

                return field;
            }

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