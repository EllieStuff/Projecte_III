using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsOptionScreenMode : SettingsOptionClass
{
    [SerializeField] Button left, right;
    [SerializeField] TextMeshProUGUI text;

    string[] textScreenMode =
    {
        "FULLSCREEN",
        "WINDOWED"
    };

    FullScreenMode currentMode;
    int currentModeText = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentMode = Screen.fullScreenMode;

        if (currentMode.ToString().Contains("Full")) currentModeText = 0;
        else if (currentMode.ToString().Contains("Window")) currentModeText = 1;

        text.text = textScreenMode[currentModeText];
        Screen.fullScreenMode = currentMode;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        Debug.Log("Accept");
    }
    public override void Interact_Right(bool _calledFromScript = false)
    {
        Debug.Log("Right");

        if (currentMode == FullScreenMode.ExclusiveFullScreen)
            currentMode = FullScreenMode.Windowed;
        else if (currentMode == FullScreenMode.Windowed)
            currentMode = FullScreenMode.ExclusiveFullScreen;

        currentModeText++;
        if (currentModeText > 1) currentModeText = 0;

        text.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        Debug.Log("Left");
        if (currentMode == FullScreenMode.ExclusiveFullScreen)
            currentMode = FullScreenMode.Windowed;
        else if (currentMode == FullScreenMode.Windowed)
            currentMode = FullScreenMode.ExclusiveFullScreen;

        currentModeText--;
        if (currentModeText < 0) currentModeText = 1;

        text.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;
    }

    public override void Select()
    {

    }
    public override void Deselect()
    {

    }
}
