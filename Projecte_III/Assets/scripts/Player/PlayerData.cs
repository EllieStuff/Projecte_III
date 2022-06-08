using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int id;

    PlayersManager playersManager;
    GameObject player;
    PlayerVehicleScript playerScript;
    bool sceneLoaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        player = playersManager.GetPlayer(id).gameObject;
        playerScript = player.GetComponent<PlayerVehicleScript>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
        if (objs.Length < 2)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneLoaded) return;

        if (scene.name.Contains("Building Scene"))
        {

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPlayerManager>().GetInitPos(id);
            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 1)
            {
                Destroy(objs[1]);
            }
        }
        else if (scene.name != "Menu")
        {
            //Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(id);
            //gameObject.transform.position = initial.position;
            //gameObject.transform.localRotation = initial.localRotation;
            //gameObject.transform.localScale = initial.localScale;

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            GetComponentInChildren<VehicleTriggerAndCollisionEvents>().Init();

            sceneLoaded = true;
        }

    }

}
