using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ConsumableDB : ScriptableObject
{
	public List<ConsumableDBEntity> Entities;
    private Dictionary<int, ConsumableDBEntity> EntitiesMap;

    public Dictionary<int, ConsumableDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, ConsumableDBEntity>();
            foreach (ConsumableDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.GameAssetId, Entity);
            }
        }

        return EntitiesMap;
    }
}
