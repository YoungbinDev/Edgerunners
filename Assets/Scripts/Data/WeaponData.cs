using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class WeaponData : MonoBehaviour
{
    private WeaponDB WeaponDB;
    private Transform Owner;
    private CharacterData CharacterData;

    private float DefaultDamagePerAttack;
    private float DefaultTimePerAttack;
    private float DefaultAttackRange;

    public float DamagePerAttack;
    public float TimePerAttack;
    public float AttackRange;

    private string StatValueForAdditionalWeaponEffect;

    public void Init(ItemDBEntity Entity)
    {
        if (GameManager.Instance.GameFeatureManager == null)
        {
            return;
        }

        WeaponDB = GameManager.Instance.DataTableManager.DataTableMap["WeaponDB"] as WeaponDB;

        Owner = GlobalFunction.FindCharacterTransformFromParents(transform);
        if(Owner != null)
        {
            CharacterData = Owner.GetComponent<CharacterData>();
            CharacterData.OnChangedStatValueEvent += OnChangedCharacterDataStatValue;
        }
        
        LoadData(Entity.GameAssetId);
    }

    //Load default data
    private void LoadData(int GameAssetId)
    {
        WeaponDBEntity Entity = WeaponDB.GetDataDictionary()[GameAssetId];

        SetDefaultData(Entity);
        SetData(Entity);
        ApplyCharacterData();
    }

    private void SetDefaultData(WeaponDBEntity Entity)
    {
        DefaultDamagePerAttack = Entity.DamagePerAttack;
        DefaultTimePerAttack = Entity.TimePerAttack;
        DefaultAttackRange = Entity.AttackRange;

        StatValueForAdditionalWeaponEffect = Entity.StatValueForAdditionalWeaponEffect;
    }

    private void SetData(WeaponDBEntity Entity)
    {
        DamagePerAttack = Entity.DamagePerAttack;
        TimePerAttack = Entity.TimePerAttack;
        AttackRange = Entity.AttackRange;
    }

    private void OnChangedCharacterDataStatValue()
    {
        ResetData();
        ApplyCharacterData();
    }

    private void ResetData()
    {
        DamagePerAttack = DefaultDamagePerAttack;
        TimePerAttack = DefaultTimePerAttack;
        AttackRange = DefaultAttackRange;
    }

    private void ApplyCharacterData()
    {
        if(CharacterData == null)
        {
            return;
        }

        string statsString = StatValueForAdditionalWeaponEffect;
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
                    FieldInfo characterStatFieldInfo = CharacterData.GetType().GetField(characterStatName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    int characterStat = 0;
                    characterStat = (int)characterStatFieldInfo.GetValue(CharacterData);

                    string weaponStatName = statNames[1];
                    FieldInfo weaponStatFieldInfo = GetType().GetField(weaponStatName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    float weaponStat = 0;
                    weaponStat = (float)weaponStatFieldInfo.GetValue(this);

                    for(int i = 1; i <= characterStat; ++i)
                    {
                        weaponStat += weaponStat / 100 * statValue;
                    }

                    weaponStatFieldInfo.SetValue(this, weaponStat);
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
