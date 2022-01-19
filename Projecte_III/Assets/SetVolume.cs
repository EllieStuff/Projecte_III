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
    
    Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        float volume = PlayerPrefs.GetFloat(savedVolumeKey, 1.0f);
        audioMixer.SetFloat(mixerKey, volume);
        slider.value = volume;

        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }


    void HandleSliderValueChanged(float _value)
    {
        audioMixer.SetFloat(mixerKey, _value);
        PlayerPrefs.SetFloat(savedVolumeKey, _value);
    }

}
