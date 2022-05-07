using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoSpawnerScript : MonoBehaviour
{
    public GameObject[] objectsArray;
    int randomNumber;
    float timePassedSinceSpawn;
    public float timeLimit;
    GameObject ObjectInstantiated;
    public float ObjectScale;

    Vector3 butterflySpawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        timePassedSinceSpawn = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassedSinceSpawn >= timeLimit)
        {
            randomNumber = Random.Range(0, objectsArray.Length);
            if(gameObject.name.Contains("Butterflies"))
            {
                butterflySpawnOffset = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-2.0f, 5.0f), Random.Range(-10.0f, 10.0f));
                ObjectInstantiated = Instantiate(objectsArray[randomNumber], transform.position + butterflySpawnOffset, transform.rotation);
            }
            else
                ObjectInstantiated = Instantiate(objectsArray[randomNumber], transform.position, transform.rotation);
            ObjectInstantiated.transform.localScale = new Vector3(ObjectScale, ObjectScale, ObjectScale);
            ObjectInstantiated.AddComponent<ObjectDisplacementScript>();

            timePassedSinceSpawn = 0;
        }
        else
            timePassedSinceSpawn += Time.deltaTime;
    }
}
