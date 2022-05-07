using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static RandomModifierGet;

public class RoundManager : MonoBehaviour
{
    [SerializeField] GameObject WinnerUI;
    [SerializeField] TextMeshProUGUI WinnerText;
    [SerializeField] GameObject inGameMenu;
    private PlayersManager playersManager;
    private int _carsAlive;
    internal bool roundFinished;
    internal int playerWinner;
    private InitPlayerManager initPos;

    void Start()
    {
        initPos = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPlayerManager>();
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
    }

    void Update()
    {
        _carsAlive = CheckPlayersAlive();
        if(playersManager.numOfPlayers > 1 && _carsAlive == 1 && !roundFinished)
        {
            playerWinner = GetPlayerWinner();
            WinnerUI.SetActive(true);
            WinnerText.text = "Player "+ (playerWinner + 1) + " Wins!";
            inGameMenu.SetActive(false);
            StartCoroutine(StopTime());
            roundFinished = true;
        }
        else if(_carsAlive == 2 && playersManager.numOfPlayers > 2)
        {
            AudioManager.Instance.OST_AudioSource.pitch = 1.2f;
        }
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


    IEnumerator StopTime()
    {
        float timer = 0.0f, maxTime = 1.0f;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(1, 0, timer / maxTime);
        }
        Time.timeScale = 0.0f;
    }

}
