using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField]private Stats stats;

    private GameObject wheelsSpot, quadSpot, modifierSpot;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.AddComponent<Stats>();
        wheelsSpot = GameObject.FindGameObjectWithTag("Wheels");
        quadSpot = GameObject.FindGameObjectWithTag("PlayerVehicle");
        modifierSpot = GameObject.FindGameObjectWithTag("ModifierSpots");

        SetStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats()
    {
        stats.ResetStats();

        //Wheels stats
        stats.SetStats(stats + wheelsSpot.GetComponentInChildren<Stats>());

        //Quad stats
        stats.SetStats(stats + quadSpot.GetComponentInChildren<Stats>());

        //Modifier Stats
        //Transform modfs = GameObject.FindGameObjectWithTag("ModifierSpots").transform;
        Transform modfs = modifierSpot.transform.GetChild(0);

        for (int i = 0; i < modfs.childCount; i++)
        {
            if (modfs.GetChild(i).childCount > 0)
                stats.SetStats(stats + modfs.GetChild(i).GetComponentInChildren<Stats>());
        }

        GameObject.FindGameObjectWithTag("StatsManager").GetComponentInChildren<StatsListUI>().UpdateStatsUI(stats.GetStats());
    }

    public void HideVoidModifier()
    {
        //Transform modfs = GameObject.FindGameObjectWithTag("ModifierSpots").transform;
        Transform modfs = modifierSpot.transform.GetChild(0);

        for (int i = 0; i < modfs.childCount; i++)
        {
            Transform child = modfs.GetChild(i);

            if (child.transform.childCount <= 0)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
