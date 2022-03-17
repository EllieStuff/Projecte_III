using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstaclesCameraRaycast : MonoBehaviour
{
    [SerializeField] Material auxPolyBrushMat;
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
        public MeshRenderer renderer; public Material savedMaterial; public ObstacleState state = ObstacleState.DISAPPEARING;
        public ObstacleData(MeshRenderer _renderer, Material _savedMaterial) { renderer = _renderer; savedMaterial = _savedMaterial; }
    }
    Dictionary<Transform, ObstacleData> obstaclesDataDict = new Dictionary<Transform, ObstacleData>();

    // Start is called before the first frame update
    void Start()
    {
        int playerId = GetComponent<CameraScript>().playerId;
        player = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>().GetPlayer(playerId);
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currMargin = player.forward * forwardMargin;
        currMargin.y += downMargin;
        Vector3 cameraPos = camera.transform.position + currMargin;
        Vector3 playerPos = player.position + currMargin;

        Debug.DrawLine(cameraPos, playerPos, Color.red);
        RaycastHit[] hits = Physics.SphereCastAll(new Ray(playerPos, cameraPos - playerPos), clearingRadius, Vector3.Distance(cameraPos, playerPos));
        UpdateHitsDictionary(hits);
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

                if (!found && obstaclesDataDict[key].state == ObstacleState.DEFAULT)
                {
                    obstaclesDataDict[key].state = ObstacleState.APPEARING;
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
                    MeshRenderer renderer = currHit.GetComponent<MeshRenderer>();
                    if (renderer != null)
                    {
                        ObstacleData newObstacleData = new ObstacleData(renderer, renderer.material);
                        obstaclesDataDict.Add(currHit, newObstacleData);
                        StartCoroutine(ClearObstacle(newObstacleData, currHit));
                    }
                }
                else if(obstaclesDataDict[currHit].state == ObstacleState.APPEARING)
                {
                    obstaclesDataDict[currHit].state = ObstacleState.DISAPPEARING;
                    StartCoroutine(ClearObstacle(obstaclesDataDict[currHit], currHit));
                }
            }
        }
    }

    bool IsObstacle(string _objectTag)
    {
        return _objectTag == "Tree";
    }


    IEnumerator ReappearObstacle(ObstacleData _obsData, Transform _key)
    {
        yield return new WaitForEndOfFrame();
        Material targetMaterial = _obsData.savedMaterial;
        Color targetColor = targetMaterial.color;

        float timer = 0.0f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            if (obstaclesDataDict[_key].state != ObstacleState.APPEARING)
                yield break;
            timer += Time.deltaTime;
            _obsData.renderer.material.color = Color.Lerp(_obsData.renderer.material.color, targetColor, timer / lerpTime);
        }
        yield return new WaitForEndOfFrame();
        _obsData.renderer.material = targetMaterial;
        obstaclesDataDict.Remove(_key);
    }
    IEnumerator ClearObstacle(ObstacleData _obsData, Transform _key)
    {
        yield return new WaitForEndOfFrame();
        _obsData.renderer.material = new Material(auxPolyBrushMat);
        Color targetColor = _obsData.renderer.material.color;
        targetColor.a = 0.3f;

        float timer = 0.0f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            _obsData.renderer.material.color = Color.Lerp(_obsData.renderer.material.color, targetColor, timer / lerpTime);
        }
        obstaclesDataDict[_key].state = ObstacleState.DEFAULT;
    }


}
