using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuadButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject quadModel;
    [SerializeField] private GameObject quadSpot;

    [SerializeField] private GameObject currentQuad;
    private bool placed = false;

    private void Start()
    {
        quadSpot = QuadSceneManager.Instance.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(quadModel != null)
        {
            Instantiate(quadModel, quadSpot.transform);
            
            if(quadSpot.transform.childCount > 1 && (currentQuad == null || currentQuad.name != quadModel.name))
            {
                currentQuad = quadSpot.transform.GetChild(0).gameObject;

                currentQuad.SetActive(false);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if(quadModel != null && quadSpot.transform.childCount > 1)
        {
            Destroy(quadSpot.transform.GetChild(1).gameObject);
        }
        else if(quadModel != null && !placed)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }

        if(currentQuad != null && !currentQuad.activeSelf)
        {
            currentQuad.SetActive(true);
        }
    }

    public void SetQuad()
    {
        if (quadSpot.transform.childCount > 0 && quadSpot.transform.GetChild(0).name != quadModel.name)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }
        Instantiate(quadModel, quadSpot.transform);
        placed = true;
    }
}
