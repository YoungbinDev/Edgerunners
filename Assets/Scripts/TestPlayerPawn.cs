using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerPawn : MonoBehaviour
{
    private CharacterController CharacterController;

    [SerializeField]
    private float PlayerSpeed = 2.0f;
    private Vector2 MoveInput;

    private PlayerController PlayerController;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerController = GameManager.Instance.PlayerController;
        PlayerController.AddCallbackToOnChangedInputActionValueEvent("Movement", OnMove);
        PlayerController.AddCallbackToOnChangedInputActionValueEvent("Jump", OnJump);

        CharacterController = GetComponent<CharacterController>();
    }

    private void OnDestroy()
    {
        if(PlayerController != null)
        {
            PlayerController.RemoveCallbackToOnChangedInputActionValueEvent("Movement", OnMove);
            PlayerController.RemoveCallbackToOnChangedInputActionValueEvent("Jump", OnJump);
        }
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(MoveInput.x, 0f, MoveInput.y);

        CharacterController.Move(moveDirection * Time.deltaTime * PlayerSpeed);
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
        MoveInput = value != null ? (Vector2)value : Vector2.zero;
    }
}
