using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuadSceneManager : MonoBehaviour
{
    PlayerVehicleScript playerScript;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerScript = GetComponentInChildren<PlayerVehicleScript>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 0)
            {
                Destroy(objs[0]);
            }
        }
        else if (scene.name == "Building Scene")
        {
            playerScript.buildingScene = true;

        }
        else if (scene.name != "Menu")
        {
            Transform initial = GameObject.FindGameObjectWithTag("InitPos").transform;

            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            GetComponentInChildren<PlayerVehicleScript>().buildingScene = false;
            GetComponentInChildren<PlayerVehicleScript>().SetWheels();

            playerScript.HideVoidModifier();
            playerScript.SetWheels();
            playerScript.buildingScene = false;

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            SetCarModifiers();

            playerScript.Init();
        }

    }

    void SetCarModifiers()
    {
        Transform modifiers = transform.Find("Modifiers");
        playerScript.listOfModifiers = new List<string>(modifiers.childCount);
        for (int i = 0; i < modifiers.childCount; i++)
        {
            if(modifiers.GetChild(i).childCount > 0)
                playerScript.listOfModifiers.Add(modifiers.GetChild(i).GetChild(0).GetChild(0).tag);
        }
        playerScript.SetCarModifiers();

    }

}
