using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerPawn : Pawn
{
    private InputManager InputManager;
    private CharacterController CharacterController;

    [SerializeField]
    private float PlayerSpeed = 2.0f;
    private Vector2 MoveInput;

    // Start is called before the first frame update
    private void Start()
    {
        InputManager = GameManager.Instance.GetManager<InputManager>();
        if(InputManager == null)
        {
            Debug.LogWarning("[Start] InputManager is missing. Initialization aborted.");
            return;
        }

        CharacterController = GetComponent<CharacterController>();
        if(CharacterController == null)
        {
            Debug.LogWarning("[Start] CharacterController is missing. Initialization aborted.");
            return;
        }

        //TODO: ���� �׽�Ʈ���̹Ƿ� ��������
        GameManager.Instance.PlayerController.SetPossessCharacter(this);
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(MoveInput.x, 0f, MoveInput.y);

        CharacterController.Move(moveDirection * Time.deltaTime * PlayerSpeed);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }

    void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        Debug.Log(context.ReadValue<Vector2>());
    }

    protected override void BindInputSettings()
    {
        InputManager.BindFunction("Player", "Movement", OnMove);
        InputManager.BindFunction("Player", "Jump", OnJump);
    }

    protected override void UnBindInputSettings()
    {
        InputManager.UnBindFunction("Player", "Movement", OnMove);
        InputManager.UnBindFunction("Player", "Jump", OnJump);
    }
}
