using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera[] renderCameras;

    internal PlayersManager playersManager;
    internal RenderTexturesManager rendTexManager;
    //int numOfPlayers = 1;

    // Start is called before the first frame update
    void Awake()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        rendTexManager = GameObject.FindGameObjectWithTag("RenderTexturesManager").GetComponent<RenderTexturesManager>();
        //numOfPlayers = playersManager.numOfPlayers;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNumOfCameras()
    {
        return renderCameras.Length;
    }
    public Camera GetCamera(int _idx = 0)
    {
        if (playersManager.gameMode == PlayersManager.GameModes.MONO) return renderCameras[0];

        return renderCameras[_idx];
    }
}
