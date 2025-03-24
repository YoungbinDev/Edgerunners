using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject TabButtonPrefab;
    [SerializeField]
    private GameObject DropdownPrefab;
    [SerializeField]
    private HorizontalOrVerticalLayoutGroup TabMenuLayout;
    [SerializeField]
    private HorizontalOrVerticalLayoutGroup ContentsLayout;
    [SerializeField]
    private int CurrentTabMenuIndex = 0;
    private Dictionary<int, List<GameObject>> OptionUIDictionary = new();

    void OnEnable()
    {
        if(GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        if(GameManager.Instance.GameFeatureManager.GameFeature == null)
        {
            return;
        }

        MakeUI(GameManager.Instance.GameFeatureManager.GameFeature.OptionData);
        LoadOptionData();
    }

    private void ClearUI()
    {
        CurrentTabMenuIndex = 0;
        OptionUIDictionary.Clear();

        foreach (Transform child in TabMenuLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ContentsLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void MakeUI(OptionData optionData)
    {
        if(optionData == null) 
        {
            return;
        }

        ClearUI();

        int groupIndex = 0;
        foreach (var group in optionData.Groups)
        {
            CreateTabButton(group.GroupName, groupIndex);
            CreateContents(group.Options, groupIndex);
            ++groupIndex;
        }

        OnClickTabMenu(0);
    }

    private void LoadOptionData()
    {

    }

    private void CreateTabButton(string label, int index)
    {
        var tab = Instantiate(TabButtonPrefab, TabMenuLayout.transform);
        tab.GetComponentInChildren<StringUIText>().SetStringUI(label);
        tab.GetComponent<Button>().onClick.AddListener(() => OnClickTabMenu(index));
    }

    private void CreateContents(List<OptionItemData> options, int groupIndex)
    {
        List<GameObject> uiList = new();

        foreach (var option in options)
        {
            GameObject optionUI = CreateOptionUI(option);
            if (optionUI == null) continue;

            optionUI.SetActive(false);
            uiList.Add(optionUI);
        }

        OptionUIDictionary.Add(groupIndex, uiList);
    }

    private GameObject CreateOptionUI(OptionItemData option)
    {
        if (option is DropdownOptionItemData dropdown)
        {
            var ui = Instantiate(DropdownPrefab, ContentsLayout.transform);
            ui.GetComponentInChildren<StringUIText>().SetStringUI(option.LabelStringId);

            var dropdownComponent = ui.GetComponentInChildren<TMP_Dropdown>();
            dropdownComponent.ClearOptions();
            dropdownComponent.AddOptions(dropdown.Items);
            dropdownComponent.value = dropdown.DefaultItemIndex;

            return ui;
        }

        // TODO: 슬라이더, 토글 등도 여기서 확장 가능
        return null;
    }

    private void OnClickTabMenu(int index)
    {
        foreach (GameObject OptionUI in OptionUIDictionary[CurrentTabMenuIndex])
        {
            OptionUI.SetActive(false);
        }

        foreach (GameObject OptionUI in OptionUIDictionary[index])
        {
            OptionUI.SetActive(true);
        }

        CurrentTabMenuIndex = index;
    }
}
