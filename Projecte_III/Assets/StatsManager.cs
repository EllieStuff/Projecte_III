using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] StatsSliderManager[] statsSliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public StatsSliderManager GetPlayerStats(int _playerId)
    {
        return statsSliders[_playerId];
    }

}
