using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInputs : MonoBehaviour
{
    enum MenuState { MAIN, SETTINGS, CREDITS, COUNT };
    enum UIElementType { DEFAULT, BUTTON, SLIDER };

    [SerializeField] Transform mainFather;
    [SerializeField] Transform settingsFathers;
    [SerializeField] Transform creditsFather;

    Button[] mainButtons;
    Transform[] settingOptions;
    int mainIdx = 0;
    int settingsIdx = 0;
    
    MenuState menuState = MenuState.MAIN;
    UIElementType uiElementType = UIElementType.DEFAULT;
    GlobalMenuInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<GlobalMenuInputs>();

        mainButtons = new Button[mainFather.childCount];
        for (int i = 0; i < mainButtons.Length; i++)
            mainButtons[i] = mainFather.GetChild(i).GetComponent<Button>();

        settingOptions = new Transform[settingsFathers.childCount];
        for (int i = 0; i < settingOptions.Length; i++)
            settingOptions[i] = settingsFathers.GetChild(i);

        mainButtons[mainIdx].Select();
    }

    // Update is called once per frame
    void Update()
    {
        switch (menuState)
        {
            case MenuState.MAIN:
                UpdateMainMenu();

                break;

            case MenuState.SETTINGS:
                UpdateSettingsMenu();

                break;

            case MenuState.CREDITS:
                UpdateCreditsMenu();

                break;

            default:
                break;
        }
    }


    void UpdateMainMenu()
    {
        if (inputs.UpPressed)
        {
            mainIdx--;
            if (mainIdx < 0) mainIdx = mainButtons.Length - 1;
            AudioManager.Instance.Play_SFX("Hover_SFX");
            mainButtons[mainIdx].Select();
        }
        if (inputs.DownPressed)
        {
            mainIdx++;
            if (mainIdx >= mainButtons.Length) mainIdx = 0;
            AudioManager.Instance.Play_SFX("Hover_SFX");
            mainButtons[mainIdx].Select();
        }
        if (inputs.AcceptPressed)
        {
            if (mainIdx == (int)MenuState.SETTINGS)
                menuState = MenuState.SETTINGS;
            else if (mainIdx == (int)MenuState.CREDITS)
                menuState = MenuState.CREDITS;
            //AudioManager.Instance.Play_SFX("Hover_SFX");
            mainButtons[mainIdx].onClick.Invoke();
        }
    }

    void UpdateSettingsMenu()
    {
        //if (inputs.UpPressed)
        //{
        //    mainIdx--;
        //    if (mainIdx < 0) mainIdx = settingOptions.Length - 1;
        //    settingOptions[mainIdx].Select();
        //}
        //if (inputs.DownPressed)
        //{
        //    mainIdx++;
        //    if (mainIdx >= settingOptions.Length) mainIdx = 0;
        //    settingOptions[mainIdx].Select();
        //}
        //if (inputs.AcceptPressed)
        //{
        //    if (mainIdx == SETTINGS_IDX)
        //        menuState = MenuState.SETTINGS;
        //    settingOptions[mainIdx].onClick.Invoke();
        //}
        if (inputs.DeclinePressed || Input.GetKeyDown(KeyCode.Escape))
        {
            settingsFathers.gameObject.SetActive(false);
            AudioManager.Instance.Play_SFX("Click_SFX", 0.6f);
            menuState = MenuState.MAIN;
            mainButtons[mainIdx].Select();
        }
    }
    
    void UpdateCreditsMenu()
    {
        if (inputs.DeclinePressed || Input.GetKeyDown(KeyCode.Escape))
        {
            creditsFather.gameObject.SetActive(false);
            AudioManager.Instance.Play_SFX("Click_SFX", 0.6f);
            menuState = MenuState.MAIN;
            mainButtons[mainIdx].Select();
        }
    }

}
