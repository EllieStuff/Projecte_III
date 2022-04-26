using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelectionScript : MonoBehaviour
{
    //Escollir Obstacle - Mirar forma carretera - retornar tipus al fill
    [SerializeField] GameObject[] ObstacleSelection;
    GameObject[] ObjectSelected;
    int roadType;
    int numOfObstacleTypes = 1;

    //Check randomPercent on "ObstacleData" to set # of obstacles of the same prefab
    [HideInInspector] public int randomPercent;

    // Start is called before the first frame update
    void Start()
    {
        randomPercent = Random.Range(0, 100);
        //First minute
        if (Time.timeSinceLevelLoad <= 60)
        {
            //Easy - 1 typeOfObstacle, 1 instance
            if (randomPercent < 50)
                numOfObstacleTypes = 1;
            //Medium - 1 typeOfObstacle, 2 instances
            else if (randomPercent < 80)
                numOfObstacleTypes = 1;
            //Hard - 2 typeOfObstacle, 1 instance
            else if (randomPercent < 100)
                numOfObstacleTypes = 2;
        }
        //Second minute
        else if(Time.timeSinceLevelLoad <= 120)
        {
            //Easy - 1 typeOfObstacle, 2 instance
            if (randomPercent < 50)
                numOfObstacleTypes = 1;
            //Medium - 3 typeOfObstacle, 1 instances
            else if (randomPercent < 80)
                numOfObstacleTypes = 1;
            //Hard - 2 typeOfObstacle, 2 instance
            else if (randomPercent < 100)
                numOfObstacleTypes = 3;
        }
        //Third minute and onwards
        else /*if (Time.timeSinceLevelLoad <= 180)*/
        {
            //Easy - 3 typeOfObstacle, 1 instance
            if (randomPercent < 50)
                numOfObstacleTypes = 3;
            //Medium - 2 typeOfObstacle, 2 instances
            else if (randomPercent < 80)
                numOfObstacleTypes = 2;
            //Hard - 4 typeOfObstacle, 1 instance
            else if (randomPercent < 100)
                numOfObstacleTypes = 4;
        }

        ObjectSelected = new GameObject[numOfObstacleTypes];

        List<int> pickedObstacles = new List<int>();

        for (int i = 1; i <= numOfObstacleTypes; i++)
        {
            int _random = Random.Range(0, ObstacleSelection.Length);

            if (!pickedObstacles.Contains(_random))
            {
                ObjectSelected[i-1] = Instantiate(ObstacleSelection[_random], transform);

                roadType = (int)GetComponentInParent<RoadData>().RoadType;

                ObjectSelected[i-1].GetComponent<ObstacleData>().roadType = roadType;

                pickedObstacles.Add(_random);
            }
            else
                i--;            
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time Since scene started: " + Time.timeSinceLevelLoad);
    }
}
