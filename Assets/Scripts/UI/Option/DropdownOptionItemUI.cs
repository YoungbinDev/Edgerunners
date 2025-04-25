using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownOptionItemUI : MonoBehaviour, IOptionItemUI
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Button resetButton;
    private int defaultValue;
    private EOptionId optionId;

    public void Initialize(OptionItemData data, object currentValue)
    {
        if (data is not DropdownOptionItemData dropdownData)
        {
            Debug.LogWarning("잘못된 OptionItemData 타입입니다.");
            return;
        }

        optionId = data.OptionId;
        defaultValue = dropdownData.DefaultValue;
        dropdown.ClearOptions();
        dropdown.AddOptions(dropdownData.Items);
        dropdown.value = currentValue is int val ? val : defaultValue;
        dropdown.onValueChanged.AddListener(OnValueChanged);
        resetButton.onClick.AddListener(() => ResetToDefault());

        UpdateResetButton();
    }

    private void OnValueChanged(int value)
    {
        GameManager.Instance.OptionManager.SetValue(optionId, value, true);

        UpdateResetButton();
    }

    public bool IsModified() => dropdown.value != defaultValue;
    public void ResetToDefault() => dropdown.value = defaultValue;
    private void UpdateResetButton() => resetButton.gameObject.SetActive(IsModified());
}