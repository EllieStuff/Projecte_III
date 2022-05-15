using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InGameMenu : MonoBehaviour
{
    enum InGameButton { CONTINUE, REPLAY, SETTINGS, EXIT, COUNT };

    [SerializeField] GameObject menuSet, menuSettings;
    [SerializeField] Color baseColor, hoverColor;

    GlobalMenuInputs inputs;
    PlayersManager playersManager;
    Button[] buttons;
    int idx = 0;
    bool gameStarted = false;
    //int playerManaging = -1;
    InputSystem.KeyData playerManaging = null;
    internal bool invokeCalledByScript = false;


    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<GlobalMenuInputs>();
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

        buttons = new Button[(int)InGameButton.COUNT];
        buttons[(int)InGameButton.CONTINUE] = menuSet.transform.Find("Continue Button").GetComponent<Button>();
        buttons[(int)InGameButton.REPLAY] = menuSet.transform.Find("Replay Button").GetComponent<Button>();
        buttons[(int)InGameButton.SETTINGS] = menuSet.transform.Find("Settings Button").GetComponent<Button>();
        buttons[(int)InGameButton.EXIT] = menuSet.transform.Find("Exit Button").GetComponent<Button>();
        //ResetButtonColors();
        //SetButtonColor(0, 0);

        menuSet.SetActive(false);
        //menuSettings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIsInSubMenu())
            return;
        if (!CheckMenuIsOpen())
            return;


        int lastIdx = idx;
        if (inputs.UpPressed && IsManagingDeviceInput(inputs.UpData.deviceId))
        {
            idx--;
            if (idx < 0) idx = buttons.Length - 1;
            SetButtonColor(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.DownPressed && IsManagingDeviceInput(inputs.DownData.deviceId))
        {
            idx++;
            if (idx >= buttons.Length) idx = 0;
            SetButtonColor(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.AcceptPressed && IsManagingDeviceInput(inputs.AcceptData.deviceId))
        {
            invokeCalledByScript = true;
            buttons[idx].onClick.Invoke();
            invokeCalledByScript = false;
        }
        else if (inputs.DeclinePressed && IsManagingDeviceInput(inputs.DeclineData.deviceId))
        {
            //if (idx == (int)InGameButton.SETTINGS && menuSettings.activeSelf)
            //{
            //    invokeCalledByScript = true;
            //    CloseSettings();
            //    //EnableModifiers(true);
            //}
        }
    }

    private bool CheckMenuIsOpen()
    {
        if (!CheckStarted())
            return false;

        if ((inputs.StartBttnPressed || inputs.EscapeBttnPressed)
            && (playerManaging == null || IsManagingDeviceInput(inputs.StartBttnData.deviceId) || IsManagingDeviceInput(inputs.EscapeBttnData.deviceId)))
        {
            if (inputs.StartBttnPressed)
                playerManaging = inputs.StartBttnData;
            else if (inputs.EscapeBttnPressed)
                playerManaging = inputs.EscapeBttnData;

            if (idx == (int)InGameButton.SETTINGS && menuSettings.activeSelf)
            {
                invokeCalledByScript = true;
                CloseSettings();
            }
            else
            {
                menuSet.SetActive(!menuSet.activeSelf);
                if (menuSet.activeSelf)
                {
                    idx = 0;
                    ResetButtonColors();
                    SetButtonColor(0, idx);
                    Time.timeScale = 0.0f;
                    EnableModifiers(false);
                }
                else
                {
                    Time.timeScale = 1.0f;
                    playerManaging = null;
                    EnableModifiers(true);
                }
            }
        }

        return menuSet.activeSelf;
    }

    bool CheckIsInSubMenu()
    {
        return menuSettings.activeSelf;
    }

    void EnableModifiers(bool _enabled)
    {
        if (!_enabled)
        {
            for (int i = 0; i < playersManager.numOfPlayers; i++)
                playersManager.GetPlayer(i).GetComponent<RandomModifierGet>().enabled = false;
        }
        else
            StartCoroutine(EnableModifiersCoroutine());
    }
    IEnumerator EnableModifiersCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < playersManager.numOfPlayers; i++)
            playersManager.GetPlayer(i).GetComponent<RandomModifierGet>().enabled = true;
    }


    void SetButtonColor(int lastIdx, int currIdx)
    {
        buttons[lastIdx].GetComponent<Image>().color = baseColor;
        buttons[currIdx].GetComponent<Image>().color = hoverColor;
    }
    void ResetButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = baseColor;
        }
    }


    bool CheckStarted()
    {
        if (!gameStarted)
        {
            if (playersManager.GetPlayer(0).GetComponent<PlayerVehicleScript>().raceStarted) gameStarted = true;
            return false;
        }
        else
            return true;
    }

    bool IsManagingDeviceInput(int _inputDeviceOrigin)
    {
        return _inputDeviceOrigin == playerManaging.deviceId;
    }


    public void HoverSound()
    {
        AudioManager.Instance.Play_SFX("Hover_SFX");
    }

    public void PlayButton()
    {
        if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
            return;

        menuSet.SetActive(false);
        EnableModifiers(true);
        idx = 0;
        Time.timeScale = 1.0f;
        playerManaging = null;
        AudioManager.Instance.Play_SFX("Click_SFX");
    }
    public void ReplayButton()
    {
        if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
            return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        EnableModifiers(true);
        Time.timeScale = 1.0f;
        Destroy(GameObject.FindGameObjectWithTag("PlayersManager"));
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Current Building Scene");
        //SceneManager.LoadScene("ProceduralMapSceneTest");
        //GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("ProceduralMapSceneTest");
    }
    public void SettingsButton()
    {
        if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
            return;

        menuSettings.SetActive(true);
        menuSettings.GetComponent<SettingsMenuScript>().Init(inputs, playerManaging);
        idx = (int)InGameButton.SETTINGS;
        AudioManager.Instance.Play_SFX("Click_SFX");
    }
    public void ExitButton()
    {
        if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
            return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        Time.timeScale = 1.0f;
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Menu");
    }

    public void CloseSettings()
    {
        if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
            return;

        AudioManager.Instance.Play_SFX("Click_SFX", 0.6f);
        ResetButtonColors();
        SetButtonColor(0, idx);
        menuSettings.SetActive(false);
        invokeCalledByScript = false;
    }


    //bool OpenMenuInput()
    //{
    //    return Input.GetKeyDown(KeyCode.Escape) 
    //        || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) 
    //        || Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7);
    //}
    //bool DownMenuInput()
    //{
    //    //if(co)
    //    return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -0.4f;
    //}
    //bool UpMenuInput()
    //{
    //    return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0.4f;
    //}
}
