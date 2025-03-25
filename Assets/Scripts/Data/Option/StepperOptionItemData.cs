using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "StepperOptionItemData_", menuName = "Scriptable Object/Option Item Data/Stepper")]
public class StepperOptionItemData : OptionItemData
{
    public List<string> Items = new List<string>();
    public int DefaultValue = 0;

    public override object GetDefaultValue() => DefaultValue;
}