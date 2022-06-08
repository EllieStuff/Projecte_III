using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptionSlider : SettingsOptionClass
{
    [SerializeField] Slider slider;
    [SerializeField] Transform handle;

    float sliderSpeed = 0.1f;
    float sliderRotEffectSpeed = -0.08f;
    float savedValue;

    bool sliderSelected;
    bool validValueChange = true;

    Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        sliderSelected = false;
        initialRot = handle.rotation;
        savedValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderSelected)
        {
            Quaternion rot = handle.rotation * Quaternion.Euler(new Vector3(0.0f, 0.0f, sliderRotEffectSpeed * Time.unscaledTime));
            handle.rotation = rot;
        }
    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        Debug.Log("Accept");
    }
    public override void Interact_Right(bool _calledFromScript = false)
    {
        if (MouseFilterCheck(_calledFromScript))
            return;

        Debug.Log("Right");
        slider.value += sliderSpeed;
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        if (MouseFilterCheck(_calledFromScript))
            return;

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

    public override void SetPlayerManaging(InputSystem.KeyData _playerManaging)
    {
        base.SetPlayerManaging(_playerManaging);
        if (_playerManaging == null) {
            slider.interactable = true;
            return;
        }

        if (_playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD)
            slider.interactable = true;
        else if (_playerManaging.deviceType == InputSystem.DeviceTypes.CONTROLLER)
            slider.interactable = false;

    }


    public void CheckIfValidChange()
    {
        //if (!validValueChange || playerManaging == null) return;

        //if (playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD && MouseFilterCheck(false))
        //{
        //    validValueChange = false;
        //    slider.value = savedValue;
        //    validValueChange = true;
        //}
        //else
        //{
        //    savedValue = slider.value;
        //}

    }

}
