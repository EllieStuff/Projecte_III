using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsListUI : MonoBehaviour
{
    Transform[] stats;

    // Start is called before the first frame update
    void Awake()
    {
        stats = new Transform[transform.childCount];
        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = transform.GetChild(i);
        }
    }

    public void UpdateStatsUI(Stats.Data values)
    {

        stats[0].GetComponentInChildren<TextMeshProUGUI>().text = values.weight.ToString();
        stats[1].GetComponentInChildren<TextMeshProUGUI>().text = values.acceleration.ToString();
        stats[2].GetComponentInChildren<TextMeshProUGUI>().text = values.friction.ToString();
        stats[3].GetComponentInChildren<TextMeshProUGUI>().text = values.torque.ToString();
        stats[4].GetComponentInChildren<TextMeshProUGUI>().text = values.maxVelocity.ToString();
    }
}
