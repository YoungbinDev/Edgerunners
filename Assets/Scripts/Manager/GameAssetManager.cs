using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameAssetManager : ManagerBase
{
    private GameAssetDB GameAssetDB;

    public override void Init()
    {
        GameAssetDB = GameManager.Instance.GetManager<DataTableManager>()?.DataTableMap.TryGetValue("GameAssetDB", out var table) == true ? table as GameAssetDB : null;
        if(GameAssetDB == null)
        {
            Debug.LogWarning("[Init] GameAssetDB is missing. Initialization aborted.");
            return;
        }
    }

    public async Task<GameObject> LoadAssetUsingGameAssetId(int gameAssetId)
    {
        if(GameAssetDB == null)
        {
            Debug.LogWarning("[LoadAssetUsingGameAssetId] GameAssetDB is missing.");
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
