using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSquirrels : MonoBehaviour
{
    [SerializeField] GameObject squirrelPrefab;
    [SerializeField] Utils.MinMaxFloat percentageOfNSquirrelsToSpawn;

    float distBetweenSquirrels = 0.8f;
    Utils.MinMaxFloat waitBetweenSpawn = new Utils.MinMaxFloat(0.2f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        int actualNSquirrelsToSpawn = (int)percentageOfNSquirrelsToSpawn.GetRndValue();
        StartCoroutine(SpawnSquirrels(actualNSquirrelsToSpawn));
    }


    IEnumerator SpawnSquirrels(int _nSquirrelsToSpawn)
    {
        Transform spawnRef =
            Instantiate(new GameObject(), transform.position + transform.forward * distBetweenSquirrels, Quaternion.identity, transform).transform;
        for (int i = 0; i < _nSquirrelsToSpawn; i++)
        {
            yield return new WaitForSeconds(waitBetweenSpawn.GetRndValue());
            spawnRef.RotateAround(transform.position, transform.up, 360f / (float)_nSquirrelsToSpawn);
            Quaternion squirrelRot = Quaternion.LookRotation(transform.position - spawnRef.position, spawnRef.up);
            Instantiate(squirrelPrefab, spawnRef.position, squirrelRot, transform);
        }
        Destroy(spawnRef.gameObject);
    }
}
