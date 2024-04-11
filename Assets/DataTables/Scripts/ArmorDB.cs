using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ArmorDB : ScriptableObject
{
	public List<ArmorDBEntity> Entities;
    private Dictionary<int, ArmorDBEntity> EntitiesMap;

    public Dictionary<int, ArmorDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, ArmorDBEntity>();
            foreach (ArmorDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.GameAssetId, Entity);
            }
        }

        return EntitiesMap;
    }
}
