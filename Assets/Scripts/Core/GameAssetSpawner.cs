using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetSpawner : MonoBehaviour
{
    private static int SpawnedGameAssetKey;
    private static Dictionary<int, GameObject> SpawnedGameAssets = new Dictionary<int, GameObject>();

    private GameAssetManager GameAssetManager;

    [SerializeField]
    private int GameAssetId = -1;
    [SerializeField]
    private bool SpawnOnStartedGame;
    [SerializeField]
    private bool IsSocketSpawner;

    private void Start()
    {
        GameAssetManager = GameManager.Instance.GameAssetManager;
        if(GameAssetManager == null)
        {
            return;
        }

        if(SpawnOnStartedGame)
        {
            SpawnGameAsset();
        }
    }

    public async void SpawnGameAsset(int spawnGameAssetId = -1)
    {
        if (GameAssetManager == null)
        {
            return;
        }

        if (spawnGameAssetId == -1)
        {
            spawnGameAssetId = GameAssetId;
        }

        GameObject loadedAsset = await GameAssetManager.LoadAssetUsingGameAssetId(spawnGameAssetId);
        GameObject spawnedAsset = Instantiate(loadedAsset, this.transform.position, this.transform.rotation);
        GameAssetData assetData = spawnedAsset.AddComponent<GameAssetData>();
        spawnedAsset.transform.parent = IsSocketSpawner ? this.transform : null;
        assetData.Init(SpawnedGameAssetKey, (GameManager.Instance.DataTableManager.DataTableMap["GameAssetDB"] as GameAssetDB).GetDataDictionary()[spawnGameAssetId]);
        SpawnedGameAssets.Add(SpawnedGameAssetKey, spawnedAsset);
        ++SpawnedGameAssetKey;
    }
}
