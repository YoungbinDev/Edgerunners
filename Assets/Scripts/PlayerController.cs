using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnPossess(Pawn character);
    public delegate void OnUnPossess(Pawn character);

    public OnPossess OnPossessEvent;
    public OnUnPossess OnUnPossessEvent;

    [SerializeField]
    private Pawn PossessCharacter;

    public void SetPossessCharacter(Pawn character)
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

    public Pawn GetPossessCharacter()
    {
        return PossessCharacter;
    }
}
