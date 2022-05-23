using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsButton : SettingsOptionClass
{
    Button bttn;
    AudioSettings audioObj;

    // Start is called before the first frame update
    void Start()
    {
        bttn = GetComponentInChildren<Button>();
        audioObj = transform.parent.GetComponentInChildren<AudioSettings>();
    }

    public override void Init(GlobalMenuInputs _inputs, InputSystem.KeyData _playerManaging)
    {
        audioObj.Init(_inputs, _playerManaging);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Action(GlobalMenuInputs _inputs, InputSystem.KeyData _playerManaging)
    {
        audioObj.gameObject.SetActive(!audioObj.gameObject.activeSelf);
    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        bttn.onClick.Invoke();
        bttn.gameObject.SetActive(false);
    }
    public override void Interact_Right(bool _calledFromScript = false)
    {

    }
    public override void Interact_Left(bool _calledFromScript = false)
    {

    }

    public override void Select()
    {

    }
    public override void Deselect()
    {


    }
}
