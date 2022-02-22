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
        stats = new StatsSlider[transform.childCount];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = transform.GetChild(i).GetComponent<StatsSlider>();
        }
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
            statsValue = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Stats>().GetStats();


            first = true;
        }


    }
}
