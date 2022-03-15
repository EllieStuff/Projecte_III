using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttonList;
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] internal int playerId;

    public void OpenButtons()
    {
        foreach (Button item in buttonList)
        {
            Vector3 localPos = item.transform.localPosition;
            if(localPos.x != -130)
            {
                localPos.x = -130;
                item.transform.localPosition = localPos;
            }
            item.GetComponent<Image>().color = new Color(1,1,1,1);
            item.GetComponent<ButtonScript>().SetListActive(false);
        }

        if(!backgrounds[1].activeSelf)
        {
            backgrounds[0].SetActive(false);
            backgrounds[1].SetActive(true);
        }
    }

    public void CloseButtons()
    {
        foreach (Button item in buttonList)
        {
            Vector3 localPos = item.transform.localPosition;
            if(localPos.x != 0)
            {
                localPos.x = 0;
                item.transform.localPosition = localPos;
            }
            item.GetComponent<Image>().color = new Color(1,1,1,1);
        }

        if (!backgrounds[0].activeSelf)
        {
            backgrounds[0].SetActive(true);
            backgrounds[1].SetActive(false);
        }
    }
}
