using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    public abstract void Init();
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private List<ManagerBase> Managers;

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

        foreach (var manager in Managers)
        {
            manager?.Init();
        }
    }

    public T GetManager<T>() where T : ManagerBase
    {
        return Managers.OfType<T>().FirstOrDefault();
    }
}
