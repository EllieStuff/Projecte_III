using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptionSlider : SettingsOptionClass
{
    [SerializeField] Slider slider;
    float sliderSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }
    public override void Deselect()
    {

    }
}
