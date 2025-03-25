using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameAssetManager : MonoBehaviour
{
    private GameAssetDB GameAssetDB;

    public void Init()
    {
        if (GameManager.Instance?.DataTableManager?.DataTableMap == null)
        {
            return;
        }

        if (!GameManager.Instance.DataTableManager.DataTableMap.ContainsKey("GameAssetDB"))
        {
            return;
        }

        GameAssetDB = GameManager.Instance.DataTableManager.DataTableMap["GameAssetDB"] as GameAssetDB;
    }

    public async Task<GameObject> LoadAssetUsingGameAssetId(int gameAssetId)
    {
        if(GameAssetDB == null)
        {
            return null;
        }

        GameObject createdGameAsset = await LoadAssetUsingAddressableKey(GameAssetDB.GetDataDictionary()[gameAssetId].AssetName3D);
        return createdGameAsset;
    }

    public async Task<GameObject> LoadAssetUsingAddressableKey(string assetKey)
    {
        // Addressable ���� �ε�
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetKey);

        // �ε尡 �Ϸ�� ������ ���
        await handle.Task;

        // ������ �ε�Ǹ� �ν��Ͻ��� ��ȯ
        return handle.Result;
    }
}
