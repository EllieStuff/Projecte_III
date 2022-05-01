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
        foreach (BoxCollider spawnZone in spawnZones)
        {
            int actualNSpawnObj = (int)nSpawnObj.GetRndValue();
            Vector3 maxSpawnArea = Utils.Vectors.Multiply(spawnZone.transform.localScale, spawnZone.size);
            for (int i = 0; i < actualNSpawnObj; i++)
            {
                GameObject instance = Instantiate(spawnObj, spawnZone.transform);
                instance.transform.localPosition = Utils.Vectors.Randomize(Vector3.zero, maxSpawnArea);
            }
        }
    }

}
