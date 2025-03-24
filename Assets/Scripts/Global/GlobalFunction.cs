using System.Collections;
using System.Collections.Generic;
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
}
