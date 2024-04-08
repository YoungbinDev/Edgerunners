using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CharacterDB : ScriptableObject
{
	public List<CharacterDBEntity> Entities;
    private Dictionary<int, CharacterDBEntity> EntitiesMap;

    public Dictionary<int, CharacterDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<int, CharacterDBEntity>();
            foreach (CharacterDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.GameAssetId, Entity);
            }
        }

        return EntitiesMap;
    }
}
