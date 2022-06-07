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
        currentMode = Screen.fullScreenMode;

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
        Debug.Log("Right");

        if (currentMode == FullScreenMode.ExclusiveFullScreen)
            currentMode = FullScreenMode.Windowed;
        else if (currentMode == FullScreenMode.Windowed)
            currentMode = FullScreenMode.ExclusiveFullScreen;

        currentModeText++;
        if (currentModeText > 1) currentModeText = 0;

        currentScreenModeText.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;

        right.image.color = Color.white;
        LerpColor(right.image, selectColor);
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

        currentScreenModeText.text = textScreenMode[currentModeText];

        Screen.fullScreenMode = currentMode;

        left.image.color = Color.white;
        LerpColor(left.image, selectColor);
    }
    IEnumerator LerpColor(Image _image, Color _color)
    {
        Color initColor = _image.color;
        yield return new WaitForSecondsRealtime(0.2f);

        float timer = 0.0f, maxTime = 0.2f;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
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
