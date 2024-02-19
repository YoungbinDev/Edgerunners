public enum GameFeatureActionType
{
    None = 0,
    SpawnPrefab = 1,
    AddComponent = 2,
}


[System.Serializable]
public class GameFeatureAction
{
    public virtual void Activate() { }
    public virtual void Deactivate() { }
}
