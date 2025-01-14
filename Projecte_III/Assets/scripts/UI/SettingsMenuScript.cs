using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    enum SettingsOption { GLOBAL_VOLUME, OST_VOLUME, SFX_VOLUME, WINDOWED, RESOLUTION, COUNT };

    [SerializeField] bool usePlayerManaging = true;

    GlobalMenuInputs inputs = null;
    SettingsOptionClass[] options;    //Nota: Utilitzar herencia per interactuar m�s facilment amb els diferents cosos dels settings
    int idx = 0;
    int lastIdx = 0;
    InputSystem.KeyData playerManaging = null;
    //bool invokeCalledByScript = false;

    private void Start()
    {
        options = new SettingsOptionClass[(int)SettingsOption.COUNT];
        options[(int)SettingsOption.GLOBAL_VOLUME] = transform.Find("Global Volume Slider").GetComponent<SettingsOptionClass>();
        options[(int)SettingsOption.OST_VOLUME] = transform.Find("OST Volume Slider").GetComponent<SettingsOptionClass>();
        options[(int)SettingsOption.SFX_VOLUME] = transform.Find("SFX Volume Slider").GetComponent<SettingsOptionClass>();
        options[(int)SettingsOption.WINDOWED] = transform.Find("Windowed").GetComponent<SettingsOptionClass>();
        options[(int)SettingsOption.RESOLUTION] = transform.Find("Resolution").GetComponent<SettingsOptionClass>();

    }

    public void Init(GlobalMenuInputs _inputs, InputSystem.KeyData _playerManaging)
    {
        inputs = _inputs;
        playerManaging = _playerManaging;
        foreach (SettingsOptionClass option in options)
            option.SetPlayerManaging(_playerManaging);

        idx = 0;
        SetOptionSelectedFeedback(lastIdx, idx);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs == null && usePlayerManaging)
            return;

        lastIdx = idx;
        if (inputs.UpPressed && IsManagingDeviceInput(inputs.UpData.deviceId))
        {
            idx--;
            if (idx < 0) idx = options.Length - 1;
            SetOptionSelectedFeedback(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.DownPressed && IsManagingDeviceInput(inputs.DownData.deviceId))
        {
            idx++;
            if (idx >= options.Length) idx = 0;
            SetOptionSelectedFeedback(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.AcceptPressed && IsManagingDeviceInput(inputs.AcceptData.deviceId))
        {
            //invokeCalledByScript = true;
            options[idx].Interact_Accept(true);
            //invokeCalledByScript = false;
        }
        else if (inputs.RightPressed && IsManagingDeviceInput(inputs.RightData.deviceId))
        {
            //invokeCalledByScript = true;
            options[idx].Interact_Right(true);
            //invokeCalledByScript = false;
        }
        else if (inputs.LeftPressed && IsManagingDeviceInput(inputs.LeftData.deviceId))
        {
            //invokeCalledByScript = true;
            options[idx].Interact_Left(true);
            //invokeCalledByScript = false;
        }
        else if (inputs.DeclinePressed && IsManagingDeviceInput(inputs.DeclineData.deviceId))
        {
            InGameMenu ingameMenu = inputs.GetComponent<InGameMenu>();
            if (ingameMenu != null)
            {
                ingameMenu.invokeCalledByScript = true;
                ingameMenu.CloseSettings();
            }
        }
    }

    bool IsManagingDeviceInput(int _inputDeviceOrigin)
    {
        if (!usePlayerManaging) return true;
        return _inputDeviceOrigin == playerManaging.deviceId;
    }

    void SetOptionSelectedFeedback(int lastIdx, int currIdx)
    {
        options[lastIdx].Deselect();
        options[currIdx].Select();
    }
    void ResetOptionSelectedFeedback()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Deselect();
        }
    }


}
