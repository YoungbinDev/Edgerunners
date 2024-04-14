using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager InputManager;

    public delegate void OnChangedInputActionValueEvent(string actionName, object value);
    public delegate void OnPossessBy(GameObject character);

    private Dictionary<string, OnChangedInputActionValueEvent> OnChangedPlayerInputActionValueEventMap = new Dictionary<string, OnChangedInputActionValueEvent>();
    public OnPossessBy OnPossessByEvent;

    [SerializeField]
    private GameObject PossessCharacter;
    [SerializeField]
    private bool IsBlockInput;

    private void Start()
    {
        InputManager = GameManager.Instance.InputManager;
        InputManager.BindFunction("Player", "Movement", OnChangedInputActionValue);
        InputManager.BindFunction("Player", "Jump", OnChangedInputActionValue);
    }

    public void SetPossessCharacter(GameObject character)
    {
        if(character.GetComponent<CharacterData>() == null)
        {
            return;
        }

        PossessCharacter = character;
        OnPossessByEvent.Invoke(character);
    }

    public GameObject GetPossessCharacter()
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

        OnChangedPlayerInputActionValueEventMap[actionName].Invoke(actionName, value);
    }
}
