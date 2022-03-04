using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript: MonoBehaviour
{
    [SerializeField] private ButtonManager manager;
    [SerializeField] private Transform modifierSpots = null;
    [SerializeField] private GameObject list;
    private Button bttn;

    private void Start()
    {
        bttn = GetComponent<Button>();
        Transform modfs = null;
        if (list.name == "Modifiers Objs" && modifierSpots == null)
        {
            modifierSpots = GameObject.FindGameObjectWithTag("ModifierSpots").transform;
            modfs = modifierSpots.GetChild(0);
        }

        if (modifierSpots != null && modfs.gameObject.activeSelf)
        {
            for (int i = 0; i < modfs.childCount; i++)
            {
                modfs.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (list.name == "Modifiers Objs" && modifierSpots == null)
            modifierSpots = GameObject.FindGameObjectWithTag("ModifierSpots").transform;
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
                Transform target = GameObject.Find("Target").transform;
                if (!active && target.childCount > 0)
                    Destroy(target.GetChild(0).gameObject);

                Transform modfs = modifierSpots.GetChild(0);

                for (int i = 0; i < modfs.childCount; i++)
                {
                    Transform child = modfs.GetChild(i);
                    if (child.childCount <= 0)
                    {
                        child.gameObject.SetActive(active);
                    }
                }
            }
        }
    }
}
