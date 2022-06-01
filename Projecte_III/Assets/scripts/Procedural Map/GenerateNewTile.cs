using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class GenerateNewTile : MonoBehaviour
{
    [SerializeField] RoadData[] tiles;

    [SerializeField] CameraNavFollowScript cameraFollow = null;
    [SerializeField] RoadData lastTile = null;
    [SerializeField] ComputeShader computeShader_;

    [SerializeField] Instancer _instancer;

    NavMeshSurface navMesh;

    RoadData.SpawnRateSet maxSpawnRates = new RoadData.SpawnRateSet(0);

    List<RoadData> straightRoads = new List<RoadData>();
    List<RoadData> leftRoads = new List<RoadData>();
    List<RoadData> rightRoads = new List<RoadData>();

    const int tilesMargin = 3;

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

    public void CalculateNewTile()
    {
        float random = Random.Range(0, 100);
        RoadData.Type roadType = lastTile.GetRoadType(random);

        RoadData newObject = null;
        if (roadType == RoadData.Type.STRAIGHT)
            newObject = GetNewRoad(ref straightRoads, maxSpawnRates.straight);
        else if (roadType == RoadData.Type.LEFT)
            newObject = GetNewRoad(ref leftRoads, maxSpawnRates.left);
        else if (roadType == RoadData.Type.RIGHT)
            newObject = GetNewRoad(ref rightRoads, maxSpawnRates.right);
        
        if (newObject == null) Debug.LogError("Upsie");

        Transform child = lastTile.transform.GetChild(0).Find("NewSpawn");

        newObject.transform.position = child.position;

        //Vector3 _scale = newScale;
        //newObject.transform.localScale = _scale;
        newObject.transform.rotation = Quaternion.RotateTowards(newObject.transform.rotation, child.rotation, 360);

        if(transform.childCount > tilesMargin)
            Destroy(transform.GetChild(0).gameObject);

        lastTile = newObject;

        GetCheckpointPositions(ref newObject);

        navMesh.BuildNavMesh();
        cameraFollow.UpdateDestination();
    }

    RoadData GetNewRoad(ref List<RoadData> _roadList, float _maxSpawnRate)
    {
        float random = Random.Range(0, _maxSpawnRate);
        float currRndAmount = 0;
        foreach (RoadData road in _roadList)
        {
            if (random > currRndAmount && random < currRndAmount + road.SpawnRate)
            {
                GameObject _road = road.gameObject;
                GameObject newRoad = Instantiate(_road, transform);
                return newRoad.GetComponent<RoadData>();
            }

            currRndAmount += road.SpawnRate;
        }

        return null;
    }

    public class Instancer : MonoBehaviour
    {
        public int instances;
        public Mesh mesh;
        public Material[] materials;
        private List<List<Matrix4x4>> batches = new List<List<Matrix4x4>>();
        private void RenderBatches()
        {
            foreach(var batch in batches)
            {
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    Graphics.DrawMeshInstanced(mesh, i, materials[i], batch);
                }
            }
        }

        private void Update()
        {
            RenderBatches();
        }

        private void Start()
        {
            int addedMatrices = 0;
            batches.Add(new List<Matrix4x4>());

            for (int i = 0; i < instances; i++)
            {
                if(addedMatrices < 1000)
                {
                    batches[batches.Count - 1].Add(Matrix4x4.TRS(new Vector3(Random.Range(0, 50), Random.Range(0, 50), Random.Range(0, 50)), Random.rotation, Vector3.back /*??*/));
                    addedMatrices += 1;
                }
                else
                {
                    batches.Add(new List<Matrix4x4>());
                    addedMatrices = 0;
                }
            }
        }
    }


}
