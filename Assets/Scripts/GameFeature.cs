using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]
public class GameFeature : ScriptableObject
{
    public List<GameFeatureAction> GameFeatureActions = new List<GameFeatureAction>();

    public void Activate()
    {
        foreach (GameFeatureAction action in GameFeatureActions)
        {
            action.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (GameFeatureAction action in GameFeatureActions)
        {
            action.Deactivate();
        }
    }
}
