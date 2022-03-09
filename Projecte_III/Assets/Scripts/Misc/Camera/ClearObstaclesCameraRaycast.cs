using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstaclesCameraRaycast : MonoBehaviour
{
    [SerializeField] Material auxPolyBrushMat;
    [SerializeField] float clearingRadius = 10.0f;
    [SerializeField] float lerpTime = 3.0f;

    Transform player, camera;
    class ObstacleData
    {
        public MeshRenderer renderer; public Material savedMaterial;
        public ObstacleData(MeshRenderer _renderer, Material _savedMaterial) { renderer = _renderer; savedMaterial = _savedMaterial; }
    }
    List<ObstacleData> obstaclesData = new List<ObstacleData>();
    //List<MeshRenderer> obstaclesMeshes = new List<MeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(camera.position, player.position, Color.red);
        RaycastHit[] hits = Physics.SphereCastAll(new Ray(player.position, player.up), clearingRadius, 20.0f);
        UpdateHits(hits);
        //hits = Physics.SphereCastAll(camera.position, clearingRadius, player.position, Vector3.Distance(camera.position, player.position));
        //UpdateHits(hits);

    }

    void UpdateHits(RaycastHit[] _hits)
    {
        if (_hits.Length > 0)
        {
            // Erase Unnecesari Data
            int idx = 0;
            while (idx < obstaclesData.Count)
            {
                bool found = false;
                for (int i = 0; i < _hits.Length && !found; i++)
                {
                    if (_hits[i].transform.tag == "Tree")
                    {
                        if (obstaclesData[idx].renderer.transform == _hits[i].transform)
                            found = true;
                    }
                }

                if (found) idx++;
                else
                {
                    StartCoroutine(ReappearObstacle(obstaclesData[idx]));
                    obstaclesData.RemoveAt(idx);
                }
            }

            // Looks For new Data
            for (int i = 0; i < _hits.Length; i++)
            {
                //Debug.Log("Colliding " + _hits[i].transform.tag);
                if (_hits[i].transform.tag == "Tree")
                {
                    if (obstaclesData.Find(_obs => _obs.renderer.transform == _hits[i].transform) == null)
                    {
                        MeshRenderer renderer = _hits[i].transform.GetComponent<MeshRenderer>();
                        if (renderer != null)
                        {
                            ObstacleData newObstacleData = new ObstacleData(renderer, renderer.material);
                            obstaclesData.Add(newObstacleData);
                            StartCoroutine(ClearObstacle(newObstacleData));
                        }
                    }
                }
            }

        }
    }


    IEnumerator ReappearObstacle(ObstacleData _obsData)
    {
        Material targetMaterial = _obsData.savedMaterial;
        //prevMaterials.RemoveAt(0);
        Color targetColor = targetMaterial.color;

        float timer = 0.0f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            _obsData.renderer.material.color = Color.Lerp(_obsData.renderer.material.color, targetColor, timer / lerpTime);
        }
        _obsData.renderer.material = targetMaterial;
    }
    IEnumerator ClearObstacle(ObstacleData _obsData)
    {
        //prevMaterials.Add(_obsMesh.material);
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
    }


}
