using Editor.Attributes;
using UnityEngine;
using UnityEngine.UIElements;
namespace Editor.Overlays
{
    public class TimeOverlay
    {
        private static bool _active;
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
            timeScaleSlider.showInputField = true;



            void SetSliderValue(float value)
            {
                Time.timeScale = value;
            }

            void SetSliderActive(bool active)
            {
                timeScaleSlider.SetEnabled(active);
                timeScaleSlider.style.opacity = active ? 1 : 0.5f;
                Time.timeScale = active ? timeScaleSlider.value : 1;
                _active = active;
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
