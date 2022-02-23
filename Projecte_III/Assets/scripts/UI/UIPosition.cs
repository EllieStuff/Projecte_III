using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPosition : MonoBehaviour
{
    public int checkpointNumber;
    public int actualCheckpoint;

    void Update()
    {
        if(GetComponent<Slider>().value < actualCheckpoint)
            GetComponent<Slider>().value =  Mathf.Lerp(GetComponent<Slider>().value, actualCheckpoint, Time.deltaTime * 0.2f);
    }
}
