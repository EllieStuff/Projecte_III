using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    public enum GameModes { MONO, MULTI_LOCAL /*, MULTI_ONLINE*/ };
    public GameModes gameMode = GameModes.MONO;

    public InputSystem inputSystem;
    public int numOfPlayers = 1;
    public Transform[] players;
    [SerializeField] Transform[] modifiers;

    bool disableFunctions;

    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Menu"))
        {
            // Do nothing
        }
        else if (scene.name.Contains("Building Scene"))
            disableFunctions = true;
        if (scene.name == "Building Scene Multiplayer" && !disableFunctions)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (scene.name == "Building Scene" && !disableFunctions)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if(!disableFunctions)
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
                    if (i < numOfPlayers)
                        transform.GetChild(i).gameObject.SetActive(true);
                    else
                        transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPlayer(int _idx = 0)
    {
        if (gameMode == GameModes.MONO) return players[0];

        return players[_idx];
    }
    public Transform GetPlayerModifier(int _idx = 0)
    {
        if (gameMode == GameModes.MONO) return modifiers[0];

        return modifiers[_idx];
    }

}
