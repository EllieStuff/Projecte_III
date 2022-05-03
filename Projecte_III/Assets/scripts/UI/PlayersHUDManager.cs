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
        int playersNum = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().numOfPlayers;
        for (int i = 0; i < playerHud.Length; i++)
        {
            playerHud[i] = transform.GetChild(i).GetComponent<PlayersHUD>();
            playerHud[i].id = i;
            if (i >= playersNum)
                playerHud[i].gameObject.SetActive(false);
            
            if(playersNum == 1)
                playerHud[i].GetComponent<RectTransform>().position += new Vector3(700, 0, 0);
            else if (playersNum == 2)
                playerHud[i].GetComponent<RectTransform>().position += new Vector3(500, 0, 0);
            else if (playersNum == 3)
                playerHud[i].GetComponent<RectTransform>().position += new Vector3(200, 0, 0);
        }
    }

    public PlayersHUD GetPlayerHUD(int _idx)
    {
        return playerHud[_idx];
    }
}
