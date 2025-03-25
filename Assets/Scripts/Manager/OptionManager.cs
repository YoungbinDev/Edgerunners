using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OptionManager : MonoBehaviour
{
    public OptionSaveData OriginalSaveData { get; private set; } = new();
    public OptionSaveData WorkingSaveData { get; private set; } = new();

    private static readonly JsonSerializerSettings settings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        Formatting = Newtonsoft.Json.Formatting.Indented
    };

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "option_save.json");
    }

    public void Init()
    {
        Load();
        Apply(false);
    }

    public void Load()
    {
        if (!File.Exists(GetPath()))
        {
            OriginalSaveData = new OptionSaveData();
            foreach(var optionGroup in GameManager.Instance.GameFeatureManager.GameFeature.OptionData.Groups)
            {
                foreach(var Option in optionGroup.Options)
                {
                    OriginalSaveData.values[Option.OptionId] = Option.GetDefaultValue();
                }
            }
        }
        else
        {
            string json = File.ReadAllText(GetPath());
            OriginalSaveData = JsonConvert.DeserializeObject<OptionSaveData>(json, settings);
        }

        WorkingSaveData = Clone(OriginalSaveData);
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(WorkingSaveData, settings);
        File.WriteAllText(GetPath(), json);
        OriginalSaveData = Clone(WorkingSaveData);
    }

    public void Apply(bool isSave)
    {
        foreach (var (id, value) in WorkingSaveData.values)
        {
            OptionApplier.Apply(id, value);
        }

        if(isSave)
        {
            Save();
        }
    }

    public void Apply(EOptionId optionId, bool isSave)
    {
        OptionApplier.Apply(optionId, WorkingSaveData.values[optionId]);

        if (isSave)
        {
            Save();
        }
    }

    public void Revert()
    {
        WorkingSaveData = Clone(OriginalSaveData);

        Apply(false);
    }

    //int없음 long로 해야함
    public T GetValue<T>(EOptionId id)
    {
        if (WorkingSaveData.values.TryGetValue(id, out var obj))
        {
            if (obj is T typed)
                return typed;

            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[Option] GetValue<{typeof(T).Name}> 변환 실패: obj={obj} ({obj?.GetType().Name}) → {e.Message}");
            }
        }
        
        return GetDefaultValue<T>(id);
    }

    public T GetDefaultValue<T>(EOptionId id)
    {
        var optionData = GameManager.Instance?.GameFeatureManager?.GameFeature?.OptionData;
        if (optionData == null)
            return default;

        foreach (var group in optionData.Groups)
        {
            foreach (var option in group.Options)
            {
                if (option.OptionId == id)
                {
                    if (option.GetDefaultValue() is T typedValue)
                        return typedValue;

                    Debug.LogWarning($"[Option] DefaultValue for {id} is not of type {typeof(T).Name}");
                    return default;
                }
            }
        }

        Debug.LogWarning($"[Option] OptionId {id} not found in any group.");
        return default;
    }

    public void SetValue<T>(EOptionId id, T value, bool isApply)
    {
        WorkingSaveData.values[id] = value;

        if (isApply)
        {
            Apply(id, false);
        }
    }

    private OptionSaveData Clone(OptionSaveData source)
    {
        string json = JsonConvert.SerializeObject(source, settings);
        return JsonConvert.DeserializeObject<OptionSaveData>(json, settings);
    }
}
