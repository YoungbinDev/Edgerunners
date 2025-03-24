using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class StringUIDB : ScriptableObject
{
    public List<StringUIDBEntity> Entities;
    private Dictionary<string, StringUIDBEntity> EntitiesMap;

    public Dictionary<string, StringUIDBEntity> GetDataDictionary()
    {
        if (EntitiesMap == null)
        {
            EntitiesMap = new Dictionary<string, StringUIDBEntity>();
            foreach (StringUIDBEntity Entity in Entities)
            {
                EntitiesMap.Add(Entity.StringId, Entity);
            }
        }

        return EntitiesMap;
    }
}
