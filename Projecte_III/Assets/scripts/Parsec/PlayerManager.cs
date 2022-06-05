using ParsecGaming;
using ParsecUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public Parsec.ParsecGuest m_AssignedGuest;
    PlayerVehicleScript player;
    PlayerInputs parsecInputs;
    internal InactiveScreensManager inactiveScreensManager;
    internal Transform changeColorManager;
    internal DoneButtonManager doneManager;
    internal GameManager gameManager;

    ChangeColor changeColorScript;

    bool 
        returnPressed = false,
        rightPressed = false,
        leftPressed = false;

    public void Setup()
    {
        DontDestroyOnLoad(gameObject);
        ParsecInput.AssignGuestToPlayer(m_AssignedGuest, m_PlayerNumber);
    }

    private void Update()
    {

        if (player == null || (SceneManager.GetActiveScene().name.Contains("Current") && inactiveScreensManager == null))
        {
            try
            {
                PlayersManager _playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
                player = _playersManager.GetPlayer(m_PlayerNumber - 1).GetComponent<PlayerVehicleScript>();
                
                if (player.playerNum > 0)
                {
                    parsecInputs = _playersManager.GetPlayer(m_PlayerNumber - 1).GetComponent<PlayerInputs>();
                    _playersManager.numOfPlayers++;
                    inactiveScreensManager.spawnParsecCar = true;
                    changeColorScript = changeColorManager.GetChild(player.playerNum).GetComponent<ChangeColor>();
                    changeColorScript.enabled = true;
                    _playersManager.GetPlayer(player.playerNum).GetComponent<IA>().parsecEnabled = true;
                }
            }
            catch(Exception e)
            {
                GameObject parsec = GameObject.Find("UI").transform.Find("Parsec").gameObject;
                GameObject.Find("UI").transform.Find("OnlineMultiplayerButton").Find("Online Button").GetComponent<PressedButton>().interactable = false;

                if (!parsec.activeSelf)
                {
                    parsec.SetActive(true);
                    parsec.transform.Find("Authentication").gameObject.SetActive(false);
                }
                else
                {
                    gameManager = GameObject.Find("UI").transform.Find("Parsec").Find("GameManager").GetComponent<GameManager>();

                    gameManager.SpawnPlayer(m_PlayerNumber, new Parsec.ParsecGuest());

                    Destroy(gameObject);
                }
            }
        }

        if (player != null && player.playerNum > 0)
        {
            parsecInputs.parsecP.forward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.W);
            parsecInputs.parsecP.backward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.S);
            parsecInputs.parsecP.left = ParsecInput.GetKey(player.playerNum + 1, KeyCode.A);
            parsecInputs.parsecP.right = ParsecInput.GetKey(player.playerNum + 1, KeyCode.D);
            parsecInputs.parsecP.upArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.UpArrow);
            parsecInputs.parsecP.downArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.DownArrow);
            parsecInputs.parsecP.leftArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.LeftArrow);
            parsecInputs.parsecP.rightArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.RightArrow);

            CheckReturn();
            CheckRight();
            CheckLeft();
        }
    }

    void CheckReturn()
    {
        if (!returnPressed && ParsecInput.GetKey(player.playerNum + 1, KeyCode.Return))
        {
            returnPressed = true;
            doneManager.GetButton(player.playerNum).SetReady();
        }
        else if (returnPressed && !ParsecInput.GetKey(player.playerNum + 1, KeyCode.Return))
            returnPressed = false;
    }
    void CheckRight()
    {
        if (!rightPressed && ParsecInput.GetKey(player.playerNum + 1, KeyCode.D))
        {
            rightPressed = true;
            changeColorScript.PressButton(changeColorScript.buttonRight);
            changeColorScript.SetNewColor(1);
        }
        else if (rightPressed && !ParsecInput.GetKey(player.playerNum + 1, KeyCode.D))
            rightPressed = false;
    }
    void CheckLeft()
    {
        if (!leftPressed && ParsecInput.GetKey(player.playerNum + 1, KeyCode.A))
        {
            leftPressed = true;
            changeColorScript.PressButton(changeColorScript.buttonLeft);
            changeColorScript.SetNewColor(-1);
        }
        else if (leftPressed && !ParsecInput.GetKey(player.playerNum + 1, KeyCode.A))
            leftPressed = false;
    }


    public void BreakDown()
    {
        //Destroy(m_Instance);
    }
}
