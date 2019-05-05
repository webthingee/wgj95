using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudText : MonoBehaviour
{
    [SerializeField] private string _text;
    private TextMeshProUGUI _tmpText;

    public string TextValue
    {
        get { return _text; }
        set
        {
            _text = value; 
            _tmpText.text = _text;       
        }
    }
    
    public float FloatValue
    {
        get { return float.Parse(_text); }
        set
        {
            if (value <= 0) value = 0;
            
            _text = value.ToString(); 
            _tmpText.text = _text;                   
        }
    }

    private void Awake()
    {
        _tmpText = GetComponent<TextMeshProUGUI>();
    }
}
