using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DropdownOptionItemData_", menuName = "Scriptable Object/Option Item Data/Dropdown")]
public class DropdownOptionItemData : OptionItemData
{
    public List<string> Items = new List<string>();
    public int DefaultValue = 0;

    public override object GetDefaultValue() => DefaultValue;
}