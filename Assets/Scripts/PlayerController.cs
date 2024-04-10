using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager InputManager;

    public delegate void OnChangedInputActionValueEvent(string actionName, object value);

    private Dictionary<string, OnChangedInputActionValueEvent> OnChangedPlayerInputActionValueEventMap = new Dictionary<string, OnChangedInputActionValueEvent>();

    [SerializeField]
    private bool IsBlockInput;

    private void Start()
    {
        InputManager = GameManager.Instance.InputManager;
        InputManager.BindFunction("Player", "Movement", OnChangedInputActionValue);
        InputManager.BindFunction("Player", "Jump", OnChangedInputActionValue);
    }

    public void AddCallbackToOnChangedInputActionValueEvent(string actionName, OnChangedInputActionValueEvent Callback)
    {
        if (!OnChangedPlayerInputActionValueEventMap.ContainsKey(actionName))
        {
            OnChangedPlayerInputActionValueEventMap.Add(actionName, null);
        }

        OnChangedPlayerInputActionValueEventMap[actionName] += Callback;
    }

    private void OnChangedInputActionValue(string actionName, object value)
    {
        if(IsBlockInput)
        {
            return;
        }

        if(!OnChangedPlayerInputActionValueEventMap.ContainsKey(actionName))
        {
            return;
        }

        OnChangedPlayerInputActionValueEventMap[actionName].Invoke(actionName, value);
    }
}
