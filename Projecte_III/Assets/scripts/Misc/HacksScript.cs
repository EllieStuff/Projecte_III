using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacksScript : MonoBehaviour
{
    [SerializeField] VehicleTriggerAndCollisionEvents[] players;
    private RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayersManager _players = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();

        players = new VehicleTriggerAndCollisionEvents[_players.numOfPlayers];

        for (int i = 0; i < _players.numOfPlayers; i++)
            players[i] = _players.GetPlayer(i).GetComponent<VehicleTriggerAndCollisionEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        Hacks();
    }

    private void InfiniteLifes()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log(!players[i].infiniteLifes);
            players[i].infiniteLifes = !players[i].infiniteLifes;
        }
    }

    private void Hacks()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            InfiniteLifes();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerThrowPlunger>().SetPlungerModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<Umbrella>().SetUmbrellaModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerOilGun>().SetOilGunModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<PlayerPaintGun>().SetPaintGunModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<SaltoBomba>().SetSaltoBombaModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<BoostModifierScript>().SetBoostModifier();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            if(roundManager.roundFinished)
            {
                roundManager.WinnerUI.SetActive(false);
                roundManager.StopTimescale();
            }

            roundManager.enabled = !roundManager.enabled;
        }
    }

}
