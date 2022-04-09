using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleData : MonoBehaviour
{
    Transform[] possiblePositions;
    MeshRenderer mesh;
    MeshCollider col;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<MeshCollider>();

        possiblePositions = new Transform[transform.childCount];
        for (int i = 0; i < possiblePositions.Length; i++)
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
    }
}
