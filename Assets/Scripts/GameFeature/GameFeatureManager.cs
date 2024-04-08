using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeatureManager : MonoBehaviour
{
    public GameFeature GameFeature;

    public void Init()
    {
        GameFeature.Activate();
    }

    public void DeInit()
    {
        GameFeature.Deactivate();
    }
}
