using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera[] renderCameras;

    internal PlayersManager playersManager;
    bool inGameScene;

    // Start is called before the first frame update
    void Awake()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        GameObject rendTexManagerGO = GameObject.FindGameObjectWithTag("RenderTexturesManager");
        //numOfPlayers = playersManager.numOfPlayers;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNumOfCameras()
    {
        return renderCameras.Length;
    }
    public Camera GetCamera()
    {
        return mainCamera;
    }
    public Camera GetCamera(int _idx = 0)
    {
        return mainCamera;
        //if (inGameScene) return mainCamera;


        //if (playersManager.gameMode == PlayersManager.GameModes.MONO) return renderCameras[0];

        //return renderCameras[_idx];
    }
    public Transform GetRendererCamera(int _idx = 0)
    {
        if (inGameScene)
        {
            Debug.LogError("Renderercamera was asked for in a game Scene");
        }
        return null;


        //if (playersManager.gameMode == PlayersManager.GameModes.MONO) return rendTexManager.transform.GetChild(0);

        //return rendTexManager.transform.GetChild(_idx);
    }
}
