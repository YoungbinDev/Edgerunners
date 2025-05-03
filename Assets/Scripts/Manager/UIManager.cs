using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : ManagerBase
{
    [SerializeField] private Canvas mainCanvas;

    public override async void Init()
    {
        Debug.Log("UIManager �ʱ�ȭ");

        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }

        await OpenUI("MainMenuUI");
    }

    public async Task<GameObject> OpenUI(string addressableKey)
    {
        if (string.IsNullOrEmpty(addressableKey))
        {
            Debug.LogError("[UIManager] OpenUI ����: addressableKey�� ����ֽ��ϴ�.");
            return null;
        }

        var handle = Addressables.LoadAssetAsync<GameObject>(addressableKey);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject uiInstance = Instantiate(handle.Result, mainCanvas.transform);

            Addressables.Release(handle);

            return uiInstance;
        }
        else
        {
            Debug.LogError($"[UIManager] OpenUI ����: {addressableKey} �ε� ����");
            return null;
        }
    }
}
