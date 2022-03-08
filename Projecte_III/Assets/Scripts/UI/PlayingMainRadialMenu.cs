using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingMainRadialMenu : MonoBehaviour
{
    [SerializeField] RadialMenuManager manager;
    [SerializeField] BuildingRadialMenu menuToCopy;

    Transform player;
    PlayerInputs playerInputs;
    internal RadialMenuPieceScript[] rmPieces;
    float degreesPerPiece;
    float 
        selectedAlpha = 0.75f,
        nonSelectedAlpha = 0.5f;
    int activeElement, lastActiveElement;
    RadialMenuManager.PieceData lastSelectedGadged = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[menuToCopy.playerId].transform;
        playerInputs = player.GetComponent<PlayerInputs>();

        transform.Rotate(0, 0, -menuToCopy.gapDegrees);
    }
    internal void Init()
    {
        rmPieces = new RadialMenuPieceScript[menuToCopy.transform.childCount];
        for (int i = 0; i < rmPieces.Length; i++)
        {
            GameObject tmpGO = Instantiate(menuToCopy.transform.GetChild(i).gameObject, this.transform);
            rmPieces[i] = tmpGO.GetComponent<RadialMenuPieceScript>();
        }

        degreesPerPiece = menuToCopy.degreesPerPiece;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && rmPieces.Length > 0)
        {
            HighlightActiveElement(activeElement);
            GetInput();

            lastActiveElement = activeElement;
            activeElement = GetActiveElement();
        }

    }

    private int GetActiveElement()
    {
        float finalAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, playerInputs.ChooseItem, Vector3.forward) + degreesPerPiece / 2.0f);

        return (int)(finalAngle / degreesPerPiece);
    }

    private float NormalizeAngle(float _angle) => (_angle + 360.0f) % 360.0f;

    private void HighlightActiveElement(int _activeElement)
    {
        Color newColor = rmPieces[0].backGround.color;
        for(int i = 0; i < rmPieces.Length; i++)
        {
            if(i == _activeElement) 
                newColor.a = selectedAlpha;
            else 
                newColor.a = nonSelectedAlpha;

            rmPieces[i].LerpBgColor(newColor);
        }

    }

    private void GetInput()
    {
        if (playerInputs.ConfirmGadget)
        {
            // Update Selected Gadget
            RadialMenuManager.PieceData newSelectedGadged = new RadialMenuManager.PieceData(lastActiveElement, rmPieces[lastActiveElement].delayTime, rmPieces[lastActiveElement].tag);
            manager.SetSelectedGadget(newSelectedGadged);

            //Disable Menu
            rmPieces[lastActiveElement].ReinitColor();
            gameObject.SetActive(false);
        }
    }


    //internal void ManageModifiersDelays()
    //{
    //    foreach (string key in piecesData.Keys)
    //    {
    //        PieceData currPieceData = piecesData[key];
    //        if (currPieceData.countdownActive)
    //        {
    //            Debug.Log(key + " has " + Mathf.Lerp(0.0f, menuToCopy.bgFillAmount, currPieceData.delayTimer / menuToCopy.bgFillAmount));
    //            rmPieces[currPieceData.id].delayBackground.fillAmount = Mathf.Lerp(0.0f, menuToCopy.bgFillAmount, currPieceData.delayTimer / menuToCopy.bgFillAmount);
    //            if (currPieceData.delayTimer > 0)
    //                piecesData[key].delayTimer -= Time.deltaTime;
    //            else
    //                piecesData[key].countdownActive = false;
    //        }


    //    }
    //}

}
