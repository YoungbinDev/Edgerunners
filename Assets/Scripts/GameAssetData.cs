using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameAssetData : MonoBehaviour
{
    [SerializeField]
    private int AssetKey;
    private GameAssetDBEntity Entity;

    public void Init(int AssetKey, GameAssetDBEntity Entity)
    {
        this.AssetKey = AssetKey;
        this.Entity = Entity;

        LoadData();
    }

    public void LoadData()
    {
        switch(Entity.Type)
        {
            case EGameAssetType.Character:
                {
                    CharacterData characterData = this.transform.AddComponent<CharacterData>();
                    characterData.Init(Entity);
                    break;
                }
                
            case EGameAssetType.Item:
                {
                    break;
                }
        }
    }
}
