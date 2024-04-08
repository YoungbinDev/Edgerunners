using UnityEngine;


public class GameFeatureAction_SpawnPrefab : GameFeatureAction
{
    public GameObject prefab;

    public override void Activate()
    {
        GameObject.Instantiate(prefab);
    }

    public override void Deactivate() 
    { 
    }
}
