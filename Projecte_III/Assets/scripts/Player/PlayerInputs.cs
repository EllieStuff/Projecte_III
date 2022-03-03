using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public enum GameModes { MONO, MULTI_LOCAL /*, MULTI_ONLINE*/ };

    GameModes gameMode = GameModes.MONO;
    InputSystem inputSystem;
    InputSystem.ControlData controlData = null;    // ToDo: Adapatar-ho a array

    // Keys
    [HideInInspector] 
    public bool
        forward, backward, right, left,
        drift, openGadgetMenu, confirmGadget;

    // Axis
    [HideInInspector] 
    public Vector2
        chooseItem;

    public InputSystem.ControlData ControlData { get { return controlData; } }

    // Start is called before the first frame update
    void Awake()
    {
        inputSystem = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<InputSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMode)
        {
            case GameModes.MONO:
                if (controlData == null)
                    controlData = inputSystem.GetActiveControllerData();
                else
                {
                    UpdateInputs();
                }

                break;

            case GameModes.MULTI_LOCAL:

                break;

            default:
                break;
        }
    }

    void UpdateInputs()
    {
        // Keys
        forward = inputSystem.GetKey(InputSystem.KeyCodes.FORWARD, controlData);
        backward = inputSystem.GetKey(InputSystem.KeyCodes.BACKWARD, controlData);
        right = inputSystem.GetKey(InputSystem.KeyCodes.RIGHT, controlData);
        left = inputSystem.GetKey(InputSystem.KeyCodes.LEFT, controlData);
        drift = inputSystem.GetKey(InputSystem.KeyCodes.DRIFT, controlData);
        openGadgetMenu = inputSystem.GetKey(InputSystem.KeyCodes.OPEN_GADGET_MENU, controlData);
        confirmGadget = inputSystem.GetKey(InputSystem.KeyCodes.CONFIRM_GADGET, controlData);

        // Axis
        chooseItem = inputSystem.GetAxis(InputSystem.AxisCodes.CHOOSE_ITEM, controlData);
    }



    public void SetGameMode(GameModes _gameMode)
    {
        gameMode = _gameMode;
    }

}
