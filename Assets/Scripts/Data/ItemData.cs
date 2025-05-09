using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    private ItemDB ItemDB;

    public EItemType Type;
    public string Name;
    public int Price;
    public string Explain;

    public void Init(GameAssetDBEntity Entity)
    {
        ItemDB = GameManager.Instance.GetManager<DataTableManager>()?.DataTableMap.TryGetValue("ItemDB", out var table) == true ? table as ItemDB : null;
        if(ItemDB == null) 
        {
            Debug.LogWarning("[Init] ItemDB is missing. Initialization aborted.");
            return;
        }

        LoadData(Entity.Id);
    }

    //Load default data
    private void LoadData(int GameAssetId)
    {
        ItemDBEntity Entity = ItemDB.GetDataDictionary()[GameAssetId];

        SetData(Entity);
        InitializeComponents(Entity);
    }

    private void SetData(ItemDBEntity Entity)
    {
        Type = Entity.Type;
        Name = Entity.Name;
        Price = Entity.Price;
        Explain = Entity.Explain;
    }

    private void InitializeComponents(ItemDBEntity Entity)
    {
        switch (Entity.Type)
        {
            case EItemType.Weapon:
                WeaponData weaponData = this.transform.AddComponent<WeaponData>();
                weaponData.Init(Entity);
                break;
            case EItemType.Armor:
                //this.transform.AddComponent<ArmorData>();
                break;
            case EItemType.Consumable:
                //this.transform.AddComponent<ConsumableData>();
                break;
        }
    }
}
