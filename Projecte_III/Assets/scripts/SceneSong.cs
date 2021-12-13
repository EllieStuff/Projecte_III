using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSong : MonoBehaviour
{
    private GameObject playerVehicle;

    // Start is called before the first frame update
    void Start()
    {
        playerVehicle = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerVehicle.GetComponent<PlayerVehicleScript>().editorModeActive)
        {
            if (this.GetComponent<AudioSource>().volume < 0.2f)
                this.GetComponent<AudioSource>().volume += 0.0001f;
        }
        else if(this.GetComponent<AudioSource>().volume > 0)
        {
            this.GetComponent<AudioSource>().volume -= 0.001f;
        }
    }
}
