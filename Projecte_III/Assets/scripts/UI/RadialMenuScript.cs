using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuScript : MonoBehaviour
{
    [SerializeField] List<RadialMenuPieceScript> rmPiecesPrefabs;
    //int modifiersNum;

    Transform player;
    PlayerInputs playerInputs;
    RadialMenuPieceScript[] rmPieces;
    float degreesPerPiece;
    float gapDegrees = 3.0f;
    float distToIcon;
    float 
        selectedAlpha = 0.75f,
        nonSelectedAlpha = 0.5f;
    int activeElement, lastActiveElement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInputs = player.GetComponent<PlayerInputs>();

        //modifiersNum = rmPiecesPrefabs.Count;
        for(int i = 0; i < rmPiecesPrefabs.Count; i++)
        {
            if (rmPiecesPrefabs[i].tag == "Floater")
            {
                rmPiecesPrefabs.RemoveAt(i);
                //modifiersNum--;
            }
        }

        if (rmPiecesPrefabs.Count > 0)
        {
            degreesPerPiece = 360.0f / rmPiecesPrefabs.Count;
            distToIcon = Vector3.Distance(rmPiecesPrefabs[0].icon.transform.position, rmPiecesPrefabs[0].backGround.transform.position);
            transform.Rotate(0, 0, -gapDegrees);

            rmPieces = new RadialMenuPieceScript[rmPiecesPrefabs.Count];
            for (int i = 0; i < rmPieces.Length; i++)
            {
                rmPieces[i] = Instantiate(rmPiecesPrefabs[i], this.transform);
                //Vector3 posDiff = rmPieces[i].backGround.transform.localPosition - rmPieces[i].icon.transform.localPosition;
                if (rmPieces.Length > 1)
                {
                    rmPieces[i].backGround.fillAmount = (1.0f / rmPieces.Length) - (gapDegrees / 360.0f);
                    rmPieces[i].backGround.transform.localRotation = Quaternion.Euler(0, 0, degreesPerPiece / 2.0f + gapDegrees / 2.0f + i * degreesPerPiece);
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
            //Do action from each modifier
            switch (rmPieces[lastActiveElement].tag)
            {
                case "OilGun":
                    player.GetComponent<PlayerOilGun>().Activate();
                    break;

                case "PaintGun":
                    player.GetComponent<PlayerPaintGun>().Activate();
                    break;

                case "Plunger":
                    // ToDo: Adaptar amb els nous scripts
                    player.GetComponent<PlayerVehicleScript>().ActivatePlunger();
                    break;

                case "AlaDelta":
                    // ToDo: Adaptar amb els nous scripts
                    player.GetComponent<PlayerVehicleScript>().ActivateAlaDelta();
                    break;

                case "ChasisElevation":
                    // ToDo: Adaptar amb els nous scripts
                    player.GetComponent<PlayerVehicleScript>().ActivateChasis();
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

            rmPieces[lastActiveElement].ReinitColor();
            gameObject.SetActive(false);
        }
    }

}
