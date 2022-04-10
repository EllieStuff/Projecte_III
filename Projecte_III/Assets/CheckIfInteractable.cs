using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIfInteractable : MonoBehaviour
{
    /*ButtonManager manager;
    PlayerInputs playerInputs;
    bool bttnInited;

    // Start is called before the first frame update
    void Start()
    {
        manager = transform.parent.GetComponentInParent<ButtonManager>();
        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerInputs = playersManager.GetPlayer(manager.playerId).GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !bttnInited && playerInputs.Inited())
        {
            bttnInited = true;
            if (!playerInputs.UsesKeyboard())
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Button bttn = transform.GetChild(i).GetComponent<Button>();
                    bttn.interactable = false;
                    var colors = bttn.colors;
                    colors.disabledColor = Color.white;
                    bttn.colors = colors;
                }
            }
        }
    }*/
}
