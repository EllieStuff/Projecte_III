using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOptionClass : MonoBehaviour
{
    [HideInInspector] public InputSystem.KeyData playerManaging = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //virtual public void OnOpenMenu() { }

    virtual public void Interact_Accept(bool _calledFromScript = false) { }
    virtual public void Interact_Right(bool _calledFromScript = false) { }
    virtual public void Interact_Left(bool _calledFromScript = false) { }

    virtual public void Select() { }
    virtual public void Deselect() { }

    virtual public void SetPlayerManaging(InputSystem.KeyData _playerManaging)
    {
        playerManaging = _playerManaging;
    }
    public bool MouseFilterCheck(bool _calledFromScript)
    {
        return playerManaging != null && !((!_calledFromScript && playerManaging.deviceType == InputSystem.DeviceTypes.KEYBOARD) || _calledFromScript);
    }

}
