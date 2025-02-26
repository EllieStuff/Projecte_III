using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsOptionResolution : SettingsOptionClass
{
    [SerializeField] Button left, right;
    [SerializeField] TextMeshProUGUI selectedResolutionText, resolutionTitleText;

    [SerializeField] Color selectColor;


    string[] textResolution;

   // CanvasScaler canvasParent;

    [SerializeField] Vector2[] possibleResolution;
    Vector2 resolution;
    int currentModeText = 0;

    // Start is called before the first frame update
    void Start()
    {
        Resolution[] _allResolutions = Screen.resolutions;

        List<Vector2> _tmpRes = new List<Vector2>();

        for (int i = 0; i < _allResolutions.Length; i++)
        {
            Vector2 _curr = new Vector2(_allResolutions[i].width, _allResolutions[i].height);
            IsResolutionSaved(_curr, ref _tmpRes);
        }

        possibleResolution = new Vector2[_tmpRes.Count];
        textResolution = new string[_tmpRes.Count];

        for (int i = 0; i < _tmpRes.Count; i++)
        {
            possibleResolution[i] = _tmpRes[i];
            textResolution[i] = _tmpRes[i].x + " x " + _tmpRes[i].y;
        }

        Resolution _currRes = Screen.currentResolution;
        currentModeText = GetCurrentResolution(_currRes.width, _currRes.height);

        resolution = possibleResolution[currentModeText];
        selectedResolutionText.text = textResolution[currentModeText];
    }

    void IsResolutionSaved(Vector2 _res, ref List<Vector2> _resList)
    {
        if (_resList.Count == 0)
        {
            _resList.Add(_res);
            return;
        }

        for (int i = 0; i < _resList.Count; i++)
        {
            if(_res.x == _resList[i].x)
            {
                if (_res.y > _resList[i].y) _resList[i] = _res;
                return;
            }
        }

        _resList.Add(_res);
    }

    int GetCurrentResolution(Vector2 _res)
    {
        for (int i = 0; i < possibleResolution.Length; i++)
        {
            if (possibleResolution[i] == _res)
                return i;
        }
        return -1;
    }

    int GetCurrentResolution(float _resX, float _resY)
    {
        for (int i = 0; i < possibleResolution.Length; i++)
        {
            if (possibleResolution[i].x == _resX && possibleResolution[i].y == _resY)
                return i;
        }
        return -1;
    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        Debug.Log("Accept");
    }
    public override void Interact_Right(bool _calledFromScript = false)
    {
        if (MouseFilterCheck(_calledFromScript))
            return;

        Debug.Log("Right");

        currentModeText++;
        if (currentModeText >= possibleResolution.Length) currentModeText = 0;

        selectedResolutionText.text = textResolution[currentModeText];

        resolution = possibleResolution[currentModeText];

       // canvasParent.referenceResolution = resolution;

        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreen);

        right.image.color = selectColor;
        StartCoroutine(LerpColor(right.image, Color.white));
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        if (MouseFilterCheck(_calledFromScript))
            return;

        Debug.Log("Left");

        currentModeText--;
        if (currentModeText < 0) currentModeText = possibleResolution.Length - 1;

        selectedResolutionText.text = textResolution[currentModeText];

        resolution = possibleResolution[currentModeText];

        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreen);

        left.image.color = selectColor;
        StartCoroutine(LerpColor(left.image, Color.white));
    }
    IEnumerator LerpColor(Image _image, Color _color)
    {
        Color initColor = _image.color;
        yield return new WaitForSecondsRealtime(0.1f);

        float timer = 0.0f, maxTime = 0.2f;
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
        resolutionTitleText.color = selectColor; 
    }
    public override void Deselect()
    {
        resolutionTitleText.color = Color.white;
    }
}
