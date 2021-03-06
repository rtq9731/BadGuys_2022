using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAndInputField : MonoBehaviour
{
    [SerializeField] float max = 100;
    [SerializeField] float min = 0;

    [SerializeField] public Slider inputSlider;
    [SerializeField] public InputField inputField;

    public event Action<float> onValueChange;

    private void Start()
    {
        inputSlider.onValueChanged.AddListener(onValueChanged);
        inputField.onValueChanged.AddListener(onValueChanged);
    }

    public void SetValue(float value)
    {
        inputField.onValueChanged.RemoveListener(onValueChanged);
        inputSlider.onValueChanged.RemoveListener(onValueChanged);

        value = Clamp(value);
        inputField.text = value.ToString();
        inputField.placeholder.GetComponent<Text>().text = value.ToString();
        inputSlider.value = value / max;

        inputField.onValueChanged.AddListener(onValueChanged);
        inputSlider.onValueChanged.AddListener(onValueChanged);
    }

    private void onValueChanged(float num)
    {
        inputField.onValueChanged.RemoveListener(onValueChanged);

        num = Clamp(num * max);
        inputField.text = num.ToString();
        inputSlider.value = num / max;

        onValueChange?.Invoke(num);
        inputField.onValueChanged.AddListener(onValueChanged);
    }

    private void onValueChanged(string str)
    {
        inputSlider.onValueChanged.RemoveListener(onValueChanged);

        if (float.TryParse(str, out float result))
        {
            result = Clamp(result);

            inputField.text = result.ToString();
            inputSlider.value = result / max;
        }
        else
        {
            inputField.text = (inputSlider.value * max).ToString();
        }

        onValueChange?.Invoke(result);
        inputSlider.onValueChanged.AddListener(onValueChanged);
    }

    private float Clamp(float num)
    {
        return Mathf.Clamp(num, min, max);
    }
}
