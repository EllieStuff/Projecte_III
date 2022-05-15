using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOptionClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    virtual public void Interact_Accept(bool _calledFromScript = false) { }
    virtual public void Interact_Right(bool _calledFromScript = false) { }
    virtual public void Interact_Left(bool _calledFromScript = false) { }

    virtual public void Select() { }
    virtual public void Deselect() { }

}
