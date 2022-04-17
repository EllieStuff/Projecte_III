using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWithChildsData : MonoBehaviour
{
    //"Pos" child objects must be the first ones under the parent object
    public int numOfPositions;
    Transform[] possiblePositions;
    [SerializeField] GameObject[] childObjects;
    MeshRenderer mesh;
    MeshCollider col;

    int countOfChildsWithoutPP;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<MeshCollider>();

        countOfChildsWithoutPP = transform.childCount - numOfPositions;

        possiblePositions = new Transform[numOfPositions];
        for (int i = 0; i < numOfPositions; i++)
        {
            possiblePositions[i] = transform.GetChild(i);
            MeshRenderer _mesh = possiblePositions[i].GetComponent<MeshRenderer>();
            if (_mesh != null)
                _mesh.enabled = false;
        }

        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        int _random = Random.Range(0, possiblePositions.Length);

        Transform _spawnedPosition = possiblePositions[_random];

        transform.position = _spawnedPosition.position;
        transform.rotation = _spawnedPosition.rotation;
        transform.localScale = _spawnedPosition.localScale;

        if (mesh != null) mesh.enabled = true;

        if (col != null) col.enabled = true;

        //No va idk why
        for (int i = numOfPositions; i < countOfChildsWithoutPP; i++)
        {
            childObjects[i].transform.position = transform.GetChild(i).position;
            childObjects[i].transform.rotation = transform.GetChild(i).rotation;
            childObjects[i].transform.localScale = transform.GetChild(i).localScale;

            childObjects[i].GetComponent<MeshRenderer>().enabled = true;
            childObjects[i].GetComponent<MeshCollider>().enabled = true;
        }
    }
}
