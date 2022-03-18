using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPositions : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private PlayersManager quads;
    float[] distances = new float[4];
    double[] sortedDistance = new double[4];

    private void Start()
    {
        quads = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

        if (quads.numOfPlayers == 1)
        {
            transform.GetChild(0).gameObject.active = false;
            transform.GetChild(1).gameObject.active = false;
            transform.GetChild(2).gameObject.active = false;
            transform.GetChild(3).gameObject.active = false;
            Destroy(this);
        }
        else if (quads.numOfPlayers == 2)
        {
            transform.GetChild(1).gameObject.active = false;
            transform.GetChild(3).gameObject.active = false;
            transform.GetChild(2).SetSiblingIndex(1);
        }
        else if (quads.numOfPlayers == 3)
        {
            transform.GetChild(3).gameObject.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < quads.numOfPlayers; i++)
        {
            distances[i] = Vector3.Distance(quads.players[i].position, goal.position);
            sortedDistance[i] = (double)distances[i];
        }

        Array.Sort(sortedDistance);

        for (int i = 0; i < quads.numOfPlayers; i++)
        {
            TextMeshProUGUI text = transform.GetChild(i).GetComponent<TextMeshProUGUI>();

            for (int o = 0; o < quads.numOfPlayers; o++)
            {
                if (distances[i] == sortedDistance[o])
                {
                    text.text = (o + 1).ToString();
                }
            }

        }
    }
}
