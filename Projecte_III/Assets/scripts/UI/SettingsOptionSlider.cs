using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOptionSlider : SettingsOptionClass
{
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
    }
    public override void Interact_Left(bool _calledFromScript = false)
    {
        Debug.Log("Left");
    }

    public override void Select()
    {

    }
    public override void Deselect()
    {

    }
}
