using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript: MonoBehaviour
{
    [SerializeField] private ButtonManager manager;
    [SerializeField] private GameObject modifierSpots = null;
    [SerializeField] private GameObject list;
    private Button bttn;

    private void Start()
    {
        bttn = GetComponent<Button>();

        if (list.name == "Modifiers Objs" && modifierSpots == null)
            modifierSpots = GameObject.FindGameObjectWithTag("ModifierSpots").gameObject;

        if (modifierSpots != null && modifierSpots.activeSelf)
        {
            for (int i = 0; i < modifierSpots.transform.childCount; i++)
            {
                modifierSpots.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (list.name == "Modifiers Objs" && modifierSpots == null)
            modifierSpots = GameObject.FindGameObjectWithTag("ModifierSpots").gameObject;
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

                for (int i = 0; i < modifierSpots.transform.childCount; i++)
                {
                    GameObject child = modifierSpots.transform.GetChild(i).gameObject;
                    if (child.transform.childCount <= 0)
                    {
                        child.SetActive(active);
                    }
                }
            }
        }
    }
}
