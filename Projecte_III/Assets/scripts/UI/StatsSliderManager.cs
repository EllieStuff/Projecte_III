using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSliderManager : MonoBehaviour
{
    StatsSlider[] stats;

    Stats.Data statsValue;

    private bool first = false;

    // Start is called before the first frame update
    void Start()
    {
        stats = new StatsSlider[transform.GetChild(0).childCount];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = transform.GetChild(0).GetChild(i).GetComponent<StatsSlider>();
        }
    }

    public void SetSliderValue(Stats.Data stats, bool placed = false)
    {
        SetSliderValue(stats.acceleration, "Acceleration", placed);
        SetSliderValue(stats.friction, "Friction", placed);
        SetSliderValue(stats.maxVelocity, "MaxVelocity", placed);
        SetSliderValue(stats.torque, "Torque", placed);
        SetSliderValue(stats.weight, "Weight", placed);

        if (placed) statsValue = stats;
    }

    public void SetSliderValue(float value, string statType, bool placed = false)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if(stats[i].gameObject.name == statType)
            {
                stats[i].SetSliderValue(value, placed);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!first)
        {
            GetPlayerStats();
            SetSliderValue(statsValue, true);

            first = true;
        }
    }

    public void GetPlayerStats()
    {
        statsValue = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>().GetStats();
    }
}
