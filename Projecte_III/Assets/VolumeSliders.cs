using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public AudioMixer masterMixer;
    [SerializeField] float sliderSpeedMult = 30.0f;

    public void SetMasterVolume(float _value)
    {
        masterMixer.SetFloat("_mainVolume", Mathf.Log10(_value) * sliderSpeedMult);
    }
    public void SetOSTVolume(float _value)
    {
        masterMixer.SetFloat("_ostVolume", Mathf.Log10(_value) * sliderSpeedMult);
    }
    public void SetSFXVolume(float _value)
    {
        masterMixer.SetFloat("_sfxVolume", Mathf.Log10(_value) * sliderSpeedMult);
    }

}
