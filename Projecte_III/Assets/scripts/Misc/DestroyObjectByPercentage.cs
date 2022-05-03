using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectByPercentage : MonoBehaviour
{
    [SerializeField] int percentageOfSurvive;

    void Start()
    {
        int random = Random.Range(0, 100);

        if (random >= percentageOfSurvive)
            Destroy(gameObject);
    }
}
