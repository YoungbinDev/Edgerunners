using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void BindFunction(string actionName, InputActionPhase actionPhase, Action<InputAction.CallbackContext> callbackContext)
    {
        var action = playerControls.FindAction(actionName);

        if (action == null)
        {
            Debug.LogWarning($"Action '{actionName}' not found.");
            return;
        }

        switch (actionPhase)
        {
            case InputActionPhase.Started:
                action.started += callbackContext;
                break;
            case InputActionPhase.Performed:
                action.performed += callbackContext;
                break;
            case InputActionPhase.Canceled:
                action.canceled += callbackContext;
                break;
        }
    }

    public unsafe T GetInputActionValue<T>(string actionName)
    {
        var action = playerControls.FindAction(actionName);
    
        if (action == null)
        {
            Debug.LogWarning($"Action '{actionName}' not found.");
            return default(T);
        }

        object actionObj = action.ReadValueAsObject();
        if (actionObj == null)
        {
            return default(T);
        }
       
        return actionObj.ConvertTo<T>();
    }
}
