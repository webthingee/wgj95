using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private float _imageValue;
    private Image _image;

    public float ImageValue
    {
        get { return _imageValue; }
        set
        {
            _imageValue = value; 

            if (_imageValue <= 0) _imageValue = 0;
            if (_imageValue >= 1) _imageValue = 1;
            
            _image.fillAmount = _imageValue;
            
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}