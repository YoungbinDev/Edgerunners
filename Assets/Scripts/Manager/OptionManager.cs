using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private string FilePath;
    public OptionData OptionData;

    public void Init()
    {
        FilePath = Path.Combine(Application.persistentDataPath, "options.json");

        //TODO: 구현할거
        //StringUIText.cs / SetStringUI()
        //OptionManager.cs

        //Load();
    }

    //public void Save()
    //{
    //    string json = JsonUtility.ToJson(MakeOptionData(), true);
    //    File.WriteAllText(FilePath, json);
    //    Debug.Log("Option saved: " + FilePath);
    //}

    //public void Load()
    //{
    //    if (!File.Exists(FilePath))
    //    {
    //        Debug.Log("Option file not found. Creating default.");
    //        return;
    //    }

    //    string json = File.ReadAllText(FilePath);
    //    OptionData LoadedOptionData = JsonUtility.FromJson<OptionData>(json);
    //    Debug.Log("Option loaded: " + FilePath);
    //    SetOptionData(LoadedOptionData);
    //}

    //public void SetOptionData(OptionData optionData)
    //{
    //    LanguageType = optionData.Language;
    //}

    //public OptionData GetOptionData()
    //{
    //    return MakeOptionData();
    //}

    //private OptionData MakeOptionData()
    //{
    //    OptionData optionData = new OptionData();
    //    optionData.Language = LanguageType;
    //    return optionData;
    //}
}
