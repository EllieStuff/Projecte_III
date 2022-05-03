using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    Material currentColor;

    public int currentValue;

    static List<Material> colorList = new List<Material>();
    [SerializeField] VehicleTriggerAndCollisionEvents player;
    [SerializeField] Button buttonRight, buttonLeft;

    PlayerMenuInputsPressed playerInputs;
    Button lastButton = null;
    bool gradient = false;

    // Start is called before the first frame update
    void Start()
    {
        gradient = transform.parent.GetComponent<UseGradientMaterials>().gradient;
        playerInputs = GetComponent<PlayerMenuInputsPressed>();

        buttonRight.GetComponent<Image>().color = Color.yellow;
        buttonLeft.GetComponent<Image>().color = Color.yellow;

        Debug.Log("List size: " + colorList.Count);
        if(colorList.Count <= 0)
        {
            Material[] mats;
            if (!gradient)
                mats = Resources.LoadAll<Material>("Materials/CarMaterials/Flat");
            else
                mats = Resources.LoadAll<Material>("Materials/CarMaterials/Gradient");

            for (int i = 0; i < mats.Length; i++)
            {
                colorList.Add(mats[i]);
            }
        }

        int _rand = Random.Range(0, colorList.Count);

        currentColor = colorList[_rand];
        colorList.RemoveAt(_rand);

        player.DefaultMaterial = currentColor;
    }

    private void Update()
    {
        if (playerInputs.MenuRightPressed)
        {
            PressButton(buttonRight);
            //buttonRight.onClick.Invoke();
        }
        else if (playerInputs.MenuLeftPressed)
        {
            PressButton(buttonLeft);
            //buttonLeft.onClick.Invoke();
        }
    }

    public void SetNewColor(int _direction)
    {
        Material _currentColor = null;

        if(_direction > 0)
        {
            _currentColor = colorList[colorList.Count - 1];
            colorList.RemoveAt(colorList.Count - 1);
            colorList.Insert(0, currentColor);
        }
        else if(_direction < 0)
        {
            _currentColor = colorList[0];
            colorList.RemoveAt(0);
            colorList.Add(currentColor);
        }
        currentColor = _currentColor;

        player.DefaultMaterial = currentColor;
    }
    public void IncreaseColorId()
    {
        Material _currentColor = null;

        _currentColor = colorList[colorList.Count - 1];
        colorList.RemoveAt(colorList.Count - 1);
        colorList.Insert(0, currentColor);
        currentColor = _currentColor;

        player.DefaultMaterial = currentColor;
    }
    public void DecreaseColorId()
    {
        Material _currentColor = null;

        _currentColor = colorList[0];
        colorList.RemoveAt(0);
        colorList.Add(currentColor);
        currentColor = _currentColor;

        player.DefaultMaterial = currentColor;
    }

    void PressButton(Button _button)
    {
        if(lastButton != null)
            lastButton.GetComponent<Image>().color = Color.yellow;

        _button.onClick.Invoke();
        _button.GetComponent<Image>().color = Color.white;
        StartCoroutine(LerpColor(_button.GetComponent<Image>(), Color.yellow));
        lastButton = _button;
    }

    IEnumerator LerpColor(Image _image, Color _color)
    {
        Color initColor = _image.color;
        yield return new WaitForSecondsRealtime(0.2f);

        float timer = 0.0f, maxTime = 0.2f;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            _image.color = Color.Lerp(initColor, _color, timer / maxTime);
        }
        _image.color = _color;
    }
}
