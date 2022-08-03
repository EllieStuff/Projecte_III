using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaIgnition : MonoBehaviour
{
    [SerializeField] PlayersManager players;
    [SerializeField] PlayerVehicleScript localPlayer;
    const float DISTANCE = 2f;
    const float JUMPSPEED = 2.5f;

    public void ThrowPlayer()
    {
        if(players == null)
        players = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();

        for (int i = 0; i < players.players.Length; i++)
        {
            Transform otherPlayer = players.GetPlayer(i);
            if (i != localPlayer.playerNum && Vector3.Distance(transform.GetChild(0).position, otherPlayer.transform.position) <= DISTANCE)
            {
                otherPlayer.GetComponent<PlayerVehicleScript>().vehicleRB.velocity = new Vector3(0, JUMPSPEED * 8, 0);
                break;
            }
        }
    }

    public bool CanThrowPlayer()
    {
        if (players == null)
            players = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();

        for (int i = 0; i < players.players.Length; i++)
        {
            Transform otherPlayer = players.GetPlayer(i);
            if (i != localPlayer.playerNum && Vector3.Distance(transform.GetChild(0).position, otherPlayer.transform.position) <= DISTANCE)
            {
                return true;
            }
        }

        return false;
    }
}
