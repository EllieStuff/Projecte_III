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

        if (scene.name.Contains("Menu"))
        {

            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    GameObject child = transform.GetChild(i).gameObject;
            //    if (child.activeSelf)
            //        child.SetActive(false);
            //}
        }
        else if (scene.name.Contains("Building Scene"))
        {
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    GameObject child = transform.GetChild(i).gameObject;
            //    if (!child.activeSelf)
            //        child.SetActive(true);
            //}

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(id);
            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    Transform child = transform.GetChild(i);
            //    if (child.tag == "ModifierSpots") continue;
            //    child.localPosition = Vector3.zero;
            //    if (child.name == "Player")
            //        child.localRotation = new Quaternion(0, 180, 0, 0);
            //    else
            //        child.localRotation = Quaternion.identity;
            //}

            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

            //playerScript.buildingScene = true;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 1)
            {
                Destroy(objs[1]);
            }
        }
        else if (scene.name != "Menu" && scene.name != "SceneSelector")
        {
            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(id);
            gameObject.transform.position = initial.position;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;


            GetComponentInChildren<VehicleTriggerAndCollisionEvents>().Init();

            sceneLoaded = true;
        }

    }

}
