using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        inputManager = InputManager.Instance;
        inputManager.BindFunction("Jump", UnityEngine.InputSystem.InputActionPhase.Started, ctx => OnJump());
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Debug.Log(groundedPlayer);

        Vector2 moveInput = inputManager.GetInputActionValue<Vector2>("Movement");
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(moveDirection * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnJump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}