using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMultiplayerScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick2Button7))
            LoadSceneManager.LoadScene("Building Scene Multiplayer");
    }
}
