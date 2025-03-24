using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StringUIText : MonoBehaviour
{
    public string StringId;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(Application.isPlaying)
        {
            return;
        }

        SetStringUI(StringId);
    }
#endif

    void Start()
    {
        SetStringUI(StringId);
    }

    public void SetStringUI(string stringId)
    {
        StringId = stringId;

        if (Application.isPlaying)
        {
            if (!IsValid(stringId))
            {
                this.GetComponent<TextMeshProUGUI>().text = stringId;
                return;
            }

            //this.GetComponent<TextMeshProUGUI>().text = (GameManager.Instance.DataTableManager.DataTableMap["StringUIDB"] as StringUIDB).GetDataDictionary()[stringId].GetLocalizedString(GameManager.Instance.GameFeatureManager.GameFeature.OptionData.Groups["General"].Options.LanguageType);
        }
        else
        {
            var StringUIDB = AssetDatabase.LoadAssetAtPath<StringUIDB>("Assets/DataTables/StringUIDB.asset");
            if (StringUIDB == null)
            {
                this.GetComponent<TextMeshProUGUI>().text = stringId;
                return;
            }

            if (!StringUIDB.GetDataDictionary().ContainsKey(StringId))
            {
                this.GetComponent<TextMeshProUGUI>().text = stringId;
                return;
            }

            this.GetComponent<TextMeshProUGUI>().text = StringUIDB.GetDataDictionary()[StringId].GetLocalizedString(ELanguageType.English);
        }
    }

    private bool IsValid(string stringId)
    {
        if (stringId == null)
        {
            return false;
        }

        if (GameManager.Instance.DataTableManager == null)
        {
            return false;
        }

        if (GameManager.Instance.DataTableManager.DataTableMap == null)
        {
            return false;
        }

        if (!GameManager.Instance.DataTableManager.DataTableMap.ContainsKey("StringUIDB"))
        {
            return false;
        }

        if (!(GameManager.Instance.DataTableManager.DataTableMap["StringUIDB"] as StringUIDB).GetDataDictionary().ContainsKey(stringId))
        {
            return false;
        }

        if(GameManager.Instance.GameFeatureManager.GameFeature == null)
        {
            return false;
        }

        if(GameManager.Instance.GameFeatureManager.GameFeature.OptionData == null)
        {
            return false;
        }

        return true;
    }
}
