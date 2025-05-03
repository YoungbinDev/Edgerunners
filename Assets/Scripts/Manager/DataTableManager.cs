using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : ManagerBase
{
    public Dictionary<string, ScriptableObject> DataTableMap;

    public override void Init()
    {
        DataTableMap = GameManager.Instance.GetManager<GameFeatureManager>()?.GameFeature?.GetDataDictionary();
        if(DataTableMap == null)
        {
            Debug.LogWarning("[Init] DataTableMap is missing. Initialization aborted.");
            return;
        }
    }
}
