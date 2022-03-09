using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsSlider : MonoBehaviour
{
    float currentValue = -1;

    [SerializeField] float maxValue = 200;

    Color mainColor;

    [SerializeField] Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        sliders = new Slider[transform.childCount];
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i] = transform.GetChild(i).GetComponent<Slider>();
            sliders[i].maxValue = maxValue;
        }

        mainColor = Color.blue;
    }

    public void SetSliderValue(float value, bool placed)
    {
        if(placed || value == currentValue)
        {
            sliders[0].value = value;
            sliders[0].transform.GetChild(1).GetComponentInChildren<Image>().color = mainColor;

            sliders[1].value = value;
            sliders[1].transform.GetChild(0).GetComponentInChildren<Image>().color = mainColor;

            currentValue = value;
            return;
        }

        if (value < currentValue)
        {
            sliders[0].value = currentValue;
            sliders[0].transform.GetChild(1).GetComponentInChildren<Image>().color = Color.red;

            sliders[1].value = value;
            sliders[1].transform.GetChild(0).GetComponentInChildren<Image>().color = mainColor;
        }
        else
        {
            sliders[0].value = value;
            sliders[0].transform.GetChild(1).GetComponentInChildren<Image>().color = Color.green;

            sliders[1].value = currentValue;
            sliders[1].transform.GetChild(0).GetComponentInChildren<Image>().color = mainColor;
        }
    }
}
