using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraNavFollowScript : MonoBehaviour
{
    const float MARGIN = 0.5f;

    [SerializeField] GenerateNewTile roadManager;

    [SerializeField] Transform cameraCheckpointsSet;
    [SerializeField] private float startDelay = 6.0f;
    [SerializeField] private float stopSpeed = 1.0f;

    [SerializeField] private List<Vector3> cameraCheckpoints = new List<Vector3>();
    private NavMeshAgent navMeshAgent;
    private int currCheckpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(StartDelayCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Vector3.Distance(transform.position, cameraCheckpoints[0]));
        //Debug.Log(navMeshAgent.destination);
        if (cameraCheckpoints.Count > 0 && Vector3.Distance(transform.position, cameraCheckpoints[0]) < MARGIN)
        {
            if(cameraCheckpoints.Count > 1)
                cameraCheckpoints.RemoveAt(0);
            UpdateDestination();
        }
    }

    public void UpdateDestination()
    {
        if(cameraCheckpoints.Count > 0)
            navMeshAgent.destination = cameraCheckpoints[0];
    }

    public void AddCheckPoints(ref List<Vector3> newCheckpoints)
    {
        foreach (var _checkPoints in newCheckpoints)
        {
            cameraCheckpoints.Add(_checkPoints);
        }
    }

    public void ReachedGoal()
    {
        Debug.Log("Goal Reached");
        StartCoroutine(StopCarCoroutine());
    }

    IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        UpdateDestination();
    }

    IEnumerator StopCarCoroutine()
    {
        while (navMeshAgent.speed > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            navMeshAgent.speed -= stopSpeed * Time.deltaTime;
        }

        if (navMeshAgent.speed < 0.0f) navMeshAgent.speed = 0.0f;
    }
}
