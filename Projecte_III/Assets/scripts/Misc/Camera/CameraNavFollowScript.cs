using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraNavFollowScript : MonoBehaviour
{
    const float MARGIN = 0.5f;

    [SerializeField] Transform cameraCheckpointsSet;
    [SerializeField] private float startDelay = 6.0f;
    [SerializeField] private float stopSpeed = 1.0f;

    private Vector3[] cameraCheckpoints;
    private NavMeshAgent navMeshAgent;
    private int currCheckpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        cameraCheckpoints = new Vector3[cameraCheckpointsSet.childCount];
        for (int i = 0; i < cameraCheckpoints.Length; i++)
            cameraCheckpoints[i] = cameraCheckpointsSet.GetChild(i).position;

        StartCoroutine(StartDelayCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cameraCheckpoints[currCheckpoint]) < MARGIN)
        {
            currCheckpoint++;
            if (currCheckpoint >= cameraCheckpoints.Length)
                currCheckpoint = 0;

            navMeshAgent.destination = cameraCheckpoints[currCheckpoint];
        }
    }


    public void ReachedGoal()
    {
        StartCoroutine(StopCarCoroutine());
    }


    IEnumerator StartDelayCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
        navMeshAgent.destination = cameraCheckpoints[currCheckpoint];
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
