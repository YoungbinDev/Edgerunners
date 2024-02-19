using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GameFeature : ScriptableObject
{
    [HideInInspector]
    public GameFeatureActionType currentActionType;

    [SerializeField]
    public List<ActionData> actionDataList = new List<ActionData>();

    [Serializable]
    public struct ActionData
    {
        [SerializeField, ReadOnly]
        private GameFeatureActionType actionType;
        [SerializeReference]
        public GameFeatureAction gameFeatureAction;

        public ActionData(GameFeatureActionType actionType, GameFeatureAction gameFeatureAction)
        {
            this.actionType = actionType;
            this.gameFeatureAction = gameFeatureAction;
        }
    }

    public void Activate()
    {
        foreach (ActionData actionData in actionDataList)
        {
            actionData.gameFeatureAction.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (ActionData actionData in actionDataList)
        {
            actionData.gameFeatureAction.Deactivate();
        }
    }

    public void AddAction(GameFeatureActionType actionType)
    {
        switch (actionType)
        {
            case GameFeatureActionType.SpawnPrefab:                
                actionDataList.Add(new ActionData(currentActionType, new GameFeatureAction_SpawnPrefab()));
                break;
            case GameFeatureActionType.AddComponent:
                actionDataList.Add(new ActionData(currentActionType, new GameFeatureAction_AddComponent()));
                break;
        }
    }
}
