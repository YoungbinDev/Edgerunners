using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableUI : MonoBehaviour
{
    public event Action OnOpenedEvent;
    public event Action OnClosedEvent;

    public virtual void Open()
    {
        OnOpenedEvent?.Invoke();
    }

    public virtual void Close()
    {
        OnClosedEvent?.Invoke();
    }
}
