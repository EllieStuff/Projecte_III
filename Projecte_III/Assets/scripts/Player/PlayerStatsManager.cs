using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private GameObject statsManager;
    bool first = false;
    PlayersManager playersManager;
    int playerId;

    private GameObject wheelsSpot, quadSpot;

    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.AddComponent<Stats>();
        playerId = GetComponentInParent<QuadSceneManager>().playerId;
        playersManager = transform.parent.GetComponentInParent<PlayersManager>();
        wheelsSpot = transform.parent.Find("Wheels Models").gameObject;
        quadSpot = transform.Find("vehicleChasis").gameObject;
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
        Transform modfs = playersManager.GetPlayerModifier(playerId).GetChild(0);
        //Transform modfs = GameObject.FindGameObjectWithTag("ModifierSpots").transform.GetChild(0);

        for (int i = 0; i < modfs.childCount; i++)
        {
            if (modfs.GetChild(i).childCount > 0)
            {
                stats.SetStats(stats + modfs.GetChild(i).GetComponentInChildren<Stats>());
            }
        }

        statsManager.GetComponent<StatsManager>().GetPlayerStats(playerId).SetSliderValue(stats.GetStats(), true);
    }

    private void Update()
    {
        if (!first)
        {
            SetStats();
            first = true;
        }
    }

    public void HideVoidModifier()
    {
        Transform modfs = playersManager.GetPlayerModifier(playerId).GetChild(0);

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
