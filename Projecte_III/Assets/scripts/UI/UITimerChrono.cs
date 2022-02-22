using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimerChrono : MonoBehaviour
{
    public float second;
    public int minute;

    public bool finishedRace;

    void Update()
    {
        if (minute < 10 && second < 10)
            GetComponent<TextMeshProUGUI>().text = "00:0" + minute + ":0" + (int)second;
        else if (minute < 10 && second >= 10)
            GetComponent<TextMeshProUGUI>().text = "00:0" + minute + ":" + (int)second;
        else if (minute >= 10 && second < 10)
            GetComponent<TextMeshProUGUI>().text = "00:0" + minute + (int)second;
        else if (minute >= 10 && second >= 10)
            GetComponent<TextMeshProUGUI>().text = "00:" + minute + ":" + (int)second;

        if (!finishedRace)
            {
                second += Time.deltaTime;
                if (second >= 60)
                {
                    minute++;
                    second = 0;
                }
            }
    }
}
