using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstaclesCameraRaycast : MonoBehaviour
{
    [SerializeField] Material auxPolyBrushMat;
    [SerializeField] Material[] auxRaftMaterials;
    [SerializeField] float clearingRadius = 10.0f;
    [SerializeField] float lerpTime = 3.0f;
    [SerializeField] float forwardMargin = 10.0f;
    [SerializeField] float downMargin = 5.0f;

    Transform player;
    Camera camera;

    //Transform player, camera;
    public enum ObstacleState { DEFAULT, APPEARING, DISAPPEARING }
    class ObstacleData
    {
        public MeshRenderer renderer; public Material[] savedMaterials; public ObstacleState state = ObstacleState.DISAPPEARING;
        public ObstacleData(MeshRenderer _renderer, Material[] _savedMaterials) { renderer = _renderer; savedMaterials = _savedMaterials; }
    }
    Dictionary<Transform, ObstacleData[]> obstaclesDataDict = new Dictionary<Transform, ObstacleData[]>();

    // Start is called before the first frame update
    void Start()
    {
        //int playerId = GetComponent<CameraScript>().playerId;
        //player = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayer(playerId);
        //camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 currMargin = player.forward * forwardMargin;
        //currMargin.y += downMargin;
        //Vector3 cameraPos = camera.transform.position + currMargin;
        //Vector3 playerPos = player.position + currMargin;

        //Debug.DrawLine(cameraPos, playerPos, Color.red);
        //RaycastHit[] hits = Physics.SphereCastAll(new Ray(playerPos, cameraPos - playerPos), clearingRadius, Vector3.Distance(cameraPos, playerPos));
        //UpdateHitsDictionary(hits);
    }

    
    void UpdateHitsDictionary(RaycastHit[] _hits)
    {
        // Erase Unnecesari Data
        if (obstaclesDataDict.Count > 0)
        {
            foreach (Transform key in obstaclesDataDict.Keys)
            {
                bool found = false;
                for (int i = 0; i < _hits.Length && !found; i++)
                {
                    if (IsObstacle(_hits[i].transform.tag) && key == _hits[i].transform)
                        found = true;
                }


                if (!found && obstaclesDataDict[key][0].state == ObstacleState.DEFAULT)
                {
                    obstaclesDataDict[key][0].state = ObstacleState.APPEARING;
                    StartCoroutine(ReappearObstacle(obstaclesDataDict[key], key));
                }

            }
        }

        // Looks For new Data
        for (int i = 0; i < _hits.Length; i++)
        {
            Transform currHit = _hits[i].transform;
            if (IsObstacle(currHit.tag))
            {
                if (!obstaclesDataDict.ContainsKey(currHit))
                {
                    ObstacleData[] obstaclesData = GetObstaclesdata(currHit);
                    //ObstacleData newObstacleData = new ObstacleData(renderer, renderer.materials);
                    if (obstaclesData != null)
                    {
                        obstaclesDataDict.Add(currHit, obstaclesData);
                        StartCoroutine(ClearObstacle(obstaclesData, currHit));
                    }
                }
                else if (obstaclesDataDict[currHit][0].state == ObstacleState.APPEARING)
                {
                    obstaclesDataDict[currHit][0].state = ObstacleState.DISAPPEARING;
                    StartCoroutine(ClearObstacle(obstaclesDataDict[currHit], currHit));
                }
            }
        }
    }

    bool IsObstacle(string _objectTag)
    {
        return _objectTag == "Tree" || _objectTag == "Raft";
    }

    ObstacleData[] GetObstaclesdata(Transform _obstacle)
    {
        ObstacleData[] obstaclesData = null;
        MeshRenderer renderer = null;
        switch (_obstacle.tag)
        {
            case "Tree":
                renderer = _obstacle.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    obstaclesData = new ObstacleData[renderer.materials.Length];
                    obstaclesData[0] = new ObstacleData(renderer, renderer.materials);
                }
                break;

            case "Raft":
                renderer = _obstacle.GetComponentInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    obstaclesData = new ObstacleData[renderer.materials.Length];
                    for (int i = 0; i < _obstacle.childCount; i++)
                    {
                        MeshRenderer raftRenderer = _obstacle.GetChild(i).GetComponent<MeshRenderer>();
                        obstaclesData[i] = new ObstacleData(raftRenderer, raftRenderer.materials);
                    }
                }
                break;

            default:
                Debug.LogWarning("Tag not found");
                break;
        }
        return obstaclesData;
    }


    IEnumerator ReappearObstacle(ObstacleData[] _obsData, Transform _key)
    {
        yield return new WaitForEndOfFrame();
        Material[] targetMaterials = _obsData[0].savedMaterials;
        Color[] targetColors = new Color[targetMaterials.Length];
        for(int i = 0; i < targetColors.Length; i++)
            targetColors[i] = targetMaterials[i].color;

        float timer = 0.0f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            if (obstaclesDataDict[_key][0].state != ObstacleState.APPEARING)
                yield break;
            timer += Time.deltaTime;
            for (int j = 0; j < _obsData.Length; j++)
            {
                for (int i = 0; i < _obsData[j].renderer.materials.Length; i++)
                    _obsData[j].renderer.materials[i].color = Color.Lerp(_obsData[j].renderer.materials[i].color, targetColors[i], timer / lerpTime);
            }
        }
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < _obsData.Length; i++)
            _obsData[i].renderer.materials = targetMaterials;
        obstaclesDataDict.Remove(_key);
    }
    IEnumerator ClearObstacle(ObstacleData[] _obsData, Transform _key)
    {
        yield return new WaitForEndOfFrame();
        if (_key.CompareTag("Tree"))
        {
            _obsData[0].renderer.material = new Material(auxPolyBrushMat);
        }
        else if (_key.CompareTag("Raft"))
        {
            Material[] auxMaterials = new Material[auxRaftMaterials.Length];
            for (int i = 0; i < auxMaterials.Length; i++)
            {
                auxMaterials[i] = new Material(auxRaftMaterials[i]);
                _obsData[i].renderer.materials = auxMaterials;
            }

            for (int i = 0; i < _obsData[0].renderer.materials.Length; i++)
                _obsData[i].renderer.materials = auxMaterials;
        }

        Color[] targetColors = new Color[_obsData[0].renderer.materials.Length];
        for (int j = 0; j < _obsData.Length; j++)
        {
            for (int i = 0; i < targetColors.Length; i++)
            {
                targetColors[i] = _obsData[j].renderer.materials[i].color;
                targetColors[i].a = 0.3f;
            }
        }


        float timer = 0.0f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            for (int j = 0; j < _obsData.Length; j++)
            {
                for (int i = 0; i < _obsData[j].renderer.materials.Length; i++)
                    _obsData[j].renderer.materials[i].color = Color.Lerp(_obsData[j].renderer.materials[i].color, targetColors[i], timer / lerpTime);
            }
        }
        obstaclesDataDict[_key][0].state = ObstacleState.DEFAULT;
    }


}
