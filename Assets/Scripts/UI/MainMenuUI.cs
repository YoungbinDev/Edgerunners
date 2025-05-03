using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void OnClickPlayButton()
    {

    }

    public void OnClickCharacterButton()
    {

    }

    public async void OnClickOptionsButton()
    {
        await GameManager.Instance.GetManager<UIManager>().OpenUI("OptionUI");
    }

    public async void OnClickExitButton()
    {
        GameObject stringUIPopupPrefab = await GameManager.Instance.GetManager<UIManager>().OpenUI("StringUIPopup");
        StringUIPopup stringUIPopupComponent = stringUIPopupPrefab.GetComponent<StringUIPopup>();
        stringUIPopupComponent.Init("StringId_Notice", "StringId_Question_CloseApplication", "StringId_Yes", "StringId_No");
        stringUIPopupComponent.OnClickPositiveButtonEvent += () =>
        {
            Application.Quit();
        };
        stringUIPopupComponent.OnClickNegativeButtonEvent += () =>
        {
            Destroy(stringUIPopupPrefab);
        };
    }
}
