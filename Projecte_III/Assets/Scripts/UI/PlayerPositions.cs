using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPositions : MonoBehaviour
{
    public Transform[] checkpoints;
    [SerializeField] private PlayersManager quads;
    float[] distances = new float[4];
    double[] sortedDistance = new double[4];

    private void Start()
    {
        checkpoints = new Transform[4];
        quads = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

        if (quads.numOfPlayers == 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            Destroy(this);
        }
        else if (quads.numOfPlayers == 2)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(2).SetSiblingIndex(1);
        }
        else if (quads.numOfPlayers == 3)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < quads.numOfPlayers; i++)
        {
            distances[i] = Vector3.Distance(quads.players[i].position, checkpoints[i].position) - (transform.GetChild(i).GetComponent<UIPosition>().checkpointNumber * 1000);
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
                    switch(o + 1)
                    {
                        case 1:
                            text.color = Color.green;
                            break;
                        case 2:
                            text.color = Color.cyan;
                            break;
                        case 3:
                            text.color = Color.yellow;
                            break;
                        case 4:
                            text.color = Color.red;
                            break;
                    }

                    text.text = (o + 1).ToString();
                }
            }

        }
    }
}
