using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EOptionId
{
    None,
    Language,
    Resolution,
    MouseSensitivity,
    GraphicsQuality,
}

[Serializable]
public class OptionGroupData
{
    public string GroupName;
    public List<OptionItemData> Options = new();
}

[CreateAssetMenu(fileName = "OptionData_", menuName = "Scriptable Object/Option Data")]
public class OptionData : ScriptableObject
{
    public List<OptionGroupData> Groups = new();
}