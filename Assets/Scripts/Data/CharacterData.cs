using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CharacterData : MonoBehaviour
{
    private CharacterDB CharacterDB;

    public float HP;
    public float SP;
    public int Str;
    public int Dex;
    public int Luck;
    public int Int;

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
        string statsString = Entity.DefaultStat;
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
                    switch (statName)
                    {
                        case "HP":
                            HP = statValue;
                            break;
                        case "SP":
                            SP = statValue;
                            break;
                        case "Str":
                            Str = statValue;
                            break;
                        case "Dex":
                            Dex = statValue;
                            break;
                        case "Luck":
                            Luck = statValue;
                            break;
                        case "Int":
                            Int = statValue;
                            break;
                        default:
                            Debug.LogWarning("Unknown stat: " + statName);
                            break;
                    }
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
}