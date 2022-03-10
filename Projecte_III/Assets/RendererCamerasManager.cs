using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererCamerasManager : MonoBehaviour
{
    PlayersManager playerManager;
    int numOfPlayers = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayersManager>();
        //numOfPlayers = playerManager.numOfPlayers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
