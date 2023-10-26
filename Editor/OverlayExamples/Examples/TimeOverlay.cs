using EOverlays.Editor.Attributes;
using UnityEngine;
using UnityEngine.UIElements;
namespace EOverlays.Editor.OverlayExamples
{
    public class TimeOverlay
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
            var fixedDeltaTimeToggle = new Toggle("Include Fixed Delta Time");
            var timeScaleSlider = new Slider();
            timeScaleSlider.showInputField = true;



            void SetTime(float value)
            {
                Time.timeScale = value;
                if (fixedDeltaTimeToggle.value) Time.fixedDeltaTime = value*0.02f;

            }

            void SetSliderActive(bool active)
            {
                timeScaleSlider.SetEnabled(active);
                timeScaleSlider.style.opacity = active ? 1 : 0.5f;
                SetTime(active ? timeScaleSlider.value : 1);
            }


            timeScaleToggle.RegisterValueChangedCallback((callback) =>
            {
                SetSliderActive(callback.newValue);
            });

            timeScaleSlider.RegisterValueChangedCallback((callback) =>
            {
                var newValue = callback.newValue;
                SetTime(newValue);
            });

            SetSliderActive(false);
            timeScaleSlider.value = Time.timeScale;

            _root.Add(timeScaleToggle);
            _root.Add(fixedDeltaTimeToggle);
            _root.Add(timeScaleSlider);

            return _root;
        }
    }
}
