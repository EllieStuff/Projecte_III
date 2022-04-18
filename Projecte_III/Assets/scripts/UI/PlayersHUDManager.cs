using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHUDManager : MonoBehaviour
{
    PlayersHUD[] playerHud;

    // Start is called before the first frame update
    void Start()
    {
        playerHud = new PlayersHUD[transform.childCount];
        for (int i = 0; i < playerHud.Length; i++)
        {
            playerHud[i] = transform.GetChild(i).GetComponent<PlayersHUD>();
        }
    }

    public PlayersHUD GetPlayerHUD(int _idx)
    {
        return playerHud[_idx];
    }
}
