using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AssetReferenceGameFeature : AssetReferenceT<GameFeature>
{
    public AssetReferenceGameFeature(string guid) : base(guid)
    {
    }
}

public class GameFeatureManager : MonoBehaviour
{
    [SerializeField] 
    private AssetReferenceGameFeature GameFeatureAssetReference;

    [HideInInspector]
    public GameFeature GameFeature;
    private AsyncOperationHandle<GameFeature> handle;

    public void Init()
    {
        if (handle.IsValid()) return;

        handle = GameFeatureAssetReference.LoadAssetAsync<GameFeature>();
        handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameFeature = handle.Result;
            GameFeature.Activate();
        }
        else
        {
            Debug.LogError("Failed to load GameFeature.");
        }
    }

    public void DeInit()
    {
        if (!handle.IsValid()) return;

        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameFeature.Deactivate();
            Addressables.Release(handle);
        }
    }
}
