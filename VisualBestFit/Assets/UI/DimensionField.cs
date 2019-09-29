using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class DimensionField : MonoBehaviour
{
    [SerializeField]
    private InputField _field;

    public void ValidateInput (string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            try
            {
                double result = Convert.ToDouble(input);
                if (result <= 0) throw new FormatException("Input must be greater than 0");
            }
            catch (FormatException ex)
            {
                _field.text = string.Empty;
            }
        }
    }
    
    public double Value ()
    {
        return string.IsNullOrEmpty(_field.text) ? 0 : Convert.ToDouble(_field.text);
    }

    public void Reset()
    {
        _field = GetComponent<InputField>();
    }
}
