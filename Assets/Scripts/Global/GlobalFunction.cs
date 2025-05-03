using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GlobalFunction : MonoBehaviour
{
    public static Transform FindCharacterTransformFromParents(Transform transform)
    {
        Transform parentTransform = transform.parent;

        while (parentTransform != null)
        {
            CharacterData characterDataComponent = parentTransform.GetComponent<CharacterData>();

            if (characterDataComponent != null)
            {
                return parentTransform;
            }

            parentTransform = parentTransform.parent;
        }

        return null;
    }

    public static List<T> GetOptionItems<T>(OptionData optionData, EOptionId optionId)
    {
        var option = optionData.Groups
            .SelectMany(group => group.Options)
            .FirstOrDefault(opt => opt.OptionId == optionId);
        if (option is IOptionWithItems<T> optionWithItems)
        {
            return optionWithItems.Items;
        }

        Debug.LogWarning($"[GlobalFunction] Option {optionId} is not IOptionWithItems<{typeof(T).Name}>");
        return null;
    }

    public static T GetOptionDefaultValue<T>(OptionData optionData, EOptionId optionId)
    {
        var option = optionData.Groups
            .SelectMany(group => group.Options)
            .FirstOrDefault(opt => opt.OptionId == optionId);
        if (option.GetDefaultValue() is T typedValue)
        {
            return typedValue;
        }

        Debug.LogWarning($"[GlobalFunction] Option {optionId} is not {typeof(T).Name}");
        return default;
    }

    public static string ConvertStringIdToText(string stringId)
    {
        var optionManager = GameManager.Instance.GetManager<OptionManager>();
        var dataTableManager = GameManager.Instance.GetManager<DataTableManager>();

        if (dataTableManager == null || optionManager == null)
        {
            Debug.LogWarning("[ConvertStringIdToText] DataTableManager 또는 OptionManager가 없습니다.");
            return stringId;
        }

        var stringUIDB = dataTableManager.DataTableMap["StringUIDB"] as StringUIDB;
        if (stringUIDB == null)
        {
            Debug.LogWarning("[ConvertStringIdToText] StringUIDB를 찾을 수 없습니다.");
            return stringId;
        }

        var dataDict = stringUIDB.GetDataDictionary();
        if (dataDict == null)
        {
            Debug.LogWarning("[ConvertStringIdToText] StringUIDB 데이터가 없습니다.");
            return stringId;
        }

        if (!dataDict.TryGetValue(stringId, out var stringUIData))
        {
            // ID를 못 찾으면 stringId 그대로 반환
            Debug.LogWarning($"[ConvertStringIdToText] StringId '{stringId}'를 찾을 수 없습니다.");
            return stringId;
        }

        ELanguageType languageType = (ELanguageType)optionManager.GetValue<int>(EOptionId.Language);
        return stringUIData.GetLocalizedString(languageType);
    }
}
