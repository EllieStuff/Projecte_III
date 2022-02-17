using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    float currentValue = -1;
    float newValue = -1;
    string sliderStat;

    Color mainColor;

    Slider[] sliders;

    // Start is called before the first frame update
    void Start()
    {
        sliderStat = gameObject.name;

        sliders = new Slider[transform.childCount];
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i] = transform.GetChild(i).GetComponent<Slider>();
        }

        mainColor = Color.blue;
    }



    // Update is called once per frame
    void Update()
    {
        if(currentValue < 0)
        {
            Stats.Data d = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().GetStats();

            if(sliderStat == "Torque")
            {
                currentValue = d.torque;
            }
            else if (sliderStat == "Weight")
            {
                currentValue = d.weight;
            }
            else if (sliderStat == "Acceleration")
            {
                currentValue = d.acceleration;
            }
            else if (sliderStat == "MaxVelocity")
            {
                currentValue = d.maxVelocity;
            }
            else if (sliderStat == "Friction")
            {
                currentValue = d.friction;
            }

            newValue = currentValue;
        }

        if(newValue != currentValue)
        {
            if(newValue > currentValue)         //Green
            {
                sliders[0].value = newValue;
                sliders[0].transform.GetChild(1).GetComponentInChildren<Image>().color = Color.green;

                sliders[1].value = currentValue;
                sliders[1].transform.GetChild(1).GetComponentInChildren<Image>().color = mainColor;

            }
            else                                //Red
            {
                sliders[0].value = currentValue;
                sliders[0].transform.GetChild(1).GetComponentInChildren<Image>().color = Color.red;

                sliders[1].value = newValue;
                sliders[1].transform.GetChild(1).GetComponentInChildren<Image>().color = mainColor;

            }
        }
    }

    public void SetNewValue(float _value) { newValue = _value; }
}
