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
        GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
        if (objs.Length < 2)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.activeSelf)
                    child.SetActive(false);
            }
        }
        else if (scene.name == "Building Scene")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (!child.activeSelf)
                    child.SetActive(true);
            }

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").transform;

            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.localPosition = Vector3.zero;
                if(child.name == "Player")
                    child.localRotation = new Quaternion(0, 180, 0, 0);
                else
                    child.localRotation = Quaternion.identity;

            }

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

            playerScript.buildingScene = true;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 1)
            {
                Destroy(objs[1]);
            }
        }
        else if (scene.name != "Menu")
        {
            Transform initial = GameObject.FindGameObjectWithTag("InitPos").transform;

            Debug.Log(initial.localPosition);

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
