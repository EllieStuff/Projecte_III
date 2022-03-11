using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public enum GameModes { MONO, MULTI_LOCAL /*, MULTI_ONLINE*/ };
    public GameModes gameMode = GameModes.MONO;

    public InputSystem inputSystem;
    public int numOfPlayers = 1;
    [SerializeField] Transform[] players;
    [SerializeField] Transform[] modifiers;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
