using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SliderOptionItemData_", menuName = "Scriptable Object/Option Item Data/Slider")]
public class SliderOptionItemData : OptionItemData
{
    public float MinValue;
    public float DefaultValue;
    public float MaxValue;

    public override object GetDefaultValue() => DefaultValue;
}