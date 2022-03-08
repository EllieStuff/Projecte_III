using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadialMenuManager : MonoBehaviour
{
    public enum RadialMenuState { DEFAULT, BUILDING, PLAYING };

    public GameObject buildingRadialMenu;
    [SerializeField] GameObject playingRadialMenu;
    [SerializeField] PlayingMainRadialMenu playingMainRM;
    [SerializeField] int playerId = 0;
    [SerializeField] RadialMenuState state = RadialMenuState.DEFAULT;

    PlayerInputs playerInputs;
    bool triggered = false;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        buildingRadialMenu.GetComponent<BuildingRadialMenu>().playerId = playerId;

        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    // Start is called before the first frame update
    void Start()
    {
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            if (scene.name == "Menu")
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            else if (scene.name == "Building Scene" || scene.name == "Building Scene Multiplayer")
            {
                state = RadialMenuState.BUILDING;
                buildingRadialMenu.SetActive(true);
                playingRadialMenu.SetActive(false);

                buildingRadialMenu.GetComponent<BuildingRadialMenu>().Init();
            }
            else
            {
                state = RadialMenuState.PLAYING;
                buildingRadialMenu.SetActive(false);
                playingRadialMenu.SetActive(true);

                playingMainRM.Init();
                playingRadialMenu.GetComponentInChildren<PlayingSecondaryRadialMenu>().Init();
            }
        }
        catch { }

    }

    // Update is called once per frame
    void Update()
    {
        if (state == RadialMenuState.PLAYING)
        {
            if (playerInputs.EnableGadgetMenu && !triggered)
            {
                triggered = true;
                playingMainRM.gameObject.SetActive(true);
            }
            else if (!playerInputs.EnableGadgetMenu && triggered)
            {
                triggered = false;
                playingMainRM.gameObject.SetActive(false);
            }

        }

    }



}
