using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    PlayerVehicleScript.GameModes gameMode = PlayerVehicleScript.GameModes.MONO;
    InputSystem inputSystem;
    InputSystem.ControlData[] controlData = new InputSystem.ControlData[1];

    // Keys
    [HideInInspector] 
    public bool
        forward, backward, right, left,
        drift, enableGadgetMenu, confirmGadget;

    // Axis
    [HideInInspector] 
    public Vector2
        chooseItem;

    public InputSystem.ControlData[] ControlData { get { return controlData; } }

    // Start is called before the first frame update
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("InputSystem") != null)
        inputSystem = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<InputSystem>();
        controlData[0] = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMode)
        {
            case PlayerVehicleScript.GameModes.MONO:
                if(controlData == null || controlData[0] == null)
                    controlData = inputSystem.GetAllControllersData();
                else
                {
                    UpdateInputs();
                }

                break;

            case PlayerVehicleScript.GameModes.MULTI_LOCAL:
                if (controlData[0] == null)
                    controlData[0] = inputSystem.GetActiveControllerData();
                else
                {
                    UpdateInputs();
                }

                break;

            default:
                break;
        }
    }

    void UpdateInputs()
    {
        //Keys
        forward = inputSystem.GetKey(InputSystem.KeyCodes.FORWARD, controlData);
        backward = inputSystem.GetKey(InputSystem.KeyCodes.BACKWARD, controlData);
        right = inputSystem.GetKey(InputSystem.KeyCodes.RIGHT, controlData);
        left = inputSystem.GetKey(InputSystem.KeyCodes.LEFT, controlData);
        drift = inputSystem.GetKey(InputSystem.KeyCodes.DRIFT, controlData);
        enableGadgetMenu = inputSystem.GetKey(InputSystem.KeyCodes.ENABLE_GADGET_MENU, controlData);
        confirmGadget = inputSystem.GetKey(InputSystem.KeyCodes.CONFIRM_GADGET, controlData);

        // Axis
        chooseItem = inputSystem.GetAxis(InputSystem.AxisCodes.CHOOSE_ITEM, controlData);
    }



    public void SetGameMode(PlayerVehicleScript.GameModes _gameMode)
    {
        gameMode = _gameMode;
    }

}
