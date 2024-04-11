using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class WeaponDB : ScriptableObject
{
	public List<WeaponDBEntity> Entities;
    private Dictionary<int, WeaponDBEntity> EntitiesMap;

    public Dictionary<int, WeaponDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, WeaponDBEntity>();
            foreach (WeaponDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.GameAssetId, Entity);
            }
        }

        return EntitiesMap;
    }
}
