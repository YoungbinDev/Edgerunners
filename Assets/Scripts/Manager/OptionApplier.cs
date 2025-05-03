using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OptionApplier : MonoBehaviour
{
    public static event Action<EOptionId, object> OnChangedOption;

    public static void Apply(EOptionId id, object value)
    {
        switch (id)
        {
            case EOptionId.MasterVolume:
                ApplyMasterVolume(value);
                break;

            case EOptionId.Resolution:
                ApplyResolution(value);
                break;

            case EOptionId.ScreenMode:
                ApplyScreenMode(value);
                break;

            case EOptionId.Language:
                ApplyLanguage(value);
                break;

            default:
                Debug.LogWarning($"[OptionApplier] �� �� ���� �ɼ� ID: {id}");
                break;
        }

        OnChangedOption?.Invoke(id, value);
    }

    private static void ApplyMasterVolume(object value)
    {
        if (value is float volume)
        {
            AudioListener.volume = volume;
            Debug.Log($"[OptionApplier] MasterVolume ����: {volume}");
        }
    }

    public static void ApplyResolution(object value)
    {
        int index;
        if (value is int i)
            index = i;
        else if (value is long l)
            index = Convert.ToInt32(l);
        else
            return;

        var optionData = GameManager.Instance.GetManager<GameFeatureManager>()?.GameFeature?.OptionData;
        if(optionData == null)
        {
            Debug.LogWarning("[ApplyResolution] OptionData is missing.");
            return;
        }

        var items = GlobalFunction.GetOptionItems<string>(optionData, EOptionId.Resolution);
        if (items == null || index >= items.Count)
        {
            Debug.LogWarning("[ApplyResolution] Invalid items.");
            return;
        }

        if (TryParseResolution(items[index], out int width, out int height))
        {
            FullScreenMode fullScreenMode = (FullScreenMode)GameManager.Instance.GetManager<OptionManager>()?.GetValue<int>(EOptionId.ScreenMode);
            Screen.SetResolution(width, height, fullScreenMode);
            Debug.Log($"[OptionApplier] Resolution ����: {width}x{height} ({fullScreenMode})");
        }
    }

    private static bool TryParseResolution(string resolutionText, out int width, out int height)
    {
        width = 0;
        height = 0;

        var parts = resolutionText.Split('x');
        if (parts.Length == 2 &&
            int.TryParse(parts[0].Trim(), out width) &&
            int.TryParse(parts[1].Trim(), out height))
        {
            return true;
        }

        return false;
    }

    private static void ApplyScreenMode(object value)
    {
        if (value is int screenMode)
        {
            Screen.fullScreenMode = (FullScreenMode)screenMode;
            Debug.Log($"[OptionApplier] ScreenMode ����: {(FullScreenMode)screenMode}");
        }
        else if(value is long screenMode_long)
        {
            Screen.fullScreenMode = (FullScreenMode)screenMode_long;
            Debug.Log($"[OptionApplier] ScreenMode ����: {(FullScreenMode)screenMode_long}");
        }
    }

    private static void ApplyLanguage(object value)
    {
        if (value is int lang)
        {
            Debug.Log($"[OptionApplier] Language ����: {(ELanguageType)lang}");
        }
        else if (value is long lang_long)
        {
            Debug.Log($"[OptionApplier] Language ����: {(ELanguageType)lang_long}");
        }
    }
}
