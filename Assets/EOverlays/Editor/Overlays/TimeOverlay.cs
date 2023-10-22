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
            var timeScaleToggle = new Toggle("Active");
            var timeScaleSlider = new Slider();



            void SetSliderValue(float value)
            {
                timeScaleSlider.label = value.ToString(CultureInfo.InvariantCulture);
                Time.timeScale = value;
            }

            void SetSliderActive(bool active)
            {
                timeScaleSlider.style.opacity = active ? 1 : 0.5f;
                Time.timeScale = active ? timeScaleSlider.value : 1;
            }


            timeScaleToggle.RegisterValueChangedCallback((callback) =>
            {
                SetSliderActive(callback.newValue);
            });

            timeScaleSlider.RegisterValueChangedCallback((callback) =>
            {
                var newValue = callback.newValue;
                SetSliderValue(newValue);
            });

            SetSliderActive(false);
            SetSliderValue(Time.timeScale);
            timeScaleSlider.value = Time.timeScale;

            _root.Add(timeScaleToggle);
            _root.Add(timeScaleSlider);

            return _root;
        }
    }
}
