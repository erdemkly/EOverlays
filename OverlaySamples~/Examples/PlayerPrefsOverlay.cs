using System;
using EOverlays.Editor.Attributes;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace EOverlays.Editor.OverlayExamples
{
    public class PlayerPrefsOverlay
    {
        #region Enums

        private enum PlayerPrefsType
        {
            String,
            Int,
            Float
        }

        #endregion

        private static void SetValueByPrefsType(PlayerPrefsType playerPrefsType, object value)
        {
            switch (playerPrefsType)
            {
                case PlayerPrefsType.String:
                    ((TextField)_playerPrefsValueField).value = value.ToString();
                    break;
                case PlayerPrefsType.Int:
                    ((IntegerField)_playerPrefsValueField).value = (int)value;
                    break;
                case PlayerPrefsType.Float:
                    ((FloatField)_playerPrefsValueField).value = (float)value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerPrefsType), playerPrefsType, null);
            }
        }
        private static VisualElement GetVisualElementByPrefsType(PlayerPrefsType playerPrefsType)
        {
            return playerPrefsType switch
            {
                PlayerPrefsType.String => new TextField(),
                PlayerPrefsType.Int => new IntegerField(),
                PlayerPrefsType.Float => new FloatField(),
                _ => throw new ArgumentOutOfRangeException(nameof(playerPrefsType), playerPrefsType, null)
            };
        }
        private static object GetVisualElementValueByPrefsType(PlayerPrefsType playerPrefsType)
        {
            return playerPrefsType switch
            {
                PlayerPrefsType.String => (_playerPrefsValueField as TextField)?.value,
                PlayerPrefsType.Int => ((IntegerField)_playerPrefsValueField).value,
                PlayerPrefsType.Float => ((FloatField)_playerPrefsValueField).value,
                _ => throw new ArgumentOutOfRangeException(nameof(playerPrefsType), playerPrefsType, null)
            };
        }
        private static object FetchPlayerPrefsValue(PlayerPrefsType playerPrefsType, string prefsName)
        {
            return playerPrefsType switch
            {
                PlayerPrefsType.String => PlayerPrefs.GetString(prefsName),
                PlayerPrefsType.Int => PlayerPrefs.GetInt(prefsName),
                PlayerPrefsType.Float => PlayerPrefs.GetFloat(prefsName),
                _ => throw new ArgumentOutOfRangeException(nameof(playerPrefsType), playerPrefsType, null)
            };
        }
        private static void SetPlayerPrefs(PlayerPrefsType playerPrefsType, string prefsName, object value)
        {
            switch (playerPrefsType)
            {

                case PlayerPrefsType.String:
                    PlayerPrefs.SetString(prefsName, (string)value);
                    break;
                case PlayerPrefsType.Int:
                    PlayerPrefs.SetInt(prefsName, (int)value);
                    break;
                case PlayerPrefsType.Float:
                    PlayerPrefs.SetFloat(prefsName, (float)value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerPrefsType), playerPrefsType, null);
            }
        }

        private static PlayerPrefsType _ppt;

        private static VisualElement _playerPrefsValueField;
        private static TextField _playerPrefsNameField;
        private static VisualElement _root;
        private static EnumField _prefsTypeElement;
        private static bool _init;
        private static string _prefsName;
        private static void Init()
        {
            if (_init) return;
            _init = true;

            _root = new VisualElement();

            _prefsTypeElement = new EnumField(PlayerPrefsType.Float);
            _root.Add(_prefsTypeElement);

            _ppt = PlayerPrefsType.Float;
            _prefsTypeElement.RegisterValueChangedCallback((callback) =>
            {
                var value = (PlayerPrefsType)callback.newValue;
                _ppt = value;
                var index = _root.IndexOf(_playerPrefsValueField);
                var newPrefsField = GetVisualElementByPrefsType(value);
                _root.Insert(index, newPrefsField);
                _root.Remove(_playerPrefsValueField);
                _playerPrefsValueField = newPrefsField;

                Refresh();
            });

            _root.Add(new Label("Player Prefs Name"));
            _playerPrefsNameField = new TextField();
            _playerPrefsNameField.RegisterValueChangedCallback((callback) =>
            {
                Refresh();
            });
            _root.Add(_playerPrefsNameField);

            _root.Add(new Label("Player Prefs Value"));
            _playerPrefsValueField = GetVisualElementByPrefsType(_ppt);
            _root.Add(_playerPrefsValueField);

            var btnGroup = new GroupBox()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1,
                    flexShrink = 1,
                    alignSelf = Align.Center,
                    alignItems = Align.Stretch
                },
            };
            
            var btnSet = new Button(() =>
            {
                SetPlayerPrefs(_ppt, _playerPrefsNameField.value, GetVisualElementValueByPrefsType(_ppt));
                Debug.Log($"{_playerPrefsNameField.value} new value: {FetchPlayerPrefsValue(_ppt, _playerPrefsNameField.value)}");
                Refresh();
            });
            btnSet.Add(new Label("Set"));
            btnGroup.Add(btnSet);
            
            var btnClearAll = new Button(()=>
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("Deleted all player prefs.");
                Refresh();
            });
            btnClearAll.Add(new Label("Clear All Prefs"));
            btnGroup.Add(btnClearAll);
            
            _root.Add(btnGroup);


        }

        private static void Refresh()
        {
            SetValueByPrefsType(_ppt,FetchPlayerPrefsValue(_ppt, _playerPrefsNameField.value));
        }

        [EOverlayElement("Player Prefs")]
        public static VisualElement CreateOverlay()
        {
            Init();
            return _root;
        }
    }
}
