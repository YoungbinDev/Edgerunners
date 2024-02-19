using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


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

    [SerializeField]
    private InputActionAsset inputActions;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void BindFunction(string actionMapName, string actionName, InputActionPhase actionPhase, Action<InputAction.CallbackContext> callbackContext)
    {
        var actionMap = inputActions.FindActionMap(actionMapName);

        if (actionMap == null)
        {
            Debug.LogWarning($"ActionMap '{actionMap}' not found.");
            return;
        }

        var action = actionMap.FindAction(actionName);

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

    public unsafe T GetInputActionValue<T>(string actionMapName, string actionName)
    {
        var actionMap = inputActions.FindActionMap(actionMapName);

        if (actionMap == null)
        {
            Debug.LogWarning($"ActionMap '{actionMap}' not found.");
            return default(T);
        }

        var action = actionMap.FindAction(actionName);

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
