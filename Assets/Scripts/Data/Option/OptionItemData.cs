using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOptionWithItems<T>
{
    List<T> Items { get; }
}

public abstract class OptionItemData : ScriptableObject
{
    public EOptionId OptionId;
    public string LabelStringId;

    public abstract object GetDefaultValue();
}