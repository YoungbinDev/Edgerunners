using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    private PlayerController PlayerController;

    public void OnPossessed(PlayerController playerController)
    {
        PlayerController = playerController;
    }

    public void UnPossessed()
    {
        PlayerController = null;
    }
}
