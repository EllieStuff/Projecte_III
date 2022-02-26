using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimerChrono : MonoBehaviour
{
    public float second;
    public int minute;
    public bool finishedRace;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if((Mathf.Round(second * 100) * 0.01f).ToString().Length <= 6)
        {
            if (minute < 10 && second < 10)
                text.text = "0" + minute + ":0" + Mathf.Round(second * 100) * 0.01f;
            else if (minute < 10 && second >= 10)
                text.text = "0" + minute + ":" + Mathf.Round(second * 100) * 0.01f;
            else if (minute >= 10 && second < 10)
                text.text = "0" + minute + ":0" + Mathf.Round(second * 100) * 0.01f;
            else if (minute >= 10 && second >= 10)
                text.text = minute + ":" + Mathf.Round(second * 100) * 0.01f;
        }

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
