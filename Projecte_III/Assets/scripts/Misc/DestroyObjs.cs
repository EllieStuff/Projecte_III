using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject UI = GameObject.Find("DontDestroyOnLoad_UI");
        if(UI != null) Destroy(UI);

        GameObject playersManager = GameObject.FindGameObjectWithTag("PlayersManager");
        if(playersManager != null) Destroy(playersManager);

        PlayerPrefs.SetString("RoomCreated", "false");
        PlayerPrefs.SetString("LastShortUrl", "null");

        //for (int i = 0; i < 4; i++)
        //{
        //    PlayerPrefs.SetInt("ParsecPlayerId" + i, -1);
        //}

        Destroy(gameObject);
    }
}
