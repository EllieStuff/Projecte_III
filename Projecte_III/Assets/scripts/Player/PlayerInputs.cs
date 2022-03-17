using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    PlayerVehicleScript.GameModes gameMode = PlayerVehicleScript.GameModes.MONO;
    private InputSystem inputSystem;
    InputSystem.ControlData[] controlData = new InputSystem.ControlData[1];

    // Keys
    float
        forward, backward, right, left, drift;
    bool
        enableGadgetMenu, confirmGadget, useGadget;

    // Axis
    Vector2
        chooseItem;

    public InputSystem.ControlData[] ControlData { get { return controlData; } }


    public float ForwardFloat { get { return forward; } }
    public float BackwardFloat { get { return forward; } }
    public float RightFloat { get { return forward + backward; } }
    public float LeftFloat { get { return forward + backward; } }
    public float DriftFloat { get { return forward; } }

    public bool Forward { get { return forward > InputSystem.INPUT_THRESHOLD; } }
    public bool Backward { get { return backward > InputSystem.INPUT_THRESHOLD; } }
    public bool Right { get { return right > InputSystem.INPUT_THRESHOLD; } }
    public bool Left { get { return left > InputSystem.INPUT_THRESHOLD; } }
    public bool Drift { get { return drift > InputSystem.INPUT_THRESHOLD; } }
    public bool EnableGadgetMenu { get { return enableGadgetMenu; } }
    public bool ConfirmGadget { get { return confirmGadget; } }
    public bool UseGadget { get { return useGadget; } }

    public Vector2 ChooseItem { get { return chooseItem; } }

    void Awake()
    {
        inputSystem = InputSystem.Instance;
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
        forward = inputSystem.GetKeyFloat(InputSystem.KeyCodes.FORWARD, controlData);
        backward = inputSystem.GetKeyFloat(InputSystem.KeyCodes.BACKWARD, controlData);
        right = inputSystem.GetKeyFloat(InputSystem.KeyCodes.RIGHT, controlData);
        left = inputSystem.GetKeyFloat(InputSystem.KeyCodes.LEFT, controlData);
        drift = inputSystem.GetKeyFloat(InputSystem.KeyCodes.DRIFT, controlData);
        enableGadgetMenu = inputSystem.GetKey(InputSystem.KeyCodes.ENABLE_GADGET_MENU, controlData);
        confirmGadget = inputSystem.GetKey(InputSystem.KeyCodes.CONFIRM_GADGET, controlData);
        useGadget = inputSystem.GetKey(InputSystem.KeyCodes.USE_GADGET, controlData);

        // Axis
        chooseItem = inputSystem.GetAxis(InputSystem.AxisCodes.CHOOSE_ITEM, controlData);
    }



    public void SetGameMode(PlayerVehicleScript.GameModes _gameMode)
    {
        gameMode = _gameMode;
    }

}
