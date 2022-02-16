using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuadSceneManager : MonoBehaviour
{
    PlayerVehicleScript playerScript;

    public bool multiplayerMode;

    private static QuadSceneManager quadSMInstance = null;

    public static QuadSceneManager Instance { get { return quadSMInstance; } }

    private void Awake()
    {
        if (Instance != null && Instance != this && !multiplayerMode)
        {
            Destroy(gameObject);
            return;
        }

        quadSMInstance = this;

        DontDestroyOnLoad(gameObject);

        playerScript = GetComponentInChildren<PlayerVehicleScript>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Building Scene"))
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
