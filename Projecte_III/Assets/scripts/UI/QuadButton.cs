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
        {
            quadSpot = GameObject.FindGameObjectWithTag("PlayerVehicle");
            currentQuad = quadSpot.transform.GetChild(0).gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if(currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        if (quadModel != null)
        {
            Instantiate(quadModel, quadSpot.transform);
            
            if(quadSpot.transform.childCount > 1)
            {
                if (quadSpot.transform.childCount > 2)
                    Destroy(quadSpot.transform.GetChild(1).gameObject);

                currentQuad.SetActive(false);
            }

            //SetNewValues();
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        if (quadModel != null && quadSpot.transform.childCount > 1)
        {
            Destroy(quadSpot.transform.GetChild(1).gameObject);
        }
        else if (quadModel != null && !placed)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }

        if (!currentQuad.activeSelf)
        {
            currentQuad.SetActive(true);
        }
        //SetNewValues();
    }

    public void SetQuad()
    {
        if (currentQuad == null)
            currentQuad = quadSpot.transform.GetChild(0).gameObject;

        if (currentQuad.name.Contains(quadModel.name)) return;

        if (quadSpot.transform.childCount > 0)
        {
            Destroy(quadSpot.transform.GetChild(0).gameObject);
        }

        Transform clone = Instantiate(quadModel, quadSpot.transform).transform;
        Destroy(quadSpot.transform.GetChild(1).gameObject);

        Debug.Log(clone.GetChild(clone.childCount - 1));

        GameObject.FindGameObjectWithTag("ModifierSpots").GetComponent<ModifierManager>().SetNewModifierSpots(clone.GetChild(clone.childCount - 1));

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>().SetStats();

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
