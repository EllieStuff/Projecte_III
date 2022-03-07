using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSecondaryRadialMenu : MonoBehaviour
{
    [SerializeField] BuildingRadialMenu menuToCopy;

    GameObject[] rmPieces;

    // Start is called before the first frame update
    private void Start()
    {
        transform.Rotate(0, 0, -menuToCopy.gapDegrees);
    }
    internal void Init()
    {
        rmPieces = new GameObject[menuToCopy.transform.childCount];
        for (int i = 0; i < rmPieces.Length; i++)
        {
            rmPieces[i] = Instantiate(menuToCopy.transform.GetChild(i).gameObject, this.transform);
        }

    }

}
