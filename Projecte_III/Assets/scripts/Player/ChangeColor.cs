using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    Material currentColor;

    public int currentValue;

    static List<KeyValuePair<int, Material>> colorList = new List<KeyValuePair<int, Material>>();
    static List<KeyValuePair<int, Material>> defaultColorList = new List<KeyValuePair<int, Material>>();

    [SerializeField] VehicleTriggerAndCollisionEvents player;
    [SerializeField] Button buttonRight, buttonLeft;

    [SerializeField] SpriteRenderer backgroundColor;
    Image textBackgroundColor;

    UseGradientMaterials parent;

    PlayerMenuInputsPressed playerInputs;
    Button lastButton = null;
    bool gradient = false;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<UseGradientMaterials>();
        gradient = parent.gradient;
        playerInputs = GetComponent<PlayerMenuInputsPressed>();

        textBackgroundColor = transform.GetChild(0).GetComponent<Image>();

        buttonRight.GetComponent<Image>().color = Color.yellow;
        buttonLeft.GetComponent<Image>().color = Color.yellow;

        int _rand = Random.Range(0, colorList.Count);

        while(colorList[_rand].Value == null)
        {
            _rand = Random.Range(0, colorList.Count);
        }
        currentValue = _rand;
        currentColor = colorList[_rand].Value;

        colorList[_rand] = new KeyValuePair<int, Material>(_rand, null);

        player.DefaultMaterial = currentColor;
        Color _curr = UseGradientMaterials.GetColor(currentColor.name);

        backgroundColor.color = new Color(_curr.r, _curr.g, _curr.b, 0.45f);
        textBackgroundColor.color = _curr;
    }

    private void OnEnable()
    {
        if (player.GetComponentInParent<PlayerData>().id == 0)
        {
            colorList.Clear();
            defaultColorList.Clear();

            Material[] mats;
            if (!gradient)
                mats = Resources.LoadAll<Material>("Materials/CarMaterials/Flat");
            else
                mats = Resources.LoadAll<Material>("Materials/CarMaterials/Gradient");

            for (int i = 0; i < mats.Length; i++)
            {
                colorList.Add(new KeyValuePair<int, Material>( i, mats[i]));
                defaultColorList.Add(new KeyValuePair<int, Material>(i, mats[i]));
            }
        }
    }

    private void Update()
    {
        if (playerInputs.MenuRightPressed)
        {
            PressButton(buttonRight);
            SetNewColor(1);
        }
        else if (playerInputs.MenuLeftPressed)
        {
            PressButton(buttonLeft);
            SetNewColor(-1);
        }
    }

    public void SetNewColor(int _direction)
    {
        colorList[currentValue] = defaultColorList[currentValue];

        do
        {
            currentValue += _direction;
            if (currentValue >= colorList.Count) currentValue = 0;
            else if (currentValue < 0) currentValue = colorList.Count - 1;

        } while (colorList[currentValue].Value == null);

        currentColor = colorList[currentValue].Value;
        colorList[currentValue] = new KeyValuePair<int, Material>(currentValue, null);

        player.DefaultMaterial = currentColor;

        Color _curr = UseGradientMaterials.GetColor(currentColor.name);

        backgroundColor.color = new Color(_curr.r, _curr.g, _curr.b, 0.45f);
        textBackgroundColor.color = _curr;
    }

    public void IncreaseColorId()
    {
        PressButton(buttonRight);

        if (!playerInputs.UsesKeyboard())
            return;
        colorList[currentValue] = defaultColorList[currentValue];

        do
        {
            currentValue += 1;
            if (currentValue >= colorList.Count) currentValue = 0;

        } while (colorList[currentValue].Value == null);

        currentColor = colorList[currentValue].Value;
        colorList[currentValue] = new KeyValuePair<int, Material>(currentValue, null);

        player.DefaultMaterial = currentColor;

        Color _curr = UseGradientMaterials.GetColor(currentColor.name);

        backgroundColor.color = new Color(_curr.r, _curr.g, _curr.b, 0.45f);
        textBackgroundColor.color = _curr;
    }

    public void DecreaseColorId()
    {
        PressButton(buttonLeft);

        if (!playerInputs.UsesKeyboard()) 
            return;

        colorList[currentValue]= defaultColorList[currentValue];
        
        do
        {
            currentValue += -1;
            if (currentValue < 0) currentValue = colorList.Count - 1;

        } while (colorList[currentValue].Value == null);

        currentColor = colorList[currentValue].Value;
        colorList[currentValue] = new KeyValuePair<int, Material>(currentValue, null);

        player.DefaultMaterial = currentColor;

        Color _curr = UseGradientMaterials.GetColor(currentColor.name);

        backgroundColor.color = new Color(_curr.r, _curr.g, _curr.b, 0.45f);
        textBackgroundColor.color = _curr;
    }

    void PressButton(Button _button)
    {
        if(lastButton != null)
            lastButton.GetComponent<Image>().color = Color.yellow;

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
