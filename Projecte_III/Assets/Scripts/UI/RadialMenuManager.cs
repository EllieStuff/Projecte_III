using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuManager : MonoBehaviour
{
    [SerializeField] GameObject radialMenu;

    PlayerInputs playerInputs;
    bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputs.EnableGadgetMenu && !triggered)
        {
            triggered = true;
            radialMenu.SetActive(true);
        }
        else if (!playerInputs.EnableGadgetMenu && triggered)
        {
            triggered = false;
            radialMenu.SetActive(false);
        }

    }



}
