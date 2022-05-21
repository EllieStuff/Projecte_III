using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptionButton : SettingsOptionClass
{
    Button bttn;

    public Button Button
    {
        get { return bttn; }
    }

    // Start is called before the first frame update
    void Start()
    {
        bttn = GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action(GameObject _action)
    {
        _action.SetActive(!_action.activeSelf);
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
