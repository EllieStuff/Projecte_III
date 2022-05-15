using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSquirrels : MonoBehaviour
{
    [SerializeField] GameObject squirrelPrefab;
    [SerializeField] Transform spawnPointRef;
    [SerializeField] Utils.MinMaxFloat percentageOfNSquirrelsToSpawn;
    [SerializeField] float skipRate = 0.3f;
    [SerializeField] float stopRate = 0.1f;

    float distBetweenSquirrels = 0.8f;
    Utils.MinMaxFloat waitBetweenSpawn = new Utils.MinMaxFloat(0.2f, 1.0f);
    Utils.MinMaxVec3 squirrelScale = new Utils.MinMaxVec3(new Vector3(0.6f, 0.6f, 0.6f), new Vector3(1.4f, 1.4f, 1.4f));

    // Start is called before the first frame update
    void Start()
    {
        int actualNSquirrelsToSpawn = (int)percentageOfNSquirrelsToSpawn.GetRndValue();
        transform.Rotate(0f, Random.Range(0f, 360f), 0f);
        spawnPointRef.localPosition = transform.forward * distBetweenSquirrels;
        StartCoroutine(SpawnSquirrels(actualNSquirrelsToSpawn));
    }


    IEnumerator SpawnSquirrels(int _nSquirrelsToSpawn)
    {
        spawnPointRef.name = "squirrelSpawnRef";
        for (int i = 0; i < _nSquirrelsToSpawn; i++)
        {
            spawnPointRef.RotateAround(transform.position, transform.up, 360f / (float)_nSquirrelsToSpawn);
            if (Random.Range(0f, 1f) < skipRate)
                continue;

            yield return new WaitForSeconds(waitBetweenSpawn.GetRndValue());
            Quaternion squirrelRot = Quaternion.LookRotation(transform.position - spawnPointRef.position, spawnPointRef.up);
            GameObject instance = Instantiate(squirrelPrefab, spawnPointRef.position, squirrelRot, transform);
            instance.transform.localScale = squirrelScale.GetRndValue();

            if (Random.Range(0f, 1f) < stopRate) 
                break;
        }
        yield return new WaitForEndOfFrame();
    }
}
