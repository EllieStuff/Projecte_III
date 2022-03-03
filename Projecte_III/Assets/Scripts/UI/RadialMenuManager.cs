using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuManager : MonoBehaviour
{
    [SerializeField] GameObject radialMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Joystick1Button4))
            radialMenu.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.Joystick1Button4))
            radialMenu.SetActive(false);

    }



}
