using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


public class CharacterData : MonoBehaviour
{
    private CharacterDB CharacterDB;

    public delegate void OnPossessed(PlayerController playerController);
    public delegate void OnUnPossessed();
    public delegate void OnChangedStatValue();

    public OnPossessed OnPossessedEvent;
    public OnUnPossessed OnUnPossessedEvent;
    public OnChangedStatValue OnChangedStatValueEvent;

    [SerializeField]
    private float HP;
    [SerializeField]
    private float SP;
    [SerializeField]
    private int Str;
    [SerializeField]
    private int Dex;
    [SerializeField]
    private int Luck;
    [SerializeField]
    private int Int;

    public void Init(GameAssetDBEntity Entity)
    {
        if (GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        CharacterDB = GameManager.Instance.DataTableManager.DataTableMap["CharacterDB"] as CharacterDB;

        LoadData(Entity.Id);
    }

    private void LoadData(int GameAssetId)
    {
        CharacterDBEntity Entity = CharacterDB.GetDataDictionary()[GameAssetId];

        SetData(Entity);
        InitializeComponents(Entity);
    }

    private void SetData(CharacterDBEntity Entity)
    {
        string statsString = Entity.Stat;
        string[] stats = statsString.Split(';');
        foreach (string stat in stats)
        {
            if (string.IsNullOrEmpty(stat))
            {
                continue;
            }

            string[] parts = stat.Split(':');
            if (parts.Length == 2)
            {
                string statName = parts[0];
                int statValue;
                if (int.TryParse(parts[1], out statValue))
                {
                    FieldInfo fieldInfo = GetType().GetField(statName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    fieldInfo.SetValue(this, statValue);
                }
                else
                {
                    Debug.LogWarning("Invalid stat value: " + parts[1]);
                }
            }
            else
            {
                Debug.LogWarning("Invalid stat format: " + stat);
            }
        }

        OnChangedStatValueEvent?.Invoke();
    }

    private void InitializeComponents(CharacterDBEntity Entity)
    {
        switch (Entity.Type)
        {
            case ECharacterType.PC:
                break;
            case ECharacterType.NPC:
                break;
        }
    }

    private void OnValidate()
    {
        OnChangedStatValueEvent?.Invoke();
    }
}