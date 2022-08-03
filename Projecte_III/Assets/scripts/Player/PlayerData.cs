using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int id;

    //PlayersManager playersManager;
    //GameObject player;
    //PlayerVehicleScript playerScript;
    bool mainMenuSceneLoaded = false, buildingMenuSceneLoaded = false, gameSceneLoaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        //player = playersManager.GetPlayer(id).gameObject;
        //playerScript = player.GetComponent<PlayerVehicleScript>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
        if (objs.Length < 2)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (gameSceneLoaded) return;

        if (scene.name.Contains("Building Scene") && !buildingMenuSceneLoaded)
        {
            PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            PlayerVehicleScript playerScript = playersManager.GetPlayer(id).GetComponent<PlayerVehicleScript>();

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPlayerManager>().GetInitPos(id);
            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 1)
            {
                Destroy(objs[1]);
            }

            //buildingMenuSceneLoaded = true;
        }
        else if (scene.name != "Menu" && !gameSceneLoaded)
        {
            //Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(id);
            //gameObject.transform.position = initial.position;
            //gameObject.transform.localRotation = initial.localRotation;
            //gameObject.transform.localScale = initial.localScale;

            PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
            PlayerVehicleScript playerScript = playersManager.GetPlayer(id).GetComponent<PlayerVehicleScript>();

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            GetComponentInChildren<VehicleTriggerAndCollisionEvents>().Init();

            //gameSceneLoaded = true;
        }

    }

}
