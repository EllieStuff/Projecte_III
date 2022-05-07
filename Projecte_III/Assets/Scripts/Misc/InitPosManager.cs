using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPosManager : MonoBehaviour
{
    [SerializeField] Transform[] initPoses;
    PlayersManager playersManager;

    private void Awake()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("ProceduralMap"))
        {
            SetInitPoses();
            playersManager.InitPlayers();
        }
    }


    public Transform GetInitPos(int _idx)
    {
        if (playersManager.gameMode == PlayersManager.GameModes.MONO) return initPoses[0];

        return initPoses[_idx];
    }

    void SetInitPoses()
    {
        for (int i = 0; i < playersManager.numOfPlayers; i++)
        {
            Transform currPlayer = playersManager.GetPlayer(i);
            currPlayer.parent.position = initPoses[i].position;
            //currPlayer.parent.localScale = initPoses[i].localScale;
            currPlayer.parent.rotation = transform.localRotation;
            currPlayer.rotation = transform.localRotation;
            currPlayer.position = initPoses[i].position;
        }
    }

}
