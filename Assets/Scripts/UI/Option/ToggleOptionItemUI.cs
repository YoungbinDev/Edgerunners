using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOptionItemUI : MonoBehaviour, IOptionItemUI
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Button resetButton;

    private bool defaultValue;
    private EOptionId optionId;

    public void Initialize(OptionItemData data, object currentValue)
    {
        if (data is not ToggleOptionItemData toggleData)
        {
            Debug.LogWarning("잘못된 OptionItemData 타입입니다.");
            return;
        }

        optionId = data.OptionId;
        defaultValue = toggleData.DefaultValue;
        toggle.isOn = currentValue is bool val ? val : defaultValue;
        toggle.onValueChanged.AddListener(OnValueChanged);
        resetButton.onClick.AddListener(() => ResetToDefault());

        UpdateResetButton();
    }
    private void OnValueChanged(bool value)
    {
        GameManager.Instance.OptionManager.SetValue(optionId, value, true);

        UpdateResetButton();
    }

    public bool IsModified() => toggle.isOn != defaultValue;
    public void ResetToDefault() => toggle.isOn = defaultValue;
    private void UpdateResetButton() => resetButton.gameObject.SetActive(IsModified());
}