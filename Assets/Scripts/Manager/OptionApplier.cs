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
                Debug.LogWarning($"[OptionApplier] 알 수 없는 옵션 ID: {id}");
                break;
        }

        OnChangedOption?.Invoke(id, value);
    }

    private static void ApplyMasterVolume(object value)
    {
        if (value is float volume)
        {
            AudioListener.volume = volume;
            Debug.Log($"[OptionApplier] MasterVolume 적용: {volume}");
        }
    }

    public static void ApplyResolution(object value)
    {
        if (value is not int index)
        {
            if (value is long index_long)
            {
                index = Convert.ToInt32(index_long);
            }
            else
            {
                return;
            }
        }

        if (index < 0 || index >= Screen.resolutions.Length)
            return;

        var optionData = GameManager.Instance.GameFeatureManager.GameFeature.OptionData;

        var resolutionOption = optionData.Groups
            .SelectMany(group => group.Options)
            .FirstOrDefault(opt => opt.OptionId == EOptionId.Resolution) as DropdownOptionItemData;

        if (resolutionOption == null || index >= resolutionOption.Items.Count)
            return;

        if (TryParseResolution(resolutionOption.Items[index], out int width, out int height))
        {
            FullScreenMode fullScreenMode = (FullScreenMode)GameManager.Instance.OptionManager.GetValue<int>(EOptionId.ScreenMode);
            Screen.SetResolution(width, height, fullScreenMode);
            Debug.Log($"[OptionApplier] Resolution 적용: {width}x{height} ({fullScreenMode})");
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
            Debug.Log($"[OptionApplier] ScreenMode 적용: {(FullScreenMode)screenMode}");
        }
        else if(value is long screenMode_long)
        {
            Screen.fullScreenMode = (FullScreenMode)screenMode_long;
            Debug.Log($"[OptionApplier] ScreenMode 적용: {(FullScreenMode)screenMode_long}");
        }
    }

    private static void ApplyLanguage(object value)
    {
        if (value is int lang)
        {
            Debug.Log($"[OptionApplier] Language 적용: {(ELanguageType)lang}");
        }
        else if (value is long lang_long)
        {
            Debug.Log($"[OptionApplier] Language 적용: {(ELanguageType)lang_long}");
        }
    }
}
