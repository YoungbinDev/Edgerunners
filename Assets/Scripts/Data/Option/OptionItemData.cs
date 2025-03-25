using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OptionItemData : ScriptableObject
{
    public EOptionId OptionId;
    public string LabelStringId;

    public abstract object GetDefaultValue();
}