using System.Globalization;
using EOverlays.Editor.Attributes;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UIElements;
namespace EOverlays.Editor.Overlays
{
    public class TimeOverlay : EOverlayBase
    {
        private static bool _init;
        private static VisualElement _root;
        [EOverlayElement("Time")]
        public static VisualElement CreateOverlay()
        {
            if (_init) return _root;
            _init = true;
            _root = new VisualElement();
            var timeScaleToggle = new Toggle();
            var timeScaleSlider = new Slider();
            timeScaleSlider.Add(timeScaleToggle);

            timeScaleToggle.RegisterValueChangedCallback((callback) =>
            {
                timeScaleSlider.style.opacity = callback.newValue ? 1 : 0.5f;
                Time.timeScale = callback.newValue ? timeScaleSlider.value : 1;
            });

            void SetSliderValue(float value)
            {
                timeScaleSlider.label = value.ToString(CultureInfo.InvariantCulture);
                Time.timeScale = value;
            }

            timeScaleSlider.RegisterValueChangedCallback((callback) =>
            {
                var newValue = callback.newValue;
                SetSliderValue(newValue);
            });

            timeScaleToggle.value = false;
            
            SetSliderValue(Time.timeScale);

            _root.Add(timeScaleSlider);

            return _root;
        }
    }
}
