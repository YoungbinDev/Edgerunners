using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : MonoBehaviour
{
    public Dictionary<string, ScriptableObject> DataTableMap;

    public void Init()
    {
        if (GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        if (GameManager.Instance.GameFeatureManager.GameFeature == null)
        {
            return;
        }

        DataTableMap = GameManager.Instance.GameFeatureManager.GameFeature.GetDataDictionary();
    }
}
