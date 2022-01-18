using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuadSceneManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Building Scene")
        {
            Transform initial = GameObject.Find("InitPos").transform;

            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            GetComponentInChildren<PlayerVehicleScript>().buildingScene = false;
            GetComponentInChildren<PlayerVehicleScript>().SetWheels();

            Rigidbody rb = transform.GetChild(0).GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;

            rb.useGravity = true;
        }
        else
        {
            GetComponentInChildren<PlayerVehicleScript>().buildingScene = true;
        }
    }
}
