using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class InputManager : ManagerBase
{
    private InputActionAsset InputActions;

    public override void Init()
    {
        InputActions = GameManager.Instance.GetManager<GameFeatureManager>()?.GameFeature?.InputActions;
        if(InputActions == null)
        {
            Debug.LogWarning("[Init] InputActions is missing. Initialization aborted.");
            return;
        }

        ActivateAllInput();
    }

    public void ActivateAllInput()
    {
        InputActions.Enable();
    }

    public void ActivateInput(string actionMapName)
    {
        var actionMap = InputActions.FindActionMap(actionMapName);
        actionMap.Enable();
    }

    public void DeactivateAllInput()
    {
        InputActions.Disable();
    }

    public void DeactivateInput(string actionMapName)
    {
        var actionMap = InputActions.FindActionMap(actionMapName);
        actionMap.Disable();
    }

    public void BindFunction(string actionMapName, string actionName, Action<CallbackContext> Callback)
    {
        var actionMap = InputActions.FindActionMap(actionMapName);

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

        action.performed += Callback;
        action.canceled += Callback;
    }

    public void UnBindFunction(string actionMapName, string actionName, Action<CallbackContext> Callback)
    {
        var actionMap = InputActions.FindActionMap(actionMapName);

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

        action.performed -= Callback;
        action.canceled -= Callback;
    }

    public unsafe T GetInputActionValue<T>(string actionMapName, string actionName)
    {
        var actionMap = InputActions.FindActionMap(actionMapName);

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
