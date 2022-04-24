using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    Transform[] possiblePositions;
    public GameObject[] obstacleArray;
    GameObject obstacleToSpawn;
    //MeshRenderer mesh;
    //MeshCollider col;
    int _randomObject;

    public int roadType;

    // Start is called before the first frame update
    void Start()
    {
        _randomObject = Random.Range(0, obstacleArray.Length);
        obstacleToSpawn = Instantiate(obstacleArray[_randomObject], transform);

        //mesh = obstacleMeshArray[_randomMesh].GetComponent<MeshRenderer>();
        //col = obstacleMeshArray[_randomMesh].GetComponent<MeshCollider>();

        possiblePositions = new Transform[transform.GetChild(roadType).childCount];
        for (int i = 0; i < possiblePositions.Length; i++)
        {
            possiblePositions[i] = transform.GetChild(roadType).GetChild(i);
            //Es millor que les meshes estiguin deshabilitades sempre
            //MeshRenderer _mesh = possiblePositions[i].GetComponent<MeshRenderer>();
            //if (_mesh != null)
              //_mesh.enabled = false;
        }

        SpawnObstacle();
    }

    void Update()
    {
        //Debug.Log("Road Type returned: " + roadType);
    }

    public void SpawnObstacle()
    {
        int _random = Random.Range(0, possiblePositions.Length);

        Transform _spawnedPosition = possiblePositions[_random];

        obstacleToSpawn.transform.localPosition = _spawnedPosition.localPosition;
        obstacleToSpawn.transform.rotation = _spawnedPosition.rotation;
        obstacleToSpawn.transform.localScale = _spawnedPosition.localScale;

        //if (mesh != null) mesh.enabled = true;

        //if (col != null) col.enabled = true;
    }
}
