using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSecondaryRadialMenu : MonoBehaviour
{
    [SerializeField] BuildingRadialMenu menuToCopy;

    internal RadialMenuPieceScript[] rmPieces;

    // Start is called before the first frame update
    private void Start()
    {
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

    }

}
