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

    private void Start()
    {
        bttn = GetComponent<Button>();

        if (list.name == "Modifiers Objs" && modifierSpots == null)
        {
            modifierSpots = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayerModifier(manager.playerId).GetComponent<ModifierManager>();
            modifierSpots.ShowTarget(false);
        }
    }

    public void ChangeList()
    {
        if (!list.activeSelf)
        {
            manager.OpenButtons();
            bttn.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        }
        else
        {
            manager.CloseButtons();
            bttn.GetComponent<Image>().color = new Color(1,1,1,1);
        }

        SetListActive(!list.activeSelf);
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
