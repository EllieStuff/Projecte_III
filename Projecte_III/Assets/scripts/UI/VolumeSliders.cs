using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    bool firstFrame = true;


    private void LateUpdate()
    {
        if (firstFrame)
        {
            firstFrame = false;
            gameObject.SetActive(false);
        }
    }

}
