using UnityEngine;
using UnityEngine.UI;

public class HudSlider : MonoBehaviour
{
    [SerializeField] private float _sliderValue;
    private Slider _slider;

    public float SliderValue
    {
        get { return _sliderValue; }
        set
        {
            _sliderValue = value; 

            if (_sliderValue <= 0) _sliderValue = 0;
            if (_sliderValue >= 100) _sliderValue = 100;
            
            _slider.value = _sliderValue;
            
        }
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
}