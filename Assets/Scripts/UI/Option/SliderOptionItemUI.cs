using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderOptionItemUI : MonoBehaviour, IOptionItemUI
{
    [SerializeField] private Slider slider;
    [SerializeField] private float stepSize = 0.1f;
    [SerializeField] private Button resetButton;
    [SerializeField] private TMP_Text valueText;

    private float defaultValue;
    private EOptionId optionId;
    private bool isInteger;

    public void Initialize(OptionItemData data, object currentValue)
    {
        if (data is not SliderOptionItemData sliderData)
        {
            Debug.LogWarning("잘못된 OptionItemData 타입입니다.");
            return;
        }

        optionId = data.OptionId;
        defaultValue = sliderData.DefaultValue;
        slider.minValue = sliderData.MinValue;
        slider.maxValue = sliderData.MaxValue;
        slider.value = currentValue is float val ? val : defaultValue;
        slider.onValueChanged.AddListener(OnValueChanged);
        resetButton.onClick.AddListener(() => ResetToDefault());

        UpdateValueText(slider.value);
        UpdateResetButton();
    }

    private void OnValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value / stepSize) * stepSize;
        slider.SetValueWithoutNotify(roundedValue); // 슬라이더를 정정된 값으로 되돌림
        GameManager.Instance.GetManager<OptionManager>()?.SetValue(optionId, roundedValue, true);

        UpdateValueText(roundedValue);
        UpdateResetButton();
    }

    public bool IsModified() => !Mathf.Approximately(slider.value, defaultValue);

    public void ResetToDefault()
    {
        slider.SetValueWithoutNotify(defaultValue); // 콜백 호출 방지

        UpdateValueText(defaultValue);
        OnValueChanged(defaultValue); // 수동 처리
    }

    private void UpdateValueText(float value)
    {
        if (valueText != null)
            valueText.text = isInteger ? ((int)value).ToString() : value.ToString("0.00");
    }

    private void UpdateResetButton() => resetButton.gameObject.SetActive(IsModified());
}