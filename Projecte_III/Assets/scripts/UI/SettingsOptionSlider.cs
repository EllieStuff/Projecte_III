using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptionSlider : SettingsOptionClass
{
    [SerializeField] Slider slider;
    float sliderSpeed = 0.1f;

    bool sliderSelected;
    [SerializeField] Transform handle;

    Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        sliderSelected = false;

        initialRot = handle.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderSelected)
        {
            Quaternion rot = handle.rotation * Quaternion.Euler(new Vector3(0.0f, 0.0f, -90.0f * Time.deltaTime));
            handle.rotation = rot;
        }
    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        Debug.Log("Accept");
    }
    public override void Interact_Right(bool _calledFromScript = false)
    {
        Debug.Log("Right");
        slider.value += sliderSpeed;
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        Debug.Log("Left");
        slider.value -= sliderSpeed;
    }

    public override void Select()
    {
        sliderSelected = true;
    }
    public override void Deselect()
    {
        sliderSelected = false;

        handle.rotation = initialRot;
    }
}
