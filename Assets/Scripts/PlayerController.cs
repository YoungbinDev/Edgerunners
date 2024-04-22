using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager InputManager;

    public delegate void OnChangedInputActionValueEvent(string actionName, object value);
    public delegate void OnPossess(PlayableCharacter character);
    public delegate void OnUnPossess(PlayableCharacter character);

    private Dictionary<string, OnChangedInputActionValueEvent> OnChangedPlayerInputActionValueEventMap = new Dictionary<string, OnChangedInputActionValueEvent>();
    public OnPossess OnPossessEvent;
    public OnUnPossess OnUnPossessEvent;

    [SerializeField]
    private PlayableCharacter PossessCharacter;
    [SerializeField]
    private bool IsBlockInput;

    private void Start()
    {
        InputManager = GameManager.Instance.InputManager;
        InputManager.BindFunction("Player", "Movement", OnChangedInputActionValue);
        InputManager.BindFunction("Player", "Jump", OnChangedInputActionValue);
    }

    private void OnDestroy()
    {
        if(InputManager != null)
        {
            InputManager.UnBindFunction("Player", "Movement", OnChangedInputActionValue);
            InputManager.UnBindFunction("Player", "Jump", OnChangedInputActionValue);
        }
    }

    public void SetPossessCharacter(PlayableCharacter character)
    {
        if (PossessCharacter != null)
        {
            PossessCharacter.UnPossessed();
            OnUnPossessEvent?.Invoke(PossessCharacter);
            PossessCharacter = null;
        }

        if(character == null)
        {
            return;
        }

        PossessCharacter = character;
        PossessCharacter.OnPossessed(this);
        OnPossessEvent?.Invoke(character);
    }

    public PlayableCharacter GetPossessCharacter()
    {
        return PossessCharacter;
    }

    public void AddCallbackToOnChangedInputActionValueEvent(string actionName, OnChangedInputActionValueEvent Callback)
    {
        if (!OnChangedPlayerInputActionValueEventMap.ContainsKey(actionName))
        {
            OnChangedPlayerInputActionValueEventMap.Add(actionName, null);
        }

        OnChangedPlayerInputActionValueEventMap[actionName] += Callback;
    }

    public void RemoveCallbackToOnChangedInputActionValueEvent(string actionName, OnChangedInputActionValueEvent Callback)
    {
        if (!OnChangedPlayerInputActionValueEventMap.ContainsKey(actionName))
        {
            return;
        }

        OnChangedPlayerInputActionValueEventMap[actionName] -= Callback;
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

        OnChangedPlayerInputActionValueEventMap[actionName]?.Invoke(actionName, value);
    }
}
