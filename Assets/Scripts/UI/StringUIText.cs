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
        OptionApplier.OnChangedOption += OnChangedOptionValue;

        SetStringUI(StringId);
    }

    void OnDestroy()
    {
        OptionApplier.OnChangedOption -= OnChangedOptionValue;
    }

    private void OnChangedOptionValue(EOptionId optionId, object value)
    {
        if(!optionId.Equals(EOptionId.Language))
        {
            return;
        }

        SetStringUI(StringId);
    }

    public void SetStringUI(string stringId)
    {
        if (StringId == null)
        {
            return;
        }

        StringId = stringId;

        if (Application.isPlaying)
        {
            if (!IsValid(stringId))
            {
                this.GetComponent<TextMeshProUGUI>().text = stringId;
                return;
            }

            ELanguageType languageType = (ELanguageType)GameManager.Instance.OptionManager.GetValue<int>(EOptionId.Language);
            this.GetComponent<TextMeshProUGUI>().text = (GameManager.Instance.DataTableManager.DataTableMap["StringUIDB"] as StringUIDB).GetDataDictionary()[stringId].GetLocalizedString(languageType);
        }
        else
        {
#if UNITY_EDITOR
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
#endif
        }
    }

    private bool IsValid(string stringId)
    {
        var gameManager = GameManager.Instance;
        if (gameManager == null) return false;

        var dataTableMap = gameManager.DataTableManager?.DataTableMap;
        if (dataTableMap == null) return false;

        if (!dataTableMap.TryGetValue("StringUIDB", out var table)) return false;
        if (table is not StringUIDB stringDb) return false;

        if (!stringDb.GetDataDictionary().ContainsKey(stringId)) return false;

        var optionData = gameManager.GameFeatureManager?.GameFeature?.OptionData;
        if (optionData == null) return false;

        return true;
    }
}
