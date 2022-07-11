using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuInputsPressed : MonoBehaviour
{
    [SerializeField] internal int playerId;

    PlayerInputs playerInputs;

    int updateInputs = 0;
    int updateMap = 2;


    bool
        up, down, right, left,
        accept, decline;

    Dictionary<InputSystem.KeyCodes, bool> keysPressed = new Dictionary<InputSystem.KeyCodes, bool>();

    public bool MenuUp { get { return up; } }
    public bool MenuDown { get { return down; } }
    public bool MenuRight { get { return right; } }
    public bool MenuLeft { get { return left; } }
    public bool MenuAccept { get { return accept; } }
    public bool MenuDecline { get { return decline; } }
    public bool MenuUpPressed { get { return up && !keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool MenuDownPressed { get { return down && !keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool MenuRightPressed { get { return right && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool MenuLeftPressed { get { return left && !keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool MenuAcceptPressed { get { return accept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool MenuDeclinePressed { get { return decline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool MenuUpReleased { get { return !up && keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool MenuDownReleased { get { return !down && keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool MenuRightReleased { get { return !right && keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool MenuLeftReleased { get { return !left && keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool MenuAcceptReleased { get { return !accept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool MenuDeclineReleased { get { return !decline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }


    // Start is called before the first frame update
    void Start()
    {
        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerInputs = playersManager.GetPlayer(playerId).GetComponent<PlayerInputs>();

        keysPressed.Add(InputSystem.KeyCodes.MENU_UP, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_DOWN, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_RIGHT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_LEFT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_ACCEPT, false);
        keysPressed.Add(InputSystem.KeyCodes.MENU_DECLINE, false);
    }

    private void Update()
    {
        if(playerInputs == null)
        {
            PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            playerInputs = playersManager.GetPlayer(playerId).GetComponent<PlayerInputs>();
        }

        if (playerInputs.Inited())
        {
            if(updateInputs == 0)
            {
                updateInputs++;
                UpdateInputs();
            }
            else if(updateInputs == 1)
            {
                updateInputs = 0;
                UpdateMaps();
            }


            //if (updateInputs == 0)
            //{
            //    updateInputs++;
            //    playerInputs.EnableMenuInputs(true);
            //}
            //else if (updateInputs == 1)
            //{
            //    updateInputs++;
            //    playerInputs.EnableMenuInputs(false);
            //}
            
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if (updateInputs == updateMap)
        //{
        //    updateInputs = 0;
        //    UpdateMaps();
        //}
    }

    void UpdateInputs()
    {
        up = playerInputs.MenuUp;
        down = playerInputs.MenuDown;
        right = playerInputs.MenuRight;
        left = playerInputs.MenuLeft;
        accept = playerInputs.MenuAccept;
        decline = playerInputs.MenuDecline;
    }

    void UpdateMaps()
    {
        if (up && !keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = true;
        }
        if (down && !keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = true;
        }
        if (right && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = true;
        }
        if (left && !keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = true;
        }
        if (accept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = true;
        }
        if (decline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = true;
        }

        ResetMaps();
    }

    void ResetMaps()
    {
        if (!up && keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = false;
        }
        if (!down && keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = false;
        }
        if (!right && keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = false;
        }
        if (!left && keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = false;
        }
        if (!accept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = false;
        }
        if (!decline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = false;
        }

    }


    public bool Inited()
    {
        return playerInputs.Inited();
    }
    public bool UsesKeyboard()
    {
        return playerInputs.UsesKeyboard();
    }

}
