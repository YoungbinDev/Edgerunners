using System;
using UnityEngine;

public class GameFeatureAction_AddComponent : GameFeatureAction
{
    public GameObject targetObject;
    public string componentTypeName;

    public override void Activate()
    {
        //Type t = Type.GetType(componentTypeName);

        //if(targetObject.GetComponent(t) != null)
        //{
        //    return;
        //}

        //targetObject.AddComponent(t);
    }

    public override void Deactivate()
    {
    }
}
