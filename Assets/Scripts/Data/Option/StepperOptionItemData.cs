using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StepperOptionItemData_", menuName = "Scriptable Object/Option Item Data/Stepper")]
public class StepperOptionItemData : OptionItemData
{
    public List<string> Items = new List<string>();
    public int DefaultItemIndex = 0;
}