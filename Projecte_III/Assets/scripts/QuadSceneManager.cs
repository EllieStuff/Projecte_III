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
        if (scene.name != "Building Scene")
        {
            Transform initial = GameObject.Find("InitPos").transform;

            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            playerScript.buildingScene = false;
            playerScript.SetWheels();

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            SetCarModifiers();

        }
        else
        {
            playerScript.buildingScene = true;
        }
    }

    void SetCarModifiers()
    {
        Transform modifiers = transform.Find("Modifiers");
        for (int i = 0; i < modifiers.childCount; i++)
        {
            switch (modifiers.GetChild(i).GetChild(0).tag)
            {
                case "Floater":
                    playerScript.hasFloater = true;
                    break;

                default:
                    break;
            }

        }

    }

}
