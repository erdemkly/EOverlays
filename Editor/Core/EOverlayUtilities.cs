using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (type.IsInterface) return false;
            var baseType = type;
            do
            {
                baseType = baseType.BaseType;
                if (baseType == targetType) return true;
            } while (baseType.BaseType != null);

            return false;
        }

        private static VisualElement GetListVisualElement(Type type, object[] parameterValues, int index,
            Action<object[]> onValueChanged)
        {
            var root = new VisualElement();
            var groupBox = new GroupBox
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            var elements = new Foldout
            {
                text = "elements",
            };
            var intField = new IntegerField()
            {
                style = { maxHeight = 20, maxWidth = 80, flexGrow = 1 }
            };

            intField.RegisterValueChangedCallback((callback) =>
            {
                var array = new object[callback.newValue];

                elements.Clear();
                for (int i = 0; i < callback.newValue; i++)
                {
                    var i1 = i;
                    if (type.IsValueType) array[i1] = Activator.CreateInstance(type);
                    elements.Add(GetVisualElementByType(type, array, i1, (value) =>
                    {
                        array[i1] = value;
                        parameterValues = array;
                        onValueChanged?.Invoke(parameterValues);
                    }));
                }

                parameterValues = array;
                onValueChanged?.Invoke(parameterValues);
            });
            groupBox.Add(elements);
            groupBox.Add(intField);
            root.Add(groupBox);
            return root;
        }

        private static VisualElement GetListVisualElementWithValue(Type type, object[] values)
        {
            var root = new VisualElement();
            var groupBox = new GroupBox
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            var elements = new Foldout
            {
                text = "elements",
            };
            for (int i = 0; i < values.Length; i++)
            {
                elements.Add(GetVisualElementByTypeWithValue(type, values[i]));
            }

            groupBox.Add(elements);
            root.Add(groupBox);
            return root;
        }

        public static VisualElement GetVisualElementByType(this Type type, object[] parameterValues, int index,
            Action<object> onChangedValue = null)
        {
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

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var root = GetListVisualElement(elementType, parameterValues, index, (array) =>
                {
                    Array arr = Array.CreateInstance(elementType!, array.Length);
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
                var elementType = type.GetGenericArguments().Single();
                var listType = type.GetGenericTypeDefinition();
                var constructedListType = listType.MakeGenericType(elementType);
                var root = GetListVisualElement(elementType, parameterValues, index, (array) =>
                {
                    var instance = (IList)Activator.CreateInstance(constructedListType);
                    for (var i = 0; i < array.Length; i++)
                    {
                        instance.Add(array[i]);
                    }

                    parameterValues[index] = instance;
                    onChangedValue?.Invoke(parameterValues);
                });
                return root;
            }


            if (type.IsInterface)
            {
                List<Type> GetTypesOfInterfaceInheritors(Type t)
                {
                    if (!t.IsInterface) return null;
                    var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                    var result = new List<Type>();
                    foreach (var allAssembly in allAssemblies)
                    {
                        result.AddRange(allAssembly.GetTypes()
                            .Where(x => !x.IsInterface && !x.IsAbstract && t.IsAssignableFrom(x)));
                    }

                    return result;
                }

                var types = GetTypesOfInterfaceInheritors(type);
                var root = new GroupBox();
                var elements = new GroupBox();
                var dropDown = new DropdownField();
                dropDown.RegisterValueChangedCallback((callback) =>
                {
                    elements.Clear();
                    var selectedType = types[dropDown.index];
                    elements.Add(GetVisualElementByType(selectedType, parameterValues, index, (newValue) =>
                    {
                        parameterValues[index] = newValue;
                        onChangedValue?.Invoke(parameterValues);
                    }));
                });
                dropDown.choices = types.Select(x => x.Name.ToString()).ToList();
                root.Add(dropDown);
                root.Add(elements);
                return root;
            }

            if (type.IsStructOrClass())
            {
                var fields = type.GetFields();
                var properties = type.GetProperties();
                var fieldValues = new object[fields.Length + properties.Length];
                var root = new Foldout()
                {
                    text = "Properties"
                };

                void ValueChanged(object newValue, int i)
                {
                    fieldValues[i] = newValue;
                    var genericType = Activator.CreateInstance(type);
                    for (var j = 0; j < fields.Length; j++)
                    {
                        fields[j].SetValue(genericType, fieldValues[j]);
                    }

                    for (var j = 0; j < properties.Length; j++)
                    {
                        properties[j].SetValue(genericType, fieldValues[j + fields.Length]);
                    }

                    parameterValues[index] = genericType;
                    onChangedValue?.Invoke(genericType);
                }

                for (var i = 0; i < fields.Length; i++)
                {
                    var i1 = i;
                    var fieldInfo = fields[i1];
                    root.Add(new Label(fieldInfo.Name));
                    if (fieldInfo.GetType().IsValueType)
                        fieldValues[i1] = Activator.CreateInstance(fieldInfo.GetType());
                    root.Add(fieldInfo.FieldType.GetVisualElementByType(fieldValues, i1,
                        (newValue) => { ValueChanged(newValue, i1); }));
                }

                for (var i = 0; i < properties.Length; i++)
                {
                    var i1 = i + fields.Length;
                    var propertyInfo = properties[i];
                    root.Add(new Label(propertyInfo.Name));
                    if (propertyInfo.GetType().IsValueType)
                        fieldValues[i1] = Activator.CreateInstance(propertyInfo.GetType());
                    root.Add(propertyInfo.PropertyType.GetVisualElementByType(fieldValues, i1,
                        (newValue) => { ValueChanged(newValue, i1); }));
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

            if (type.EqualsWithBaseType(typeof(Object)))
            {
                var field = new ObjectField();
                field.objectType = type;
                field.value = (Object)value;
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

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var instance = (Array)value;
                if (instance == null) return new VisualElement();
                var arr = new object[instance.Length];
                for (int i = 0; i < instance.Length; i++)
                {
                    arr[i] = instance.GetValue(i);
                }

                var root = GetListVisualElementWithValue(elementType, arr);
                return root;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                var instance = (IList)value;
                var arr = new object[instance.Count];
                for (var i = 0; i < instance.Count; i++)
                {
                    arr[i] = instance[i];
                }

                var elementType = type.GetGenericArguments().Single();
                var root = GetListVisualElementWithValue(elementType, arr);
                return root;
            }

            if (type.IsInterface)
            {
                var root = new GroupBox();
                
                var properties = type.GetProperties();
                var elements = new Foldout()
                {
                    text = "Properties"
                };
                for (var i = 0; i < properties.Length; i++)
                {
                    var propertyInfo = properties[i];
                    elements.Add(new Label(propertyInfo.Name));
                    elements.Add(propertyInfo.PropertyType.GetVisualElementByTypeWithValue(propertyInfo.GetValue(value)));
                }
                
                root.Add(elements);
                return root;
            }

            if (type.IsStructOrClass())
            {
                var fields = type.GetFields();
                var properties = type.GetProperties();
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

                for (var i = 0; i < properties.Length; i++)
                {
                    var propertyInfo = properties[i];
                    root.Add(new Label(propertyInfo.Name));
                    root.Add(propertyInfo.PropertyType.GetVisualElementByTypeWithValue(propertyInfo.GetValue(value)));
                }

                return root;
            }

            return null;
        }
    }
}