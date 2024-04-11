using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GameAssetData : MonoBehaviour
{
    [SerializeField]
    private int AssetKey;

    public int Id;
    public EGameAssetType Type;

    public void Init(int AssetKey, GameAssetDBEntity Entity)
    {
        this.AssetKey = AssetKey;

        SetData(Entity);
        InitializeComponents(Entity);
    }

    private void SetData(GameAssetDBEntity Entity)
    {
        Id = Entity.Id;
        Type = Entity.Type;
    }

    private void InitializeComponents(GameAssetDBEntity Entity)
    {
        switch (Entity.Type)
        {
            case EGameAssetType.Character:
                {
                    CharacterData characterData = this.transform.AddComponent<CharacterData>();
                    characterData.Init(Entity);
                    break;
                }

            case EGameAssetType.Item:
                {
                    ItemData itemData = this.transform.AddComponent<ItemData>();
                    itemData.Init(Entity);
                    break;
                }
        }
    }
}
