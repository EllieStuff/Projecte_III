using ParsecGaming;
using ParsecUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public int playerPos;
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

    //string lastSceneName = "Current Building Scene";

    float initTimer = 0;

    public void Setup(bool _isAdmin)
    {
        DontDestroyOnLoad(gameObject);
        ParsecInput.AssignGuestToPlayer(m_AssignedGuest, m_PlayerNumber);
        if (_isAdmin) playerPos = 0;
        else playerPos = m_PlayerNumber - 1;
        Debug.LogWarning("Player " + m_PlayerNumber + " at " + playerPos);
    }
    public void InitParsecPlayer(bool _spawnParsecCar)
    {
        if(player == null) 
        {
            PlayersManager _playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            player = _playersManager.GetPlayer(playerPos).GetComponent<PlayerVehicleScript>();
            //inactiveScreensManager = GameObject.Find("UI").transform.Find("InactiveScreens").GetComponent<InactiveScreensManager>();
            if (player.playerNum > 0)
            {
                parsecInputs = player.GetComponent<PlayerInputs>();
                _playersManager.numOfPlayers++;
                //_playersManager.numOfIAs++;
                //inactiveScreensManager.spawnParsecCar = _spawnParsecCar;
                inactiveScreensManager.SetNewCar();
                //inactiveScreensManager.spawnParsecCar = true;
                changeColorScript = changeColorManager.GetChild(player.playerNum).GetComponent<ChangeColor>();
                changeColorScript.enabled = true;
                _playersManager.GetPlayer(player.playerNum).GetComponent<IA>().parsecEnabled = true;
            }
        }
        else
        {
            if(GameObject.Find("UI") != null) 
            {
                GameObject parsec = GameObject.Find("UI").transform.Find("Parsec").gameObject;
                GameObject.Find("UI").transform.Find("Online Button").GetComponent<PressedButton>().interactable = false;

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
    }

    private void Update()
    {
        //string currSceneName = SceneManager.GetActiveScene().name;
        //bool inBuildingScene = currSceneName.Contains("Building Scene");
        
        //bool sceneChanged = currSceneName != lastSceneName;
        //if (sceneChanged)
        //{
        //    lastSceneName = SceneManager.GetActiveScene().name;

        //}

        bool inBuildingScene = SceneManager.GetActiveScene().name.Contains("Building Scene");
        if ((player == null || (inBuildingScene && inactiveScreensManager == null)) /*&& PlayerPrefs.GetString("RoomCreated", "false") == "true"*/)
        {
            initTimer += Time.deltaTime;
        }

        if(initTimer > 0.2f)
        {
            initTimer = 0;
            InitParsecPlayer(true);
        }

        if (player != null && player.playerNum > 0)
        {
            if (ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button6) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick2Button6)
                || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick3Button6) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick4Button6)) 
                Debug.LogError("In Vertical - Forwards");
            if (ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button5)) Debug.LogError("In Vertical - Backwards");
            if (ParsecInput.GetAxis(player.playerNum + 1, "Horizontal") > 0.1f || ParsecInput.GetAxis(player.playerNum + 1, "Horizontal") < -0.1f) Debug.LogError("In Horizontal");


            parsecInputs.parsecP.forward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.W) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button6);
            parsecInputs.parsecP.backward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.S) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button5);
            parsecInputs.parsecP.left = ParsecInput.GetKey(player.playerNum + 1, KeyCode.A) || ParsecInput.GetAxis(player.playerNum + 1, "Horizontal") > 0.1f;
            parsecInputs.parsecP.right = ParsecInput.GetKey(player.playerNum + 1, KeyCode.D) || ParsecInput.GetAxis(player.playerNum + 1, "Horizontal") < -0.1f;
            parsecInputs.parsecP.upArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.UpArrow) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button3);
            parsecInputs.parsecP.downArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.DownArrow) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button0);
            parsecInputs.parsecP.leftArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.LeftArrow) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button2);
            parsecInputs.parsecP.rightArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.RightArrow) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button1);


            if (inBuildingScene)
            {
                CheckReturn();
                CheckRight();
                CheckLeft();
            }
        }
    }

    void CheckReturn()
    {
        bool parsecReturnPressed =
            ParsecInput.GetKey(player.playerNum + 1, KeyCode.Return) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button7);
        if (!returnPressed && parsecReturnPressed)
        {
            returnPressed = true;
            doneManager.GetButton(player.playerNum).SetReady();
        }
        else if (returnPressed && !ParsecInput.GetKey(player.playerNum + 1, KeyCode.Return))
            returnPressed = false;
    }
    void CheckRight()
    {
        bool parsecRightPressed =
            ParsecInput.GetKey(player.playerNum + 1, KeyCode.D) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button1);
        if (!rightPressed && parsecRightPressed)
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
        bool parsecLeftPressed =
            ParsecInput.GetKey(player.playerNum + 1, KeyCode.A) || ParsecInput.GetKey(player.playerNum + 1, KeyCode.Joystick1Button2);
        if (!leftPressed && parsecLeftPressed)
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

    //bool InDelay()
    //{

    //}

}
