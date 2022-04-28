using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraNavFollowScript : MonoBehaviour
{
    const float MARGIN = 0.5f;

    [SerializeField] GenerateNewTile roadManager;
    private PlayersManager players;

    [SerializeField] Transform cameraCheckpointsSet;
    [SerializeField] private float startDelay = 6.0f;
    [SerializeField] private float stopSpeed = 1.0f;
    [SerializeField] private Transform limit;

    [SerializeField] private List<Vector3> cameraCheckpoints = new List<Vector3>();
    private NavMeshAgent navMeshAgent;
    private int currCheckpoint = 0;

    private float savedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 7;
        savedSpeed = 7;

        players = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();

        StartCoroutine(StartDelayCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        int nearestPlayer = GetNearestPlayerFromLimit();
        
        if (savedSpeed < 10)
            savedSpeed += Time.deltaTime * 0.04f;

        if (nearestPlayer != -1)
            navMeshAgent.speed = players.GetPlayer(nearestPlayer).GetComponent<PlayerVehicleScript>().vehicleRB.velocity.magnitude;
        else if(navMeshAgent.speed < savedSpeed)
            navMeshAgent.speed += Time.deltaTime * 0.5f;
        else if(navMeshAgent.speed > savedSpeed)
            navMeshAgent.speed -= Time.deltaTime * 0.5f;

        //Debug.Log(Vector3.Distance(transform.position, cameraCheckpoints[0]));
        //Debug.Log(navMeshAgent.destination);

        if (cameraCheckpoints.Count > 0 && Vector3.Distance(transform.position, cameraCheckpoints[0]) < MARGIN)
        {
            if(cameraCheckpoints.Count > 1)
                cameraCheckpoints.RemoveAt(0);
            UpdateDestination();
        }
    }

    private int GetNearestPlayerFromLimit()
    {
        int nearestPlayer = -1;
        float distance = 100;
        for (int i = 0; i < players.numOfPlayers; i++)
        {
            PlayerVehicleScript _player = players.GetPlayer(i).GetComponent<PlayerVehicleScript>();
            float calculatedDistance = Vector3.Distance(_player.transform.position, limit.position);
            if (_player.transform.InverseTransformDirection(_player.vehicleRB.velocity).z > 6 && calculatedDistance < distance && calculatedDistance <= 50) 
            {
                distance = calculatedDistance;
                nearestPlayer = i;
            }
        }
        return nearestPlayer;
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
