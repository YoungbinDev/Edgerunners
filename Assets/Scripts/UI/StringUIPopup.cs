using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringUIPopup : MonoBehaviour
{
    [SerializeField] private StringUIText titleStringUIText;
    [SerializeField] private StringUIText contentStringUIText;
    [SerializeField] private StringUIText positiveButtonStringUIText;
    [SerializeField] private StringUIText negativeButtonStringUIText;

    [SerializeField] private string titleStringId;
    [SerializeField] private string contentStringId;
    [SerializeField] private string positiveButtonStringId;
    [SerializeField] private string negativeButtonStringId;

    public event Action OnClickPositiveButtonEvent;
    public event Action OnClickNegativeButtonEvent;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }

        Init(titleStringId, contentStringId, positiveButtonStringId, negativeButtonStringId);
    }
#endif

    public void OnClickPositiveButton()
    {
        OnClickPositiveButtonEvent?.Invoke();
    }

    public void OnClickNegativeButton()
    {
        OnClickNegativeButtonEvent?.Invoke();
    }

    public void Init(string titleStringId, string contentStringId, string positiveButtonStringId, string negativeButtonStringId)
    {
        titleStringUIText.SetStringUI(titleStringId);
        contentStringUIText.SetStringUI(contentStringId);
        positiveButtonStringUIText.SetStringUI(positiveButtonStringId);
        negativeButtonStringUIText.SetStringUI(negativeButtonStringId);
    }
}
