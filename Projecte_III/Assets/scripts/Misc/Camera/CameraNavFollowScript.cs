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
    private PlayerVehicleScript firstPlayer;

    private float savedSpeed;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = 0;
        savedSpeed = 7;

        players = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        firstPlayer = players.GetPlayer(0).GetComponent<PlayerVehicleScript>();

        StartCoroutine(StartDelayCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(firstPlayer.timerStartRace < 0 && !gameStarted)
        {
            navMeshAgent.speed = 7;
            gameStarted = true;
        }

        if (!gameStarted)
            return;

        int nearestPlayer = GetNearestPlayerFromLimit();
        
        if (savedSpeed < 10)
            savedSpeed += Time.deltaTime * 0.04f;

        if (nearestPlayer != -1)
        {
            Transform _player = players.GetPlayer(nearestPlayer);
            Vector3 velocity = _player.GetComponent<PlayerVehicleScript>().vehicleRB.velocity;
            velocity = new Vector3(velocity.x, 0, velocity.z);

            if (_player.InverseTransformDirection(limit.position - _player.transform.position).z > 10)
                navMeshAgent.speed = velocity.magnitude - 0.2f;
            else
                navMeshAgent.speed = velocity.magnitude + 2.5f;
        }
        else if(navMeshAgent.speed < savedSpeed)
            navMeshAgent.speed += Time.deltaTime * 0.5f;
        else if(navMeshAgent.speed > savedSpeed)
            navMeshAgent.speed -= Time.deltaTime * 0.5f;

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
            if (_player == null) continue;
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
