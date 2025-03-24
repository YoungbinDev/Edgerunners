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
    public string Name;
    public ECharacterGender Gender;
    public string Stat;
}

[System.Serializable]
public class ItemDBEntity
{
    public int GameAssetId;
    public EItemType Type;
    public string Name;
    public int Price;
    public string Explain;
}

[System.Serializable]
public class WeaponDBEntity
{
    public int GameAssetId;
    public float DamagePerAttack;
    public float TimePerAttack;
    public float AttackRange;
    public string StatValueForAdditionalWeaponEffect;
}

[System.Serializable]
public class ArmorDBEntity
{
    public int GameAssetId;
    public float DefenseDamagePerAttack;
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

[System.Serializable]
public class StringUIDBEntity
{
    public string StringId;
    public string Korean;
    public string English;

    public string GetLocalizedString(ELanguageType language)
    {
        switch (language)
        {
            case ELanguageType.Korean:
                return Korean;
            case ELanguageType.English:
                return English;
            default:
                return Korean;
        }
    }
}