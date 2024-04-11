using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ItemDB : ScriptableObject
{
	public List<ItemDBEntity> Entities;
    private Dictionary<int, ItemDBEntity> EntitiesMap;

    public Dictionary<int, ItemDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, ItemDBEntity>();
            foreach (ItemDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.GameAssetId, Entity);
            }
        }

        return EntitiesMap;
    }
}
