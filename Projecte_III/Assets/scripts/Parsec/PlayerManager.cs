using ParsecGaming;
using ParsecUnity;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                GameObject _playersManager = GameObject.Find("PlayersManager");
                player = _playersManager.GetComponent<PlayersManager>().GetPlayer(m_PlayerNumber - 1).GetComponent<PlayerVehicleScript>();
                parsecInputs = _playersManager.GetComponent<PlayersManager>().GetPlayer(m_PlayerNumber - 1).GetComponent<PlayerInputs>();
                _playersManager.GetComponent<PlayersManager>().numOfPlayers++;
                inactiveScreensManager.spawnParsecCar = true;
                changeColorManager.GetChild(player.playerNum).GetComponent<ChangeColor>().enabled = true;
                if (player.playerNum > 0)
                    _playersManager.GetComponent<PlayersManager>().GetPlayer(player.playerNum).GetComponent<IA>().parsecEnabled = true;
            }
            catch(Exception e)
            {
                gameManager = GameObject.Find("UI").transform.Find("Parsec").Find("GameManager").GetComponent<GameManager>();

                gameManager.SpawnPlayer(m_PlayerNumber, new Parsec.ParsecGuest());

                Destroy(gameObject);
            }
        }

        if (player.playerNum > 0)
        {

            parsecInputs.parsecP.forward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.W);
            parsecInputs.parsecP.backward = ParsecInput.GetKey(player.playerNum + 1, KeyCode.S);
            parsecInputs.parsecP.left = ParsecInput.GetKey(player.playerNum + 1, KeyCode.A);
            parsecInputs.parsecP.right = ParsecInput.GetKey(player.playerNum + 1, KeyCode.D);
            parsecInputs.parsecP.upArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.UpArrow);
            parsecInputs.parsecP.downArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.DownArrow);
            parsecInputs.parsecP.leftArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.LeftArrow);
            parsecInputs.parsecP.rightArrow = ParsecInput.GetKey(player.playerNum + 1, KeyCode.RightArrow);

            if (doneManager != null && !doneManager.GetButton(player.playerNum).isReady && ParsecInput.GetKey(player.playerNum + 1, KeyCode.Return))
                doneManager.GetButton(player.playerNum).SetReady();
        }
    }

    public void BreakDown()
    {
        //Destroy(m_Instance);
    }
}
