using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Threading.Tasks;

public class GenerateNewTile : MonoBehaviour
{
    [SerializeField] RoadData[] tiles;

    [SerializeField] CameraNavFollowScript cameraFollow = null;
    [SerializeField] RoadData lastTile = null;

    NavMeshSurface navMesh;

    RoadData.SpawnRateSet maxSpawnRates = new RoadData.SpawnRateSet(0);

    List<RoadData> straightRoads = new List<RoadData>();
    List<RoadData> leftRoads = new List<RoadData>();
    List<RoadData> rightRoads = new List<RoadData>();

    const int tilesMargin = 3;

    GameObject newRoad;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshSurface>();

        GameObject[] tilesGO = Resources.LoadAll<GameObject>("Prefabs/ProceduralMap");
        tiles = new RoadData[tilesGO.Length];
        for (int i = 0; i < tilesGO.Length; i++)
        {
            tiles[i] = tilesGO[i].GetComponent<RoadData>();
            tiles[i].gameObject.SetActive(false);

            if (tiles[i].RoadType == RoadData.Type.STRAIGHT)
            {
                straightRoads.Add(tiles[i]);
                maxSpawnRates.straight += tiles[i].SpawnRate;
            }
            else if (tiles[i].RoadType == RoadData.Type.LEFT)
            {
                leftRoads.Add(tiles[i]);
                maxSpawnRates.left += tiles[i].SpawnRate;
            }
            else if (tiles[i].RoadType == RoadData.Type.RIGHT)
            {
                rightRoads.Add(tiles[i]);
                maxSpawnRates.right += tiles[i].SpawnRate;
            }
        }

        GetCheckpointPositions(ref lastTile);
    }

    void GetCheckpointPositions(ref RoadData tile)
    {
        
        List<Vector3> outList = new List<Vector3>();

        Transform checkPoints = tile.GetCheckpoints();
        for (int j = 0; j < checkPoints.childCount; j++)
        {
            outList.Add(checkPoints.GetChild(j).position);
        }

        cameraFollow.AddCheckPoints(ref outList);
    }

    public IEnumerator CalculateNewTile()
    {
        newRoad = null;

        float random = Random.Range(0, 100);
        RoadData.Type roadType = lastTile.GetRoadType(random);

        RoadData newObject = null;
        if (roadType == RoadData.Type.STRAIGHT)
            StartCoroutine(GetNewRoad(straightRoads, maxSpawnRates.straight));
        else if (roadType == RoadData.Type.LEFT)
            StartCoroutine(GetNewRoad(leftRoads, maxSpawnRates.left));
        else if (roadType == RoadData.Type.RIGHT)
            StartCoroutine(GetNewRoad(rightRoads, maxSpawnRates.right));

        while (newRoad == null) { yield return null; }

        newObject = newRoad.GetComponent<RoadData>();

        Transform child = lastTile.transform.GetChild(0).Find("NewSpawn");

        newObject.transform.position = child.position;

        //Vector3 _scale = newScale;
        //newObject.transform.localScale = _scale;
        newObject.transform.rotation = Quaternion.RotateTowards(newObject.transform.rotation, child.rotation, 360);

        newObject.gameObject.SetActive(true);

        if (transform.childCount > tilesMargin)
            Destroy(transform.GetChild(0).gameObject);

        lastTile = newObject;

        GetCheckpointPositions(ref newObject);

        navMesh.UpdateNavMesh(navMesh.navMeshData);

        cameraFollow.UpdateDestination();

        yield return 0;
    }

    IEnumerator GetNewRoad(List<RoadData> _roadList, float _maxSpawnRate)
    {
        float random = Random.Range(0, _maxSpawnRate);
        float currRndAmount = 0;
        foreach (RoadData road in _roadList)
        {
            if (random > currRndAmount && random < currRndAmount + road.SpawnRate)
            {
                StartCoroutine(OptimizedInstantiate(road.transform));
                yield return 0;
            }

            currRndAmount += road.SpawnRate;
        }
        yield return 0;
    }

    IEnumerator OptimizedInstantiate(Transform parent) 
    {
        GameObject _newRoad = Instantiate(parent.gameObject, transform);
        newRoad = _newRoad;
        yield return 0;
    }
}
