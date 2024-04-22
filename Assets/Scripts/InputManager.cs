using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerController;


public class InputManager : MonoBehaviour
{
    private InputActionAsset InputActions;

    public void Init()
    {
        InputActions = GameManager.Instance.GameFeatureManager.GameFeature.InputActions;
        ActivateInput();
    }

    public void ActivateInput()
    {
        InputActions.Enable();
    }

    public void DeactivateInput()
    {
        InputActions.Disable();
    }

    public void BindFunction(string actionMapName, string actionName, OnChangedInputActionValueEvent Callback)
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

        action.performed += context => Callback(actionName, context.ReadValueAsObject());
        action.canceled += context => Callback(actionName, context.ReadValueAsObject());
    }

    public void UnBindFunction(string actionMapName, string actionName, OnChangedInputActionValueEvent Callback)
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

        action.performed -= context => Callback(actionName, context.ReadValueAsObject());
        action.canceled -= context => Callback(actionName, context.ReadValueAsObject());
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
