using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    public enum GameModes { MONO, MULTI_LOCAL /*, MULTI_ONLINE*/ };
    public GameModes gameMode = GameModes.MONO;

    public int numOfPlayers = 1;
    //public int numOfIAs = 0;
    public Transform[] players;
    //[SerializeField] Transform[] modifiers;
    bool sceneLoaded = false;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneLoaded) return;

        if (scene.name.Contains("Menu"))
        {
            // Do nothing
        }
        if (scene.name.Contains("Building Scene"))
        {
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    transform.GetChild(i).gameObject.SetActive(true);
            //}
        }
        else
        {
            if (gameMode == GameModes.MONO)
            {
                numOfPlayers = 1;
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject player = transform.GetChild(i).gameObject;
                    if (i < numOfPlayers)
                    {
                        player.SetActive(true);
                        //player.GetComponentInChildren<VehicleTriggerAndCollisionEvents>().Init();
                    }
                    else
                    {
                        //player.SetActive(false);
                    }
                }
            }

            sceneLoaded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPlayer(int _idx = 0)
    {
        if (gameMode == GameModes.MONO) return players[0];
        if (_idx >= numOfPlayers) return players[0];

        return players[_idx];
    }
    //public Transform GetPlayerModifier(int _idx = 0)
    //{
    //    if (gameMode == GameModes.MONO) return modifiers[0];

    //    return modifiers[_idx];
    //}


    public void InitPlayers()
    {
        Time.timeScale = 1.0f;
        for(int i = 0; i < numOfPlayers; i++)
        {
            players[i].gameObject.SetActive(true);
            players[i].GetComponent<VehicleTriggerAndCollisionEvents>().Init();
            players[i].GetComponent<PlayerVehicleScript>().Init();
            players[i].GetComponent<RandomModifierGet>().ResetModifiers();
        }
    }

    //public void RefreshNumOfPlayers()
    //{
    //    for (int i = numOfPlayers - numOfIAs; i < numOfPlayers; i++)
    //    {
    //        GetPlayer(i).GetComponent<PlayerInputs>().ResetControlData();
    //    }
    //    //numOfPlayers = numOfPlayers - numOfIAs;
    //    numOfIAs = 0;

    //    //numOfPlayers = numOfPlayers - numOfIAs;
    //    //numOfIAs = 0;
    //}


}
