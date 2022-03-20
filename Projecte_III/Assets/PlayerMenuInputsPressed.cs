using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuInputsPressed : MonoBehaviour
{
    [SerializeField] internal int playerId;

    PlayerInputs playerInputs;

    int updateInputs = 0;
    int updateMap = 2;


    Dictionary<InputSystem.KeyCodes, bool> keysPressed = new Dictionary<InputSystem.KeyCodes, bool>();

    public bool MenuUp { get { return playerInputs.MenuUp; } }
    public bool MenuDown { get { return playerInputs.MenuDown; } }
    public bool MenuRight { get { return playerInputs.MenuRight; } }
    public bool MenuLeft { get { return playerInputs.MenuLeft; } }
    public bool MenuAccept { get { return playerInputs.MenuAccept; } }
    public bool MenuDecline { get { return playerInputs.MenuDecline; } }
    public bool MenuUpPressed { get { return playerInputs.MenuUp && !keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool MenuDownPressed { get { return playerInputs.MenuDown && !keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool MenuRightPressed { get { return playerInputs.MenuRight && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool MenuLeftPressed { get { return playerInputs.MenuLeft && !keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool MenuAcceptPressed { get { return playerInputs.MenuAccept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool MenuDeclinePressed { get { return playerInputs.MenuDecline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }
    public bool MenuUpReleased { get { return !playerInputs.MenuUp && keysPressed[InputSystem.KeyCodes.MENU_UP]; } }
    public bool MenuDownReleased { get { return !playerInputs.MenuDown && keysPressed[InputSystem.KeyCodes.MENU_DOWN]; } }
    public bool MenuRightReleased { get { return !playerInputs.MenuRight && keysPressed[InputSystem.KeyCodes.MENU_RIGHT]; } }
    public bool MenuLeftReleased { get { return !playerInputs.MenuLeft && keysPressed[InputSystem.KeyCodes.MENU_LEFT]; } }
    public bool MenuAcceptReleased { get { return !playerInputs.MenuAccept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT]; } }
    public bool MenuDeclineReleased { get { return !playerInputs.MenuDecline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE]; } }


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
        if (playerInputs.Inited())
        {
            if (updateInputs == 0)
            {
                updateInputs++;
                playerInputs.EnableMenuInputs(true);
            }
            else if (updateInputs == 1)
            {
                updateInputs++;
                playerInputs.EnableMenuInputs(false);
            }
            
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (updateInputs == updateMap)
        {
            updateInputs = 0;
            UpdateMaps();
        }
    }

    void UpdateMaps()
    {
        if (playerInputs.MenuUp && !keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = true;
        }
        if (playerInputs.MenuDown && !keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = true;
        }
        if (playerInputs.MenuRight && !keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = true;
        }
        if (playerInputs.MenuLeft && !keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = true;
        }
        if (playerInputs.MenuAccept && !keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = true;
        }
        if (playerInputs.MenuDecline && !keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = true;
        }

        ResetMaps();
    }

    void ResetMaps()
    {
        if (!playerInputs.MenuUp && keysPressed[InputSystem.KeyCodes.MENU_UP])
        {
            keysPressed[InputSystem.KeyCodes.MENU_UP] = false;
        }
        if (!playerInputs.MenuDown && keysPressed[InputSystem.KeyCodes.MENU_DOWN])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DOWN] = false;
        }
        if (!playerInputs.MenuRight && keysPressed[InputSystem.KeyCodes.MENU_RIGHT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_RIGHT] = false;
        }
        if (!playerInputs.MenuLeft && keysPressed[InputSystem.KeyCodes.MENU_LEFT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_LEFT] = false;
        }
        if (!playerInputs.MenuAccept && keysPressed[InputSystem.KeyCodes.MENU_ACCEPT])
        {
            keysPressed[InputSystem.KeyCodes.MENU_ACCEPT] = false;
        }
        if (!playerInputs.MenuDecline && keysPressed[InputSystem.KeyCodes.MENU_DECLINE])
        {
            keysPressed[InputSystem.KeyCodes.MENU_DECLINE] = false;
        }

    }


    public bool Inited()
    {
        return playerInputs.Inited();
    }

}
