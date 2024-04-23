using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnPossess(PlayableCharacter character);
    public delegate void OnUnPossess(PlayableCharacter character);

    public OnPossess OnPossessEvent;
    public OnUnPossess OnUnPossessEvent;

    [SerializeField]
    private PlayableCharacter PossessCharacter;

    public void SetPossessCharacter(PlayableCharacter character)
    {
        if (PossessCharacter != null)
        {
            PossessCharacter.OnUnPossessed();
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
}
