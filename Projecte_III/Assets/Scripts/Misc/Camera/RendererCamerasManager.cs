using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RendererCamerasManager : MonoBehaviour
{
    Vector3 marginVector = new Vector3(0, 1, -4);

    //CameraManager cameraManager;
    //int numOfPlayers;
    //[System.Serializable]
    //class RenderCamerasData
    //{
    //    public Vector3 position, scale;
    //    public bool isActive = false;
    //}
    //[SerializeField] RenderCamerasData[] renderCamerasData1Player;
    //[SerializeField] RenderCamerasData[] renderCamerasData2Players;
    //[SerializeField] RenderCamerasData[] renderCamerasData3Players;
    //[SerializeField] RenderCamerasData[] renderCamerasData4Players;

    // Start is called before the first frame update
    void Start()
    {
        CameraManager cameraManager = GetComponentInParent<CameraManager>();

        /// Modificar escales i posicions de les textures en funció del número de jugadors
        int numOfPlayers = cameraManager.playersManager.numOfPlayers;
        cameraManager.rendTexManager.SetRenderSetup(numOfPlayers);

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Building Scene" || sceneName == "Building Scene Multiplayer") {
            if (numOfPlayers == 2)
            {
                Transform camera = cameraManager.GetCamera(0).transform;
                camera.position = camera.position + marginVector;
                camera = cameraManager.GetCamera(1).transform;
                camera.position = camera.position + marginVector;
            }
            else if (numOfPlayers == 3)
            {
                Transform camera = cameraManager.GetCamera(2).transform;
                camera.position = camera.position + marginVector;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
