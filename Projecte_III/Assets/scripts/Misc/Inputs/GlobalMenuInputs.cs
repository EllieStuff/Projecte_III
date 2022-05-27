using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMenuInputs : MonoBehaviour
{
    InputSystem inputSystem;
    InputSystem.ControlData[] controlData = new InputSystem.ControlData[1];

    [SerializeField] bool refreshControlData = false;

    bool updateInputs = false;

    [HideInInspector]
    public InputSystem.KeyData
        UpData, DownData, RightData, LeftData,
        AcceptData, DeclineData, StartBttnData, EscapeBttnData;

    Dictionary<InputSystem.KeyCodes, bool> keysPressed = new Dictionary<InputSystem.KeyCodes, bool>();


    public InputSystem.ControlData[] ControlData { get { return controlData; } }

    public bool Up { get { return UpData.pressed; } }
    public bool Down { get { return DownData.pressed; } }
    public bool Right { get { return RightData.pressed; } }
    public bool Left { get { return LeftData.pressed; } }
    public bool Accept { get { return AcceptData.pressed; } }
    public bool Decline { get { return DeclineData.pressed; } }
    public bool StartBttn { get { return StartBttnData.pressed; } }
    public bool EscapeBttn { get { return EscapeBttnData.pressed; } }
    public bool UpPressed { get { return Up && !keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool DownPressed { get { return Down && !keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool RightPressed { get { return Right && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool LeftPressed { get { return Left && !keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool AcceptPressed { get { return Accept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool DeclinePressed { get { return Decline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool StartBttnPressed { get { return StartBttn && !keysPressed[InputSystem.KeyCodes.START]; } }
    public bool EscapeBttnPressed { get { return EscapeBttn && !keysPressed[InputSystem.KeyCodes.ESCAPE]; } }
    public bool UpReleased { get { return !Up && keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool DownReleased { get { return !Down && keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool RightReleased { get { return !Right && keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool LeftReleased { get { return !Left && keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool AcceptReleased { get { return !Accept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool DeclineReleased { get { return !Decline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool StartBttnReleased { get { return !StartBttn && keysPressed[InputSystem.KeyCodes.START]; } }
    public bool EscapeBttnReleased { get { return !EscapeBttn && keysPressed[InputSystem.KeyCodes.ESCAPE]; } }


    // Start is called before the first frame update
    void Start()
    {
        inputSystem = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<InputSystem>();
        controlData[0] = null;

        keysPressed.Add(InputSystem.KeyCodes.MENU_UP, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_DOWN, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_RIGHT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_LEFT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_ACCEPT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_DECLINE, false);
        keysPressed.Add(InputSystem.KeyCodes.START, false);
        keysPressed.Add(InputSystem.KeyCodes.ESCAPE, false);
    }

    private void Update()
    {
        if (!Inited())
        {
            controlData = inputSystem.GetAllControllersData();
            if (Inited() && refreshControlData) StartCoroutine(RefreshAllControllersData());
        }
        else
        {
            updateInputs = !updateInputs;
            if (updateInputs)
                UpdateInputs();
            else
                UpdateMap();
        }
    }

    void UpdateInputs()
    {
        UpData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_UP, controlData);
        DownData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_DOWN, controlData);
        RightData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_RIGHT, controlData);
        LeftData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_LEFT, controlData);
        AcceptData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_ACCEPT, controlData);
        DeclineData = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_DECLINE, controlData);
        StartBttnData = inputSystem.GetKeyData(InputSystem.KeyCodes.START, controlData);
        EscapeBttnData = inputSystem.GetKeyData(InputSystem.KeyCodes.ESCAPE, controlData);
    }

    //public int GetDeviceId(InputSystem.KeyCodes _key)
    //{
    //    int tmpDeviceId = -1;
    //    inputSystem.GetKey(_key, controlData, ref tmpDeviceId);
    //    return tmpDeviceId;
    //}

    void UpdateMap()
    {
        if (Up && !keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = true;
        }
        if (Down && !keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = true;
        }
        if (Right && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = true;
        }
        if (Left && !keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = true;
        }
        if (Accept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = true;
        }
        if (Decline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = true;
        }
        if (StartBttn && !keysPressed[InputSystem.KeyCodes.START])
        {
            keysPressed[InputSystem.KeyCodes.START] = true;
        }
        if (EscapeBttn && !keysPressed[InputSystem.KeyCodes.ESCAPE])
        {
            keysPressed[InputSystem.KeyCodes.ESCAPE] = true;
        }

        ResetMap();
    }


    void ResetMap()
    {
        if (!Up && keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = false;
        }
        if (!Down && keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = false;
        }
        if (!Right && keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = false;
        }
        if (!Left && keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = false;
        }
        if (!Accept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = false;
        }
        if (!Decline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = false;
        }
        if (!StartBttn && keysPressed[InputSystem.KeyCodes.START])
        {
            keysPressed[InputSystem.KeyCodes.START] = false;
        }
        if (!EscapeBttn && keysPressed[InputSystem.KeyCodes.ESCAPE])
        {
            keysPressed[InputSystem.KeyCodes.ESCAPE] = false;
        }

    }


    public bool Inited()
    {
        return controlData != null && controlData[0] != null;
    }



    IEnumerator RefreshAllControllersData()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            InputSystem.ControlData[] tmpControlData = inputSystem.GetAllControllersData();
            if (controlData.Length != tmpControlData.Length)
            {
                controlData = tmpControlData;
                AudioManager.Instance.Play_SFX("Click_SFX", 0.8f);
                yield return new WaitForSeconds(0.1f);
                AudioManager.Instance.Play_SFX("Click_SFX", 0.7f);
            }
        }
    }
}
