using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera[] renderCameras;

    internal PlayersManager playersManager;
    internal RenderTexturesManager rendTexManager;

    // Start is called before the first frame update
    void Awake()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        rendTexManager = GameObject.FindGameObjectWithTag("RenderTexturesManager").GetComponent<RenderTexturesManager>();
        //numOfPlayers = playersManager.numOfPlayers;
    }

    private void Start()
    {
        for(int i = 0; i < renderCameras.Length; i++)
        {
            if (i < playersManager.numOfPlayers)
                renderCameras[i].gameObject.SetActive(true);
            else
                renderCameras[i].gameObject.SetActive(false);
        }
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
    public Transform GetRendererCamera(int _idx = 0)
    {
        if (playersManager.gameMode == PlayersManager.GameModes.MONO) return rendTexManager.transform.GetChild(0);

        return rendTexManager.transform.GetChild(_idx);
    }
}
