using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRadialMenu : MonoBehaviour
{
    internal int playerId = 0;
    Transform player;
    PlayerInputs playerInputs;
    List<RadialMenuPieceScript> rmPiecesPrefabs;
    List<RadialMenuPieceScript> rmPieces = new List<RadialMenuPieceScript>();
    internal float degreesPerPiece = 0.0f;
    internal float gapDegrees = 3.0f;
    float distToIcon;
    internal float bgFillAmount;

    class ModifierSpotsData {
        public string modifierTag = "";
        public bool hadModifier = false;
        public ModifierSpotsData() { }
    }
    [SerializeField] Transform[] modifierSpots;
    ModifierSpotsData[] modifierSpotsData;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayer(playerId);
        playerInputs = player.GetComponent<PlayerInputs>();

        transform.Rotate(0, 0, -gapDegrees);
    }
    internal void Init()
    {
        InitModifierSpots();

        rmPiecesPrefabs = new List<RadialMenuPieceScript>();
        RefreshRadialMenu();
    }
    void InitModifierSpots()
    {
        //Transform modifiersFather = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayerModifier(playerId).GetChild(0);
        //modifierSpots = new Transform[modifiersFather.childCount];
        //modifierSpotsData = new ModifierSpotsData[modifierSpots.Length];
        //for(int i = 0; i < modifierSpots.Length; i++)
        //{
        //    modifierSpots[i] = modifiersFather.GetChild(i);
        //    modifierSpotsData[i] = new ModifierSpotsData();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (modifierSpots.Length == 0 || modifierSpots[0] == null) Init();

        int changedSpotId = CheckForModifierSpotsChanges();
        if(changedSpotId >= 0)
        {
            RefreshModifierSpotData(changedSpotId);
            RefreshRadialMenu();
        }

    }


    int CheckForModifierSpotsChanges()
    {
        for (int i = 0; i < modifierSpots.Length; i++)
        {
            if (modifierSpots[i].childCount > 0)
            {
                if (!modifierSpotsData[i].hadModifier
                    || modifierSpots[i].GetChild(0).tag != modifierSpotsData[i].modifierTag)
                    return i;
            }
            else
            {
                if (modifierSpotsData[i].hadModifier)
                    return i;
            }
        }

        return -1;
    }

    void RefreshModifierSpotData(int _modIdx)
    {
        ModifierSpotsData currModSpotData = modifierSpotsData[_modIdx];
        if (currModSpotData.hadModifier && !IsPassiveModifier(currModSpotData.modifierTag)) {

            int prevPieceIdx = rmPiecesPrefabs.FindIndex(_rmPieces => _rmPieces.tag == currModSpotData.modifierTag);
            if (prevPieceIdx >= 0) rmPiecesPrefabs.RemoveAt(prevPieceIdx);
            else Debug.LogError("Previous Modifier not found");
        }

        currModSpotData.hadModifier = modifierSpots[_modIdx].childCount > 0;
        if (currModSpotData.hadModifier)
            currModSpotData.modifierTag = modifierSpots[_modIdx].GetChild(0).tag;
        else
            currModSpotData.modifierTag = "";

        modifierSpotsData[_modIdx] = currModSpotData;


        if (currModSpotData.hadModifier)
        {
            if (IsPassiveModifier(currModSpotData.modifierTag))
                return;

            GameObject[] tmpPieces = Resources.LoadAll<GameObject>("Prefabs/UI/RadialMenuPieces/");
            foreach (GameObject piece in tmpPieces)
            {
                if (piece.tag == currModSpotData.modifierTag)
                {
                    rmPiecesPrefabs.Add(piece.GetComponent<RadialMenuPieceScript>());
                    break;
                }
            }
        }

    }

    void RefreshRadialMenu()
    {
        DestroyChilds();
        rmPieces = new List<RadialMenuPieceScript>();
        if (rmPiecesPrefabs.Count > 0)
        {
            degreesPerPiece = 360.0f / rmPiecesPrefabs.Count;
            distToIcon = Vector3.Distance(rmPiecesPrefabs[0].icon.transform.position, rmPiecesPrefabs[0].backGround.transform.position);


            bgFillAmount = (1.0f / rmPiecesPrefabs.Count) - (gapDegrees / 360.0f);
            for (int i = 0; i < rmPiecesPrefabs.Count; i++)
            {
                rmPieces.Add(Instantiate(rmPiecesPrefabs[i], this.transform));
                rmPieces[i].delayBackground.fillAmount = 0.0f;
                if (rmPiecesPrefabs.Count > 1)
                {
                    rmPieces[i].backGround.fillAmount = bgFillAmount;
                    rmPieces[i].backGround.transform.localRotation = Quaternion.Euler(0, 0, degreesPerPiece / 2.0f + gapDegrees / 2.0f + i * degreesPerPiece);
                    rmPieces[i].delayBackground.transform.localRotation = Quaternion.Euler(0, 0, degreesPerPiece / 2.0f + gapDegrees / 2.0f + i * degreesPerPiece);
                }
                else
                    rmPieces[i].backGround.fillAmount = 360.0f;


                //rmPieces[i].icon.transform.RotateAround(rmPieces[i].transform.position, Vector3.forward, degreesPerPiece / 2.0f + gapDegrees / 2.0f + i * degreesPerPiece + );
                Vector3 dirVector = Quaternion.AngleAxis(i * degreesPerPiece, Vector3.forward) * Vector3.up;
                Vector3 movVector = dirVector * distToIcon;
                rmPieces[i].icon.transform.localPosition = rmPieces[i].backGround.transform.localPosition + movVector;
                rmPieces[i].icon.transform.RotateAround(rmPieces[i].transform.position, Vector3.forward, rmPieces[i].iconRotDiff);
            }

        }
    }

    void DestroyChilds()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    bool IsPassiveModifier(string _modifierTag)
    {
        return _modifierTag == "Floater";
    }

}
