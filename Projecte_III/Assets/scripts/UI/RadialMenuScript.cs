using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuScript : MonoBehaviour
{
    [SerializeField] RadialMenuPieceScript rmPiecePrefab;
    [SerializeField] int modifiersNum = 3;
    [SerializeField] bool usesController = true;

    PlayerVehicleScript player;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>();
        playerInputs = player.GetComponent<PlayerInputs>();

        degreesPerPiece = 360.0f / modifiersNum;
        distToIcon = Vector3.Distance(rmPiecePrefab.icon.transform.position, rmPiecePrefab.backGround.transform.position);
        transform.Rotate(0, 0, -gapDegrees);
        
        rmPieces = new RadialMenuPieceScript[modifiersNum];
        for(int i = 0; i < rmPieces.Length; i++)
        {
            rmPieces[i] = Instantiate(rmPiecePrefab, this.transform);
            if (rmPieces.Length > 1)
            {
                rmPieces[i].backGround.fillAmount = (1.0f / modifiersNum) - (gapDegrees / 360.0f);
                rmPieces[i].backGround.transform.localRotation = Quaternion.Euler(0, 0, degreesPerPiece / 2.0f + gapDegrees / 2.0f + i * degreesPerPiece);
            }
            else
                rmPieces[i].backGround.fillAmount = 360.0f;

            Vector3 dirVector = Quaternion.AngleAxis(i * degreesPerPiece, Vector3.forward) * Vector3.up;
            Vector3 movVector = dirVector * distToIcon;
            rmPieces[i].icon.transform.localPosition = rmPieces[i].backGround.transform.localPosition + movVector;
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
        float finalAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, playerInputs.chooseItem, Vector3.forward) + degreesPerPiece / 2.0f);

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

    private void GetInput() // Treballar amb lastActiveElement
    {
        if (playerInputs.confirmGadget)
        {
            //Do action from each modifier

            rmPieces[lastActiveElement].ReinitColor();
            gameObject.SetActive(false);
        }
    }

}
