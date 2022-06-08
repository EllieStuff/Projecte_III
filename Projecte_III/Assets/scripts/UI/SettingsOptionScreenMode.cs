using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsOptionScreenMode : SettingsOptionClass
{
    [SerializeField] Button left, right;
    [SerializeField] TextMeshProUGUI currentScreenModeText, titleScreenModeText;

    [SerializeField] Color selectColor;

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
        //currentMode = Screen.fullScreenMode;

        if (currentMode.ToString().Contains("Full")) currentModeText = 0;
        else if (currentMode.ToString().Contains("Window")) currentModeText = 1;

        currentScreenModeText.text = textScreenMode[currentModeText];
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
        //if (!((!invokeCalledByScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || invokeCalledByScript))
        //    return;
        if (MouseFilterCheck(_calledFromScript))
            return;

        Debug.Log("Right");

        if (currentMode != FullScreenMode.Windowed)
            currentMode = FullScreenMode.Windowed;
        else
            currentMode = FullScreenMode.MaximizedWindow;

        currentModeText++;
        if (currentModeText > 1) currentModeText = 0;

        currentScreenModeText.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;

        right.image.color = selectColor;
        StartCoroutine(LerpColor(right.image, Color.white));
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        if (MouseFilterCheck(_calledFromScript))
            return;

        Debug.Log("Left");
        if (currentMode != FullScreenMode.Windowed)
            currentMode = FullScreenMode.Windowed;
        else
            currentMode = FullScreenMode.MaximizedWindow;

        currentModeText--;
        if (currentModeText < 0) currentModeText = 1;

        currentScreenModeText.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;

        left.image.color = selectColor;
        StartCoroutine(LerpColor(left.image, Color.white));
    }
    IEnumerator LerpColor(Image _image, Color _color)
    {
        Color initColor = _image.color;
        yield return new WaitForSecondsRealtime(0.1f);

        float timer = 0.0f, maxTime = 0.5f;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.unscaledTime;
            _image.color = Color.Lerp(initColor, _color, timer / maxTime);
        }
        _image.color = _color;
    }

    public override void Select()
    {
        titleScreenModeText.color = selectColor;
    }
    public override void Deselect()
    {
        titleScreenModeText.color = Color.white;
    }
}
