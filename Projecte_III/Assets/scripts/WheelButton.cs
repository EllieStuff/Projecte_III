using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject wheelsModel;
    [SerializeField] private GameObject wheelSpot = null;
    [SerializeField] private GameObject currentWheel;

    private bool placed = false;

    private void Update()
    {
        if (wheelSpot == null)
            wheelSpot = GameObject.FindGameObjectWithTag("VehicleSet").transform.GetChild(1).gameObject;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (wheelsModel != null)
        {
            Instantiate(wheelsModel, wheelSpot.transform);

            if (wheelSpot.transform.GetChild(0).childCount > 1 && (currentWheel == null || currentWheel.name != wheelsModel.name))
            {
                currentWheel = wheelSpot.transform.GetChild(0).gameObject;

                currentWheel.SetActive(false);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (wheelsModel != null && wheelSpot.transform.childCount > 1)
        {
            Destroy(wheelSpot.transform.GetChild(1).gameObject);
        }
        else if (wheelsModel != null && !placed)
        {
            Destroy(wheelSpot.transform.GetChild(0).gameObject);
        }

        if (currentWheel != null && !currentWheel.activeSelf)
        {
            currentWheel.SetActive(true);
        }
    }

    public void SetWheels()
    {
        if(wheelSpot.transform.childCount > 0 && wheelSpot.transform.GetChild(0).name != wheelsModel.name)
        {
            Destroy(wheelSpot.transform.GetChild(0).gameObject);
        }

        Instantiate(wheelsModel, wheelSpot.transform);
        placed = true;
    }
}
