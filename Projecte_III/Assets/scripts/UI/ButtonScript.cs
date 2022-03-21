using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript: MonoBehaviour
{
    [SerializeField] private ButtonManager manager;
    [SerializeField] private GameObject list;
    
    private ModifierManager modifierSpots = null;
    private Button bttn;
    private PlayerInputs playerInputs;

    private void Start()
    {
        bttn = GetComponent<Button>();

        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerInputs = playersManager.GetPlayer(manager.playerId).GetComponent<PlayerInputs>();
        if (list.name == "Modifiers Objs" && modifierSpots == null)
        {
            modifierSpots = playersManager.GetPlayerModifier(manager.playerId).GetComponent<ModifierManager>();
            modifierSpots.ShowTarget(false);
        }
    }

    public void ChangeList(int _idx)
    {
        if (playerInputs.UsesKeyboard())
        {
            if (!list.activeSelf)
            {
                manager.OpenButtons();
                manager.menuState = ButtonManager.MenuState.SUB;
                manager.mainIdx = _idx;
                manager.SelectButton(bttn);
                //bttn.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
            }
            else
            {
                manager.CloseButtons();
                manager.menuState = ButtonManager.MenuState.MAIN;
                bttn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

            SetListActive(!list.activeSelf);
        }
    }

    public void SetListActive(bool active)
    {
        if(list.activeSelf != active)
        {
            list.SetActive(active);
            if(modifierSpots != null)
            {
                modifierSpots.GetComponent<ModifierManager>().ShowTarget(active);
            }
        }
    }
}
