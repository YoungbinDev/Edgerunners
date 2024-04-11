[System.Serializable]
public class GameAssetDBEntity
{
    public int Id;
    public EGameAssetType Type;
    public string AssetPath3D;
    public string AssetName3D;
    public string AssetPath2D;
    public string AssetName2D;
}

[System.Serializable]
public class CharacterDBEntity
{
    public int GameAssetId;
    public ECharacterType Type;
    public string DefaultName;
    public ECharacterGender DefaultGender;
    public string DefaultStat;
}

[System.Serializable]
public class ItemDBEntity
{
    public int GameAssetId;
    public EItemType Type;
    public string DefaultName;
    public int DefaultPrice;
    public string DefaultExplain;
}

[System.Serializable]
public class WeaponDBEntity
{
    public int GameAssetId;
    public float DefaultDamagePerAttack;
    public float DefaultTimePerAttack;
    public float DefaultAttackRange;
    public string StatValueForAdditionalWeaponEffect;
}

[System.Serializable]
public class ArmorDBEntity
{
    public int GameAssetId;
    public float DefaultDefenseDamagePerAttack;
    public string AdditionalStat;
}

[System.Serializable]
public class ConsumableDBEntity
{
    public int GameAssetId;
    public string AdditionalStat;
    public float Duration;
    public float Cooldown;
}