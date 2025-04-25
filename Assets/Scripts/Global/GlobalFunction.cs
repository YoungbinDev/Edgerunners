using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        Debug.LogWarning($"[OptionHelper] Option {optionId} is not IOptionWithItems<{typeof(T).Name}>");
        return null;
    }
}
