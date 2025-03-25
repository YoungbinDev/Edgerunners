using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu]
public class GameFeature : ScriptableObject
{
    [SerializeField]
    private List<ScriptableObject> DBList;
    private Dictionary<string, ScriptableObject> DBMap;

    public List<ActionData> ActionDataList = new List<ActionData>();
    public InputActionAsset InputActions;
    public OptionData OptionData;

    public Dictionary<string, ScriptableObject> GetDataDictionary()
    {
        if (DBMap == null)
        {
            DBMap = new Dictionary<string, ScriptableObject>();
            foreach (ScriptableObject data in DBList)
            {
                DBMap.Add(data.name, data);
            }
        }

        return DBMap;
    }

    [Serializable]
    public struct ActionData
    {
        [SerializeField]
        private GameFeatureActionType ActionType;
        [SerializeReference]
        public GameFeatureAction GameFeatureAction;

        public ActionData(GameFeatureActionType ActionType, GameFeatureAction GameFeatureAction)
        {
            this.ActionType = ActionType;
            this.GameFeatureAction = GameFeatureAction;
        }
    }

    public void Activate()
    {
        foreach (ActionData ActionData in ActionDataList)
        {
            ActionData.GameFeatureAction.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (ActionData ActionData in ActionDataList)
        {
            ActionData.GameFeatureAction.Deactivate();
        }
    }

    public void AddAction(GameFeatureActionType actionType)
    {
        switch (actionType)
        {
            case GameFeatureActionType.SpawnPrefab:
                ActionDataList.Add(new ActionData(actionType, new GameFeatureAction_SpawnPrefab()));
                break;
            case GameFeatureActionType.AddComponent:
                ActionDataList.Add(new ActionData(actionType, new GameFeatureAction_AddComponent()));
                break;
        }
    }
}
