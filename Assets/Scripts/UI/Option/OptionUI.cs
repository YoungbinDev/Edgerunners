using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject TabButtonPrefabAssetRef;
    private GameObject TabButtonPrefab;
    private AsyncOperationHandle<GameObject> handle_TabButton;

    [SerializeField]
    private AssetReferenceGameObject DropdownPrefabAssetRef;
    private GameObject DropdownPrefab;
    private AsyncOperationHandle<GameObject> handle_Dropdown;

    private bool isAssetsLoaded = false;

    [SerializeField]
    private HorizontalOrVerticalLayoutGroup TabMenuLayout;
    [SerializeField]
    private HorizontalOrVerticalLayoutGroup ContentsLayout;
    [SerializeField]
    private int CurrentTabMenuIndex = 0;

    private Dictionary<int, List<GameObject>> OptionUIDictionary = new();

    private void OnDestroy()
    {
        UnLoadAssets();
    }

    void OnEnable()
    {
        if (!isAssetsLoaded)
        {
            LoadAssets();
        }

        if(GameManager.Instance?.GameFeatureManager?.GameFeature == null)
        {
            return;
        }

        MakeUI(GameManager.Instance.GameFeatureManager.GameFeature.OptionData);
    }

    private void LoadAssets()
    {
        if (!handle_TabButton.IsValid())
        {
            handle_TabButton = TabButtonPrefabAssetRef.LoadAssetAsync<GameObject>();
            handle_TabButton.WaitForCompletion();
            if (handle_TabButton.Status == AsyncOperationStatus.Succeeded)
            {
                TabButtonPrefab = handle_TabButton.Result;
            }
        }

        if (!handle_Dropdown.IsValid())
        {
            handle_Dropdown = DropdownPrefabAssetRef.LoadAssetAsync<GameObject>();
            handle_Dropdown.WaitForCompletion();
            if (handle_Dropdown.Status == AsyncOperationStatus.Succeeded)
            {
                DropdownPrefab = handle_Dropdown.Result;
            }
        }

        isAssetsLoaded = true;
    }

    private void UnLoadAssets()
    {
        if (!isAssetsLoaded) return;

        if (handle_TabButton.IsValid())
        {
            if (handle_TabButton.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handle_TabButton);
            }
        }

        if (handle_Dropdown.IsValid())
        {
            if (handle_Dropdown.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handle_Dropdown);
            }
        }

        isAssetsLoaded = false;
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

    private void CreateTabButton(string label, int index)
    {
        if (TabButtonPrefab == null) return;

        var tab = Instantiate(TabButtonPrefab, TabMenuLayout.transform);
        tab.GetComponentInChildren<StringUIText>().SetStringUI(label);
        tab.GetComponent<Button>().onClick.AddListener(() => OnClickTabMenu(index));
    }

    private void CreateContents(List<OptionItemData> options, int groupIndex)
    {
        List<GameObject> uiList = new();

        foreach (var option in options)
        {
            GameObject optionUI = CreateOptionItemUI(option);
            if (optionUI == null) continue;

            optionUI.SetActive(false);
            uiList.Add(optionUI);
        }

        OptionUIDictionary.Add(groupIndex, uiList);
    }

    private GameObject CreateOptionItemUI(OptionItemData option)
    {
        if (option is DropdownOptionItemData)
        {
            if (DropdownPrefab == null) return null;

            var ui = Instantiate(DropdownPrefab, ContentsLayout.transform);
            ui.GetComponentInChildren<StringUIText>().SetStringUI(option.LabelStringId);

            var dropdownComponent = ui.GetComponent<DropdownOptionItemUI>();
            int currentValue = GameManager.Instance.OptionManager.GetValue<int>(option.OptionId);

            dropdownComponent.Initialize(option, currentValue);

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
