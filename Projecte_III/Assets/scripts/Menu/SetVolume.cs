using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string mixerKey = "_volume";
    [SerializeField] string savedVolumeKey = "volumeKey";
    [SerializeField] float sliderSpeedMult = 30;
    
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        float volume = PlayerPrefs.GetFloat(savedVolumeKey, slider.maxValue);
        audioMixer.SetFloat(mixerKey, Mathf.Log10(volume) * sliderSpeedMult);
        slider.value = volume;
        Debug.Log("init value " + volume);

        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }


    void HandleSliderValueChanged(float _value)
    {
        audioMixer.SetFloat(mixerKey, Mathf.Log10(_value) * sliderSpeedMult);
        PlayerPrefs.SetFloat(savedVolumeKey, _value);
        Debug.Log("value " + _value);
    }

}
