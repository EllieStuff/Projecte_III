using UnityEngine;
using UnityEngine.EventSystems;

public class QuadButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject quadModel;
    [SerializeField] private GameObject quadSpot;

    Stats.Data sliderData;

    [SerializeField] private GameObject currentQuad;
    private bool placed = false;

    private void Update()
    {
        sliderData = quadModel.GetComponent<Stats>().GetStats();

        if (quadSpot == null)
            quadSpot = GameObject.FindGameObjectWithTag("vehicleElement").transform.GetChild(0).gameObject;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(quadModel != null)
        {
            Instantiate(quadModel, quadSpot.transform);
            
            if(quadSpot.transform.childCount > 1 && (currentQuad == null || currentQuad.name != quadModel.name))
            {
                if (quadSpot.transform.childCount > 2)
                    Destroy(quadSpot.transform.GetChild(1).gameObject);
                    
                currentQuad = quadSpot.transform.GetChild(0).gameObject;

                currentQuad.SetActive(false);
            }

            //SetNewValues();
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

        //SetNewValues();
    }

    public void SetQuad()
    {
        if (quadSpot.transform.childCount > 0 && quadSpot.transform.GetChild(0).name != quadModel.name)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }
        //SetNewValues();

        Instantiate(quadModel, quadSpot.transform);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerVehicleScript>().SetStats();

        placed = true;
    }

    private void SetNewValues()
    {
        StatsSliderManager stats = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsSliderManager>();

        stats.SetSliderValue(sliderData.acceleration, "Acceleration");
        stats.SetSliderValue(sliderData.friction, "Friction");
        stats.SetSliderValue(sliderData.maxVelocity, "MaxVelocity");
        stats.SetSliderValue(sliderData.torque, "Torque");
        stats.SetSliderValue(sliderData.weight, "Weight");
    }
}
