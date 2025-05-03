using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class OptionUI : ActivatableUI
{
    // -----------------------------
    // Serialized Fields (에디터에 노출)
    // -----------------------------

    [Header("Prefabs")]
    [SerializeField] private AssetReferenceGameObject tabButtonPrefabAssetRef;
    [SerializeField] private AssetReferenceGameObject dropdownPrefabAssetRef;

    [Header("Layouts")]
    [SerializeField] private HorizontalOrVerticalLayoutGroup tabMenuLayout;
    [SerializeField] private HorizontalOrVerticalLayoutGroup contentsLayout;

    [Header("Option Settings")]
    [SerializeField] private int currentTabMenuIndex = 0;

    // -----------------------------
    // Runtime Data (런타임에서 제어)
    // -----------------------------

    private GameObject tabButtonPrefab;
    private GameObject dropdownPrefab;
    private AsyncOperationHandle<GameObject> handleTabButton;
    private AsyncOperationHandle<GameObject> handleDropdown;

    private bool isAssetsLoaded = false;

    private OptionManager optionManager;
    private Dictionary<int, List<GameObject>> optionItemUIDictionary = new();

    private async void OnEnable()
    {
        // OptionManager이 null이면 오른쪽 값 대입
        optionManager ??= GameManager.Instance.GetManager<OptionManager>();
        if (optionManager == null)
        {
            Debug.LogWarning("[OnEnable] OptionManager is missing. Initialization aborted.");
            return;
        }

        if (!isAssetsLoaded)
        {
            await LoadAssetsAsync();
        }

        MakeUI(GameManager.Instance.GetManager<GameFeatureManager>()?.GameFeature?.OptionData);
    }

    private void OnDestroy()
    {
        UnLoadAssets();
    }

    private async Task LoadAssetsAsync()
    {
        var tabButtonHandle = tabButtonPrefabAssetRef.LoadAssetAsync<GameObject>();
        var dropdownHandle = dropdownPrefabAssetRef.LoadAssetAsync<GameObject>();

        await Task.WhenAll(tabButtonHandle.Task, dropdownHandle.Task); // 둘 다 완료될 때까지 기다린다

        if (tabButtonHandle.Status == AsyncOperationStatus.Succeeded)
        {
            handleTabButton = tabButtonHandle;
            tabButtonPrefab = tabButtonHandle.Result;
        }
        else
        {
            Debug.LogError("TabButtonPrefab 로딩 실패");
        }

        if (dropdownHandle.Status == AsyncOperationStatus.Succeeded)
        {
            handleDropdown = dropdownHandle;
            dropdownPrefab = dropdownHandle.Result;
        }
        else
        {
            Debug.LogError("DropdownPrefab 로딩 실패");
        }

        isAssetsLoaded = true;
    }

    private void UnLoadAssets()
    {
        if (!isAssetsLoaded) return;

        ReleaseHandle(handleTabButton);
        ReleaseHandle(handleDropdown);

        isAssetsLoaded = false;
    }

    private void ReleaseHandle(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.IsValid() && handle.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(handle);
        }
    }

    private void ClearUI()
    {
        currentTabMenuIndex = 0;
        optionItemUIDictionary.Clear();

        foreach (Transform child in tabMenuLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in contentsLayout.transform)
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
        if (tabButtonPrefab == null) return;

        var tab = Instantiate(tabButtonPrefab, tabMenuLayout.transform);
        tab.GetComponentInChildren<StringUIText>().SetStringUI(label);
        tab.GetComponent<Button>().onClick.AddListener(() => OnClickTabMenu(index));
    }

    private void CreateContents(List<OptionItemData> options, int groupIndex)
    {
        List<GameObject> itemUIList = new();

        foreach (var option in options)
        {
            GameObject optionItemUI = CreateOptionItemUI(option);
            if (optionItemUI == null) continue;

            optionItemUI.SetActive(false);
            itemUIList.Add(optionItemUI);
        }

        optionItemUIDictionary.Add(groupIndex, itemUIList);
    }

    private GameObject CreateOptionItemUI(OptionItemData option)
    {
        if (option is DropdownOptionItemData)
        {
            if (dropdownPrefab == null) return null;

            var ui = Instantiate(dropdownPrefab, contentsLayout.transform);
            ui.GetComponentInChildren<StringUIText>().SetStringUI(option.LabelStringId);

            var dropdownComponent = ui.GetComponent<DropdownOptionItemUI>();
            int currentValue = optionManager.GetValue<int>(option.OptionId);

            dropdownComponent.Initialize(option, currentValue);

            return ui;
        }

        // TODO: 슬라이더, 토글 등도 여기서 확장 가능
        return null;
    }

    private void OnClickTabMenu(int index)
    {
        foreach (GameObject optionItemUI in optionItemUIDictionary[currentTabMenuIndex])
        {
            optionItemUI.SetActive(false);
        }

        foreach (GameObject optionItemUI in optionItemUIDictionary[index])
        {
            optionItemUI.SetActive(true);
        }

        currentTabMenuIndex = index;
    }

    public void OnClickSaveButton()
    {
        GameManager.Instance.GetManager<OptionManager>().Save();
    }

    public async void OnClickCloseButton()
    {
        GameObject stringUIPopupPrefab = await GameManager.Instance.GetManager<UIManager>().OpenUI("StringUIPopup");
        StringUIPopup stringUIPopupComponent = stringUIPopupPrefab.GetComponent<StringUIPopup>();
        stringUIPopupComponent.Init("StringId_Notice", "StringId_Question_Close", "StringId_Yes", "StringId_No");
        stringUIPopupComponent.OnClickPositiveButtonEvent += () =>
        {
            GameManager.Instance.GetManager<OptionManager>().Revert();
            Destroy(stringUIPopupPrefab);
            Destroy(this.gameObject);
        };
        stringUIPopupComponent.OnClickNegativeButtonEvent += () =>
        {
            Destroy(stringUIPopupPrefab);
        };
    }
}
