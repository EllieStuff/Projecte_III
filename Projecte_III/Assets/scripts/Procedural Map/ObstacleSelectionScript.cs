using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelectionScript : MonoBehaviour
{
    //Escollir Obstacle - Mirar forma carretera - retornar tipus al fill
    [SerializeField] GameObject[] ObstacleSelection;
    GameObject ObjectSelected;
    int roadType;

    // Start is called before the first frame update
    void Start()
    {
        //basic random for now, may change it later to manage game difficulties
        int _random = Random.Range(0, ObstacleSelection.Length);

        ObjectSelected = Instantiate(ObstacleSelection[_random], transform);

        roadType = (int)GetComponentInParent<RoadData>().RoadType;

        ObjectSelected.GetComponent<ObstacleData>().roadType = roadType;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Road Type: " + roadType);
    }
}
