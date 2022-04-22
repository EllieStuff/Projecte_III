using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [SerializeField] GameObject WinnerUI;
    [SerializeField] TextMeshProUGUI WinnerText;
    private PlayersManager playersManager;
    private int _carsAlive;
    internal bool roundFinished;
    internal int playerWinner;
    private InitPosManager initPos;

    void Start()
    {
        initPos = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>();
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
    }

    void Update()
    {
        _carsAlive = CheckPlayersAlive();
        if(_carsAlive == 1 && !roundFinished)
        {
            playerWinner = GetPlayerWinner();
            WinnerUI.SetActive(true);
            WinnerText.text = "Player: "+ (playerWinner + 1) + " Wins!";
            roundFinished = true;
        }
    }

    public void ResetScene()
    {
        for (int i = 0; i < playersManager.players.Length; i++)
        {
            PlayerVehicleScript player = playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>();
            player.lifes = 3;
            player.timerStartRace = 7;
            Transform _initPos = initPos.GetInitPos(i);
            player.transform.position = _initPos.position;
            player.transform.rotation = _initPos.rotation * Quaternion.Euler(0, 180, 0);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    int CheckPlayersAlive()
    {
        int carsAlive = 0;
        for (int i = 0; i < playersManager.players.Length; i++)
        {
            if (playersManager.GetPlayer(i).transform.parent.gameObject.activeSelf && playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().lifes > 0)
                carsAlive++;
        }
        return carsAlive;
    }

    int GetPlayerWinner()
    {
        for (int i = 0; i < playersManager.players.Length; i++)
        {
            if (playersManager.GetPlayer(i).transform.parent.gameObject.activeSelf && playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().lifes > 0)
                return i;
        }
        return 0;
    }
}
