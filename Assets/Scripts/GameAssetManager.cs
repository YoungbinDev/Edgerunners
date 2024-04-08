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
        if(GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        GameAssetDB = GameManager.Instance.DataTableManager.DataTableMap["GameAssetDB"] as GameAssetDB;
    }

    public async Task<GameObject> CreateAsset(int gameAssetId)
    {
        if(GameAssetDB == null)
        {
            return null;
        }

        GameObject createdGameAsset = await LoadAsset(GameAssetDB.GetDataDictionary()[gameAssetId].AssetName3D);
        return createdGameAsset;
    }

    private async Task<GameObject> LoadAsset(string assetKey)
    {
        // Addressable 에셋 로드
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetKey);

        // 로드가 완료될 때까지 대기
        await handle.Task;

        // 에셋이 로드되면 인스턴스를 반환
        return handle.Result;
    }
}
