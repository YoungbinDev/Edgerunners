using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    protected PlayerController PlayerController;

    protected virtual void BindInputSettings() { }
    protected virtual void UnBindInputSettings() { }

    protected virtual void OnDestroy()
    {
        if (PlayerController != null)
        {
            PlayerController.SetPossessCharacter(null);
        }
    }

    public virtual void OnPossessed(PlayerController playerController)
    {
        PlayerController = playerController;
        BindInputSettings();
    }

    public virtual void OnUnPossessed()
    {
        UnBindInputSettings();
        PlayerController = null;
    }
}
