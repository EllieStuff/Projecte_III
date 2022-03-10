using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadialMenuManager : MonoBehaviour
{
    public enum RadialMenuState { DEFAULT, BUILDING, PLAYING };

    [SerializeField] GameObject buildingRadialMenu;
    [SerializeField] GameObject playingRadialMenu;
    [SerializeField] BuildingRadialMenu buildingRM_Script;
    [SerializeField] PlayingMainRadialMenu playingRM1_Script;
    [SerializeField] PlayingSecondaryRadialMenu playingRM2_Script;
    [SerializeField] int playerId = 0;
    [SerializeField] RadialMenuState state = RadialMenuState.DEFAULT;

    PlayerVehicleScript player;
    PlayerInputs playerInputs;
    bool triggered = false;
    internal class PieceData
    {
        public int id; public string tag; public float maxDelayTime, delayTimer = 0; public bool countdownActive = false;
        public PieceData(int _id, float _maxDelayTime, string _tag = "") { id = _id; maxDelayTime = _maxDelayTime; tag = _tag; }
    }
    internal Dictionary<string, PieceData> piecesData;

    internal PieceData selectedGadget = null;


    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        buildingRM_Script.playerId = playerId;

        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[playerId].GetComponent<PlayerVehicleScript>();
        playerInputs = player.GetComponent<PlayerInputs>();
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

                buildingRM_Script.Init();
            }
            else
            {
                state = RadialMenuState.PLAYING;
                buildingRadialMenu.SetActive(false);
                playingRadialMenu.SetActive(true);

                InitPiecesData();
                playingRM1_Script.Init();
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
                playingRM1_Script.gameObject.SetActive(true);
            }
            else if (!playerInputs.EnableGadgetMenu && triggered)
            {
                triggered = false;
                playingRM1_Script.gameObject.SetActive(false);
            }

            if (playerInputs.UseGadget && selectedGadget != null && !piecesData[selectedGadget.tag].countdownActive)
            {
                UseGadget();
            }


            ManageModifiersDelays();
        }

    }


    internal void SetSelectedGadget(PieceData _selectedGadget)
    {
        if (selectedGadget != null)
        {
            playingRM1_Script.rmPieces[selectedGadget.id].outline.enabled = false;
            playingRM2_Script.rmPieces[selectedGadget.id].outline.enabled = false;
        }

        selectedGadget = _selectedGadget;
        playingRM1_Script.rmPieces[selectedGadget.id].outline.enabled = true;
        playingRM2_Script.rmPieces[selectedGadget.id].outline.enabled = true;
    }

    private void UseGadget()
    {
        //Do action from each modifier
        switch (selectedGadget.tag)
        {
            case "OilGun":
                player.GetComponent<PlayerOilGun>().Activate();
                break;

            case "PaintGun":
                player.GetComponent<PlayerPaintGun>().Activate();
                break;

            case "Plunger":
                // ToDo: Adaptar amb els nous scripts
                player.GetComponent<PlayerThrowPlunger>().Activate();
                break;

            case "AlaDelta":
                // ToDo: Adaptar amb els nous scripts
                player.GetComponent<PlayerAlaDelta>().Activate();
                break;

            case "ChasisElevation":
                // ToDo: Adaptar amb els nous scripts
                player.GetComponent<PlayerChasisElevation>().Activate();
                break;

            case "Umbrella":
                // ToDo: Fer
                break;

            ///Prolly should make an exception for this, since it's automatic
            //case "Floater":
            //    break;

            default:
                break;
        }
        // Reinit Modifier CountDown
        piecesData[selectedGadget.tag].delayTimer = piecesData[selectedGadget.tag].maxDelayTime;
        piecesData[selectedGadget.tag].countdownActive = true;
    }

    private void InitPiecesData()
    {
        piecesData = new Dictionary<string, PieceData>();
        RadialMenuPieceScript[] tmpPieces = buildingRadialMenu.GetComponentsInChildren<RadialMenuPieceScript>();
        for (int i = 0; i < tmpPieces.Length; i++)
        {
            piecesData.Add(tmpPieces[i].tag, new PieceData(i, tmpPieces[i].delayTime));
        }
    }
    private void ManageModifiersDelays()
    {
        foreach (string key in piecesData.Keys)
        {
            PieceData currPieceData = piecesData[key];
            if (currPieceData.countdownActive)
            {
                float newFillAmount = Mathf.Lerp(0.0f, buildingRM_Script.bgFillAmount, currPieceData.delayTimer / currPieceData.maxDelayTime);
                //Debug.Log(key + " has " + newFillAmount);
                playingRM1_Script.rmPieces[currPieceData.id].delayBackground.fillAmount = newFillAmount;
                playingRM2_Script.rmPieces[currPieceData.id].delayBackground.fillAmount = newFillAmount;

                if (currPieceData.delayTimer > 0)
                    piecesData[key].delayTimer -= Time.deltaTime;
                else
                    piecesData[key].countdownActive = false;
            }


        }
    }

}
