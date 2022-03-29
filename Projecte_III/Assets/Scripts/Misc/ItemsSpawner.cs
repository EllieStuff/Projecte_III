using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Items;

    float timerSpawn = 10;

    // Update is called once per frame
    void Update()
    {
        timerSpawn -= Time.deltaTime;

        if(timerSpawn <= 0)
        {
            int random = Random.Range(0, Items.Length);
            timerSpawn = 10;
            GameObject Instance = Instantiate(Items[random], transform.position, transform.rotation);
        }
    }
}
