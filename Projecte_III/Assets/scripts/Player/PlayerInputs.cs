using ParsecUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    PlayersManager playersManagers;
    PlayerVehicleScript player;
    //PlayersManager.GameModes gameMode = PlayersManager.GameModes.MONO;
    InputSystem inputSystem;
    InputSystem.ControlData[] controlData = new InputSystem.ControlData[1];
    int playerId;
    string playerInputPath = "PlayerInputData";
    bool inputPathChecked = false;

    bool generalInputsEnabled = true, menuInputsEnabled = true;
    int itCount = 0;

    // Keys
    float
        forward, backward, right, left, drift;
    bool
        start, escape, shootForward, shootBackward, shootLeft, shootRight;
    bool
        mUp, mDown, mRight, mLeft, mAccept, mDecline;

    // Axis
    Vector2
        chooseItem;

    public ParsecPlayer parsecP;

    public struct ParsecPlayer
    {
        public bool forward;
        public bool backward;
        public bool left;
        public bool right;
        public bool leftArrow;
        public bool rightArrow;
        public bool upArrow;
        public bool downArrow;
    };

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
    public bool Start { get { return start; } }
    public bool Escape { get { return escape; } }
    public bool Drift { get { return drift > InputSystem.INPUT_THRESHOLD; } }
    public bool MenuUp { get { return mUp; } }
    public bool MenuDown { get { return mDown; } }
    public bool MenuRight { get { return mRight; } }
    public bool MenuLeft { get { return mLeft; } }
    public bool MenuAccept { get { return mAccept; } }
    public bool MenuDecline { get { return mDecline; } }
    public bool ShootForward { get { return shootForward; } }
    public bool ShootBackwards { get { return shootBackward; } }
    public bool ShootLeft { get { return shootLeft; } }
    public bool ShootRight { get { return shootRight; } }
    public bool ShootAny { get { return shootForward || shootBackward || shootLeft || shootRight; } }

    public Vector2 ChooseItem { get { return chooseItem; } }


    // Start is called before the first frame update
    void Awake()
    {
        ParsecPlayer parsecP = new ParsecPlayer();
        try
        {
            playerId = GetComponentInParent<PlayerData>().id;
        }
        catch (Exception)
        {
            playerId = 1;
        }
        playersManagers = transform.parent.GetComponentInParent<PlayersManager>();
        //if(GameObject.FindGameObjectWithTag("InputSystem") != null)
        inputSystem = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<InputSystem>();
        controlData[0] = null;
        playerInputPath = playerInputPath + playerId.ToString();

        //StartCoroutine(DelayToBeInited());
    }


    // Update is called once per frame
    void Update()
    {
        switch (playersManagers.gameMode)
        {
            case PlayersManager.GameModes.MONO:
                if (controlData == null || controlData[0] == null)
                    controlData = inputSystem.GetAllControllersData();
                else
                {
                    UpdateInputs();
                }

                break;

            case PlayersManager.GameModes.MULTI_LOCAL:
                if (controlData[0] == null)
                {
                    for (int i = 0; i <= playerId; i++)
                    {
                        if (i == playerId)
                        {
                            if (!inputPathChecked)
                            {
                                inputPathChecked = true;
                                int deviceId = PlayerPrefs.GetInt(playerInputPath, -1);
                                if (PlayerPrefs.GetInt(playerInputPath, -1) >= 0)
                                {
                                    controlData[0] = inputSystem.GetControllerData(PlayerPrefs.GetInt(playerInputPath));
                                    return;
                                }
                            }
                            else
                            {
                                controlData[0] = inputSystem.GetActiveControllerData();
                                if (controlData[0] != null)
                                    PlayerPrefs.SetInt(playerInputPath, controlData[0].mainDeviceId);
                            }
                        }
                        else if (!playersManagers.GetPlayer(i).GetComponent<PlayerInputs>().Inited())
                            break;
                    }

                    if (itCount >= playerId)
                    {
                        GameObject gameManagerGO = GameObject.FindGameObjectWithTag("GameManager");
                        if (gameManagerGO != null)
                        {
                            if (gameManagerGO.GetComponent<GameManager>().RoomCreated)
                                this.enabled = false;
                        }
                        //else
                        //{
                        //    this.enabled = false;
                        //}
                    }
                    else itCount++;
                }
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
        /// General Inputs
        if (generalInputsEnabled)
        {
            // Keys
            forward = inputSystem.GetKeyFloat(InputSystem.KeyCodes.FORWARD, controlData);
            backward = inputSystem.GetKeyFloat(InputSystem.KeyCodes.BACKWARD, controlData);
            right = inputSystem.GetKeyFloat(InputSystem.KeyCodes.RIGHT, controlData);
            left = inputSystem.GetKeyFloat(InputSystem.KeyCodes.LEFT, controlData);
            start = inputSystem.GetKeyData(InputSystem.KeyCodes.START, controlData).pressed;
            escape = inputSystem.GetKeyData(InputSystem.KeyCodes.ESCAPE, controlData).pressed;
            drift = inputSystem.GetKeyFloat(InputSystem.KeyCodes.DRIFT, controlData);

            shootForward = inputSystem.GetKeyData(InputSystem.KeyCodes.SHOOT_FORWARD, controlData).pressed;
            shootBackward = inputSystem.GetKeyData(InputSystem.KeyCodes.SHOOT_BACKWARD, controlData).pressed;
            shootLeft = inputSystem.GetKeyData(InputSystem.KeyCodes.SHOOT_LEFT, controlData).pressed;
            shootRight = inputSystem.GetKeyData(InputSystem.KeyCodes.SHOOT_RIGHT, controlData).pressed;

            // Axis
            //chooseItem = inputSystem.GetAxis(InputSystem.AxisCodes.CHOOSE_ITEM, controlData);
        }

        /// Menu Inputs
        if (menuInputsEnabled)
        {
            mUp = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_UP, controlData).pressed;
            mDown = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_DOWN, controlData).pressed;
            mRight = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_RIGHT, controlData).pressed;
            mLeft = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_LEFT, controlData).pressed;
            mAccept = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_ACCEPT, controlData).pressed;
            mDecline = inputSystem.GetKeyData(InputSystem.KeyCodes.MENU_DECLINE, controlData).pressed;
        }
    }

    public bool Inited()
    {
        bool cd1 = controlData != null, cd2 = false;
        if (cd1 && playerId == 1)
        {
            int debug1 = 0;
            cd2 = controlData[0] != null;
            if(cd2)
            {
                int debug2 = 0;
            }
        }
        return controlData != null && controlData[0] != null;
    }
    public bool UsesKeyboard()
    {
        for (int i = 0; i < controlData.Length; i++)
        {
            if (controlData[i].deviceType == InputSystem.DeviceTypes.KEYBOARD) return true;
        }
        return false;
    }

    public void EnableGeneralInputs(bool _enable)
    {
        generalInputsEnabled = _enable;
    }
    public void EnableMenuInputs(bool _enable)
    {
        menuInputsEnabled = _enable;
    }

    public void ResetControlData()
    {
        controlData = new InputSystem.ControlData[1] { null };
        PlayerPrefs.SetInt(playerInputPath, -1);
    }


    //IEnumerator DelayToBeInited()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    readyToBeInited = true;
    //}

}
