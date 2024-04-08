using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct FCharacterStat
{
    public int Str;
    public int Dex;
    public int Luck;
    public int Int;
}

public class CharacterData : MonoBehaviour
{
    private CharacterDB CharacterDB;
    private CharacterDBEntity Entity;

    [SerializeField]
    FCharacterStat Stat;

    public void Init(GameAssetDBEntity Entity)
    {
        LoadData(Entity.Id);
    }

    private void LoadData(int GameAssetId)
    {
        if (GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        CharacterDB = GameManager.Instance.DataTableManager.DataTableMap["CharacterDB"] as CharacterDB;
        Entity = CharacterDB.GetDataDictionary()[GameAssetId];

        LoadStat();
    }

    private void LoadStat()
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
                        case "Str":
                            Stat.Str = statValue;
                            break;
                        case "Dex":
                            Stat.Dex = statValue;
                            break;
                        case "Luck":
                            Stat.Luck = statValue;
                            break;
                        case "Int":
                            Stat.Int = statValue;
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
}