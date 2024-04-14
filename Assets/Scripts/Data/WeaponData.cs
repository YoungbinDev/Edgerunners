using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    private WeaponDB WeaponDB;
    private CharacterData CharacterData;

    public float DamagePerAttack;
    public float TimePerAttack;
    public float AttackRange;

    public void Init(ItemDBEntity Entity)
    {
        if (GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        WeaponDB = GameManager.Instance.DataTableManager.DataTableMap["WeaponDB"] as WeaponDB;

        CharacterData = this.transform.root.GetComponent<CharacterData>();

        LoadData(Entity.GameAssetId);
    }

    private void LoadData(int GameAssetId)
    {
        WeaponDBEntity Entity = WeaponDB.GetDataDictionary()[GameAssetId];

        SetData(Entity);
        ApplyCharacterData(Entity);
    }

    private void SetData(WeaponDBEntity Entity)
    {
        DamagePerAttack = Entity.DamagePerAttack;
        TimePerAttack = Entity.TimePerAttack;
        AttackRange = Entity.AttackRange;
    }

    private void ApplyCharacterData(WeaponDBEntity Entity)
    {
        string statsString = Entity.StatValueForAdditionalWeaponEffect;
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
                string[] statNames = parts[0].Split('/');
                int statValue;
                if (int.TryParse(parts[1], out statValue))
                {
                    string characterStatName = statNames[0];
                    FieldInfo characterStatfieldInfo = CharacterData.GetType().GetField(characterStatName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    int characterStat = 0;
                    characterStat = (int)characterStatfieldInfo.GetValue(CharacterData);

                    string weaponStatName = statNames[1];
                    FieldInfo weaponStatfieldInfo = GetType().GetField(weaponStatName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    float weaponStat = 0;
                    weaponStat = (float)weaponStatfieldInfo.GetValue(this);

                    for(int i = 1; i <= characterStat; ++i)
                    {
                        weaponStat += weaponStat / 100 * statValue;
                    }
                    
                    weaponStatfieldInfo.SetValue(this, weaponStat);
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
