using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    Transform[] possiblePositions;
    public GameObject[] obstacleArray;
    GameObject[] obstacleToSpawn;
    //MeshRenderer mesh;
    //MeshCollider col;
    int _randomObject;
    int numOfInstances = 1;

    public int roadType;
    public float carWeight = 1700.0f;

    // Start is called before the first frame update
    void Start()
    {
        ObstacleSelectionScript parentObstacleGeneration = GetComponentInParent<ObstacleSelectionScript>();
        if (Time.timeSinceLevelLoad <= 60)
        {
            //Easy - 1 typeOfObstacle, 1 instance
            if (parentObstacleGeneration.randomPercent < 50)
                numOfInstances = 1;
            //Medium - 1 typeOfObstacle, 2 instances
            else if (parentObstacleGeneration.randomPercent < 80)
                numOfInstances = 2;
            //Hard - 2 typeOfObstacle, 1 instance
            else if (parentObstacleGeneration.randomPercent < 100)
                numOfInstances = 1;
        }
        //Second minute
        else if (Time.timeSinceLevelLoad <= 120)
        {
            //Easy - 1 typeOfObstacle, 2 instance
            if (parentObstacleGeneration.randomPercent < 50)
                numOfInstances = 2;
            //Medium - 3 typeOfObstacle, 1 instances
            else if (parentObstacleGeneration.randomPercent < 80)
                numOfInstances = 1;
            //Hard - 2 typeOfObstacle, 2 instance
            else if (parentObstacleGeneration.randomPercent < 100)
                numOfInstances = 2;
        }
        //Third minute and onwards
        else /*if (Time.timeSinceLevelLoad <= 180)*/
        {
            //Easy - 3 typeOfObstacle, 1 instance
            if (parentObstacleGeneration.randomPercent < 50)
                numOfInstances = 1;
            //Medium - 2 typeOfObstacle, 2 instances
            else if (parentObstacleGeneration.randomPercent < 80)
                numOfInstances = 2;
            //Hard - 4 typeOfObstacle, 1 instance
            else if (parentObstacleGeneration.randomPercent < 100)
                numOfInstances = 1;
        }

        obstacleToSpawn = new GameObject[numOfInstances];

        for (int i = 0; i < numOfInstances; i++)
        {
            _randomObject = Random.Range(0, obstacleArray.Length);
            obstacleToSpawn[i] = Instantiate(obstacleArray[_randomObject], transform);
        }

        possiblePositions = new Transform[transform.GetChild(roadType).childCount];
        for (int i = 0; i < possiblePositions.Length; i++)
        {
            possiblePositions[i] = transform.GetChild(roadType).GetChild(i);
        }

        SpawnObstacle();
    }

    void Update()
    {
        //Debug.Log("Random Percent: " + GetComponentInParent<ObstacleSelectionScript>().randomPercent);
    }

    public void SpawnObstacle()
    {
        List<int> pickedPositions = new List<int>();
        
        for(int i = 0; i < obstacleToSpawn.Length; i++)
        {
            int _random = Random.Range(0, possiblePositions.Length);

            if (!pickedPositions.Contains(_random))
            {
                Transform _spawnedPosition = possiblePositions[_random];

                obstacleToSpawn[i].transform.position = _spawnedPosition.position;
                if(this.name.Contains("Bird"))
                    obstacleToSpawn[i].GetComponent<ShitterBirdScript>().InitValues(_spawnedPosition.forward, 5.0f);
                else
                    obstacleToSpawn[i].transform.rotation = _spawnedPosition.rotation;

                if (this.name.Contains("Car"))
                {
                    Rigidbody spawnRB = obstacleToSpawn[i].AddComponent<Rigidbody>();
                    spawnRB.mass = carWeight;
                }
                pickedPositions.Add(_random);
            }
            else
                i--;

        }
    }
}
