using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDecPlants : MonoBehaviour
{
    [SerializeField] GameObject[] plantsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameObject plantToSpawn = plantsToSpawn[Random.Range(0, plantsToSpawn.Length)];
        Instantiate(plantToSpawn, transform);
    }

}
