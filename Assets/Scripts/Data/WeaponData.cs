using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    private WeaponDB WeaponDB;

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

        LoadData(Entity.GameAssetId);
    }

    private void LoadData(int GameAssetId)
    {
        WeaponDBEntity Entity = WeaponDB.GetDataDictionary()[GameAssetId];

        SetData(Entity);
    }

    private void SetData(WeaponDBEntity Entity)
    {
        DamagePerAttack = Entity.DefaultDamagePerAttack;
        TimePerAttack = Entity.DefaultTimePerAttack;
        AttackRange = Entity.DefaultAttackRange;
    }
}
