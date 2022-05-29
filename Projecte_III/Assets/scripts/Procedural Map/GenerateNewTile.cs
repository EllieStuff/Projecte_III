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

        newRoad = null;

        RoadData.Type roadType = lastTile.GetRoadType(random);

        newRoad = null;

        Task t;

        RoadData newObject = null;
        if (roadType == RoadData.Type.STRAIGHT)
            t = GetNewRoad(straightRoads, maxSpawnRates.straight);
        else if (roadType == RoadData.Type.LEFT)
            t = GetNewRoad(leftRoads, maxSpawnRates.left);
        else if (roadType == RoadData.Type.RIGHT)
            t = GetNewRoad(rightRoads, maxSpawnRates.right);

        while (newRoad == null) { yield return null; }

        newObject = newRoad.GetComponent<RoadData>();

        yield return null;

        Transform child = lastTile.transform.GetChild(0).Find("NewSpawn");

        yield return null;

        newObject.transform.position = child.position;

        yield return null;

        //Vector3 _scale = newScale;
        //newObject.transform.localScale = _scale;
        newObject.transform.rotation = Quaternion.RotateTowards(newObject.transform.rotation, child.rotation, 360);

        yield return null;

        newObject.gameObject.SetActive(true);
        
        yield return null;

        if (transform.childCount > tilesMargin)
            Destroy(transform.GetChild(0).gameObject);

        yield return null;

        lastTile = newObject;

        yield return null;

        GetCheckpointPositions(ref newObject);

        yield return null;

        Task t2 = navMesh.BuildNavMesh();

        while (!t2.IsCompleted) { yield return null; }

        yield return null;

        cameraFollow.UpdateDestination();

        yield return null;

        yield return 0;
    }

    private async Task GetNewRoad(List<RoadData> _roadList, float _maxSpawnRate)
    {
        float random = Random.Range(0, _maxSpawnRate);
        float currRndAmount = 0;
        foreach (RoadData road in _roadList)
        {
            if (random > currRndAmount && random < currRndAmount + road.SpawnRate)
            {
                Task t3 = OptimizedInstantiate(road.transform);
                break;
            }

            currRndAmount += road.SpawnRate;
        }
    }

    private async Task OptimizedInstantiate(Transform parent) 
    {
        Transform newObject = new GameObject("ROAD").transform;
        newObject.gameObject.SetActive(false);
        newObject.parent = transform;
        RoadData dataSavedRoad = parent.gameObject.GetComponent<RoadData>();
        RoadData dataNewRoad = newObject.gameObject.AddComponent<RoadData>();

        dataNewRoad.baseSpawnRate = dataSavedRoad.baseSpawnRate;
        dataNewRoad.roadType = dataSavedRoad.roadType;
        dataNewRoad.spawnRates.left = dataSavedRoad.spawnRates.left;
        dataNewRoad.spawnRates.right = dataSavedRoad.spawnRates.right;
        dataNewRoad.spawnRates.straight = dataSavedRoad.spawnRates.straight;

        foreach (Transform child in parent)
        {
            Instantiate(child.gameObject, newObject);
        }
        newRoad = newObject.gameObject;
    }
}
