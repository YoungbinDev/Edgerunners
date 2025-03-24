using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameFeatureManager GameFeatureManager;
    public DataTableManager DataTableManager;
    public GameAssetManager GameAssetManager;
    public OptionManager OptionManager;
    public InputManager InputManager;

    public PlayerController PlayerController;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //TODO: null 체크
        GameFeatureManager.Init();
        DataTableManager.Init();
        GameAssetManager.Init();
        OptionManager.Init();
        InputManager.Init();
    }
}
