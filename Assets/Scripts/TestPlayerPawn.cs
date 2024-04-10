using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerPawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.PlayerController.AddCallbackToOnChangedInputActionValueEvent("Movement", OnMove);
        GameManager.Instance.PlayerController.AddCallbackToOnChangedInputActionValueEvent("Jump", OnJump);
    }

    void OnJump(string actionName, object value)
    {
        if(value == null)
        {
            return;
        }

        Debug.Log("Jump");
    }

    void OnMove(string actionName, object value)
    {
        Vector2 moveInput = value != null ? (Vector2)value : Vector2.zero;
        Debug.Log(moveInput);
    }
}
