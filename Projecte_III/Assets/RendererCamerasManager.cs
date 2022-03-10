using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererCamerasManager : MonoBehaviour
{
    //CameraManager cameraManager;
    //int numOfPlayers;
    [System.Serializable]
    class RenderCamerasData
    {
        public Vector3 position, scale;
        public bool isActive = false;
    }
    [SerializeField] RenderCamerasData[] renderCamerasData1Player;
    [SerializeField] RenderCamerasData[] renderCamerasData2Players;
    [SerializeField] RenderCamerasData[] renderCamerasData3Players;
    [SerializeField] RenderCamerasData[] renderCamerasData4Players;

    // Start is called before the first frame update
    void Start()
    {
        CameraManager cameraManager = GetComponentInParent<CameraManager>();

        /// Modificar escales i posicions de les textures en funció del número de jugadors
        int numOfPlayers = cameraManager.playersManager.numOfPlayers;
        for (int i = 0; i < cameraManager.GetNumOfCameras(); i++)
        {
            Transform currRenderTexture = cameraManager.rendTexManager.GetRenderTexture(i);
            RenderCamerasData currCameraData = null;
            switch (numOfPlayers)
            {
                case 1:
                    currCameraData = renderCamerasData1Player[i];
                    break;

                case 2:
                    currCameraData = renderCamerasData2Players[i];
                    break;

                case 3:
                    currCameraData = renderCamerasData3Players[i];
                    break;

                case 4:
                    currCameraData = renderCamerasData4Players[i];
                    break;

                default:
                    break;
            }

            if (currCameraData != null)
            {
                Vector3 newPos = currCameraData.position + new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
                currRenderTexture.position = newPos;
                currRenderTexture.localScale = currCameraData.scale;
                currRenderTexture.gameObject.SetActive(currCameraData.isActive);
            }
            else 
                Debug.LogError("Current Camera Not Found at index " + i);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
