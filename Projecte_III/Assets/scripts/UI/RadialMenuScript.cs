using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuScript : MonoBehaviour
{
    [SerializeField] RadialMenuPieceScript rmPiecePrefab;
    [SerializeField] int modifiersNum = 3;

    PlayerVehicleScript player;
    RadialMenuPieceScript[] rmPieces;
    float degreesPerPiece;
    float gapDegrees = 3.0f;
    float distToIcon;
    float 
        selectedAlpha = 0.75f,
        nonSelectedAlpha = 0.5f;
    Vector2 lastJoystickCoordinate = Vector2.zero;
    int activeElement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>();

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

            lastJoystickCoordinate = player.controls.Quad.j2Axis;
            activeElement = GetActiveElement();
        }

    }

    private int GetActiveElement()
    {
        //Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
        //Vector3 cursorVector = Input.mousePosition - screenCenter;

        //float mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, cursorVector, Vector3.forward) + degreesPerPiece / 2.0f);

        if (player.controls.Quad.j2Axis == Vector2.zero) return -1;
        float mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, player.controls.Quad.j2Axis, Vector3.forward) + degreesPerPiece / 2.0f);

        return (int)(mouseAngle / degreesPerPiece);
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
        // Adaptar inputs a mando
        if (Input.GetKeyDown(KeyCode.Mouse0) || (player.controls.Quad.j2Axis == Vector2.zero && lastJoystickCoordinate != Vector2.zero))
        {
            //Do action from each modifier

            gameObject.SetActive(false);
        }
    }

}
