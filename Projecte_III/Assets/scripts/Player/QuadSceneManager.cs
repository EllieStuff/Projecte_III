using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuadSceneManager : MonoBehaviour
{
    public int playerId;

    PlayersManager playersManager;
    GameObject player;
    PlayerVehicleScript playerScript;
    string[] listOfAllModifiers = { "Floater", "PaintGun", "OilGun", "Plunger", "AlaDelta", "ChasisElevation" };
    bool sceneLoaded;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        player = playersManager.GetPlayer(playerId).gameObject;
        playerScript = player.GetComponent<PlayerVehicleScript>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
        if (objs.Length < 2)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Menu") && !sceneLoaded)
        {
           
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    GameObject child = transform.GetChild(i).gameObject;
            //    if (child.activeSelf)
            //        child.SetActive(false);
            //}
        }
        else if (scene.name.Contains("Building Scene") && !sceneLoaded)
        {
            ModifierManager modfs = playersManager.GetPlayerModifier(playerId).GetComponent<ModifierManager>(); //GameObject.FindGameObjectWithTag("ModifierSpots").GetComponent<ModifierManager>();
            Transform quad = playersManager.GetPlayer(playerId).GetChild(0).GetChild(0); //GameObject.FindGameObjectWithTag("PlayerVehicle").transform.GetChild(0);
            modfs.SetNewModifierSpots(quad.GetChild(quad.childCount - 1));
            modfs.Active(false);

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (!child.activeSelf)
                    child.SetActive(true);
            }

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(playerId);

            gameObject.transform.localPosition = initial.localPosition;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.tag == "ModifierSpots") continue;
                child.localPosition = Vector3.zero;
                if (child.name == "Player")
                    child.localRotation = new Quaternion(0, 180, 0, 0);
                else
                    child.localRotation = Quaternion.identity;
            }

            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;

            playerScript.buildingScene = true;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("VehicleSet");
            if (objs.Length > 1)
            {
                Destroy(objs[1]);
            }
        }
        else if (scene.name != "Menu" && scene.name != "SceneSelector" && !sceneLoaded)
        {
            playersManager.GetPlayerModifier(playerId).GetComponent<ModifierManager>().Active(false);

            Transform initial = GameObject.FindGameObjectWithTag("InitPos").GetComponent<InitPosManager>().GetInitPos(playerId);

            gameObject.transform.position = initial.position;
            gameObject.transform.localRotation = initial.localRotation;
            gameObject.transform.localScale = initial.localScale;

            GetComponentInChildren<PlayerVehicleScript>().buildingScene = false;
            GetComponentInChildren<PlayerVehicleScript>().SetWheels();

            playerScript.SetWheels();
            playerScript.buildingScene = false;

            Rigidbody rb = playerScript.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            SetCarModifiers();

            sceneLoaded = true;
        }

    }

    void SetCarModifiers()
    {
        Transform modifiers = playersManager.GetPlayerModifier(playerId).GetChild(0);
        playerScript.listOfModifiers = new List<Transform>(modifiers.childCount);
        for (int i = 0; i < modifiers.childCount; i++)
        {
            if (modifiers.GetChild(i).childCount > 0)
            {
                Transform currModifier = modifiers.GetChild(i).GetChild(0);
                playerScript.listOfModifiers.Add(currModifier);
            }
        }
        //playerScript.SetCarModifiers();
        for(int i = 0; i < listOfAllModifiers.Length; i++)
        {
            Transform currModifier = playerScript.listOfModifiers.Find(_modifier => _modifier.tag == listOfAllModifiers[i]);
            bool hasModifier = currModifier != null;
            InitModifierInPlayer(currModifier, listOfAllModifiers[i], hasModifier);
        }

    }

    void InitModifierInPlayer(Transform _modifier, string _modifierName, bool _active)
    {
        switch (_modifierName)
        {
            case "OilGun":
                player.GetComponent<PlayerOilGun>().Init(_modifier, _active);
                break;

            case "PaintGun":
                player.GetComponent<PlayerPaintGun>().Init(_modifier, _active);
                break;

            case "Plunger":
                player.GetComponent<PlayerThrowPlunger>().Init(_modifier, _active);
                break;

            case "AlaDelta":
                player.GetComponent<PlayerAlaDelta>().Init(_active);
                break;

            case "ChasisElevation":
                player.GetComponent<PlayerChasisElevation>().Init(_active);
                break;

            case "Umbrella":
                // ToDo: Fer
                break;

            case "Floater":
                player.GetComponent<PlayerFloater>().Init(_active);
                break;

            default:
                break;
        }
    }

}
