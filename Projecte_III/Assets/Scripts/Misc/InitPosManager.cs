using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosManager : MonoBehaviour
{
    [SerializeField] Transform[] initPoses;
    PlayersManager.GameModes gameMode;

    private void Awake()
    {
        gameMode = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().gameMode;
    }


    public Transform GetInitPos(int _idx)
    {
        if (gameMode == PlayersManager.GameModes.MONO) return initPoses[0];

        return initPoses[_idx];
    }

}
