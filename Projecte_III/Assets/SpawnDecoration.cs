using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecoration : MonoBehaviour
{
    [SerializeField] GameObject spawnObj;
    [SerializeField] BoxCollider[] spawnZones;
    [SerializeField] Utils.MinMaxFloat nSpawnObj;

    // Start is called before the first frame update
    void Start()
    {
        int actualNSpawnObj = (int)nSpawnObj.GetRndValue();
        foreach (BoxCollider spawnZone in spawnZones)
        {
            Vector3 maxSpawnArea = Utils.Vectors.Multiply(spawnZone.transform.localScale, spawnZone.size);
            for (int i = 0; i < actualNSpawnObj; i++)
            {
                Vector3 spawnPos = spawnZone.transform.position +  Utils.Vectors.Randomize(Vector3.zero, maxSpawnArea);
                Instantiate(spawnObj, spawnPos, spawnObj.transform.rotation, spawnZone.transform);
            }
        }
    }

}
