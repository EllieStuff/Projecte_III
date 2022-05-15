using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInputs : MonoBehaviour
{
    string inputPath = "PlayerInputData";
    int maxNumOfPlayers = 4;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxNumOfPlayers; i++)
        {
            PlayerPrefs.SetInt(inputPath + i.ToString(), -1);
        }
    }

}
