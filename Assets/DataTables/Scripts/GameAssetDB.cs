using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GameAssetDB : ScriptableObject
{
	public List<GameAssetDBEntity> Entities;
    private Dictionary<int, GameAssetDBEntity> EntitiesMap;

    public Dictionary<int, GameAssetDBEntity> GetDataDictionary()
    {
        if(EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, GameAssetDBEntity>();
            foreach (GameAssetDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.Id, Entity);
            }
        }

        return EntitiesMap;
    }
}
