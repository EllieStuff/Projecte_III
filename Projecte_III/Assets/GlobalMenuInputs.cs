using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMenuInputs : MonoBehaviour
{
    InputSystem inputSystem;
    InputSystem.ControlData[] controlData = new InputSystem.ControlData[1];


    bool updateInputs = false;

    [HideInInspector]
    public bool
        Up, Down, Right, Left,
        Accept, Decline, OpenMenu;

    Dictionary<InputSystem.KeyCodes, bool> keysPressed = new Dictionary<InputSystem.KeyCodes, bool>();


    public InputSystem.ControlData[] ControlData { get { return controlData; } }

    public bool UpPressed { get { return Up && !keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool DownPressed { get { return Down && !keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool RightPressed { get { return Right && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool LeftPressed { get { return Left && !keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool AcceptPressed { get { return Accept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool DeclinePressed { get { return Decline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool OpenMenuPressed { get { return OpenMenu && !keysPressed[InputSystem.KeyCodes.START]; } }
    public bool UpReleased { get { return !Up && keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool DownReleased { get { return !Down && keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool RightReleased { get { return !Right && keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool LeftReleased { get { return !Left && keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool AcceptReleased { get { return !Accept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool DeclineReleased { get { return !Decline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool OpenMenuReleased { get { return !OpenMenu && keysPressed[InputSystem.KeyCodes.START]; } }


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
    }

    private void Update()
    {
        if (!Inited())
            controlData = inputSystem.GetAllControllersData();
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
        Up = inputSystem.GetKey(InputSystem.KeyCodes.MENU_UP, controlData);
        Down = inputSystem.GetKey(InputSystem.KeyCodes.MENU_DOWN, controlData);
        Right = inputSystem.GetKey(InputSystem.KeyCodes.MENU_RIGHT, controlData);
        Left = inputSystem.GetKey(InputSystem.KeyCodes.MENU_LEFT, controlData);
        Accept = inputSystem.GetKey(InputSystem.KeyCodes.MENU_ACCEPT, controlData);
        Decline = inputSystem.GetKey(InputSystem.KeyCodes.MENU_DECLINE, controlData);
        OpenMenu = inputSystem.GetKey(InputSystem.KeyCodes.START, controlData);
    }

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
        if (OpenMenu && !keysPressed[InputSystem.KeyCodes.START])
        {
            keysPressed[InputSystem.KeyCodes.START] = true;
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
        if (!OpenMenu && keysPressed[InputSystem.KeyCodes.START])
        {
            keysPressed[InputSystem.KeyCodes.START] = false;
        }

    }


    public bool Inited()
    {
        return controlData != null && controlData[0] != null;
    }
}
