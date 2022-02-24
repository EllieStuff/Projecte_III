using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPosition : MonoBehaviour
{
    public int checkpointNumber;
    public int actualCheckpoint;
    private Slider sliderPos;

    private void Start()
    {
        sliderPos = GetComponent<Slider>();
    }

    void Update()
    {
        if(sliderPos.value < actualCheckpoint)
            sliderPos.value =  Mathf.Lerp(sliderPos.value, actualCheckpoint, Time.deltaTime * 0.2f);
    }
}
