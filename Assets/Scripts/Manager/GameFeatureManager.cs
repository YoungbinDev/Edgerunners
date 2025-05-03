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

public class GameFeatureManager : ManagerBase
{
    [SerializeField] 
    private AssetReferenceGameFeature GameFeatureAssetRef;

    [HideInInspector]
    public GameFeature GameFeature;
    private AsyncOperationHandle<GameFeature> handle;

    public override void Init()
    {
        if(GameFeatureAssetRef == null)
        {
            Debug.LogError("[Init] GameFeatureAssetRef is missing. Initialization aborted.");
            return;
        }

        if (handle.IsValid())
        {
            Debug.LogWarning("[Init] handle is already running.");
            return;
        }

        handle = GameFeatureAssetRef.LoadAssetAsync<GameFeature>();
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
