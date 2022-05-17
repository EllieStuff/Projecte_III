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
    [SerializeField] private Transform startRoad;

    [SerializeField] private float positionSmoothness = 1.0f;
    [SerializeField] private float rotationSmoothness = 1.0f;

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
        navMeshAgent.angularSpeed = 0;
        navMeshAgent.enabled = false;
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
        if (players.players.Length == 0) return;
        var player = players.GetPlayer(nearestPlayer);

        var posPlayerCam = player.transform.position;
        posPlayerCam.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, posPlayerCam, Time.unscaledDeltaTime * positionSmoothness);

        Quaternion lookOnLook = Quaternion.LookRotation(Vector3.ProjectOnPlane(player.transform.forward, Vector3.up));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.unscaledDeltaTime * rotationSmoothness);

        //if (savedSpeed < 10)
        //    savedSpeed += Time.deltaTime * 0.04f;

        //if (nearestPlayer != -1)
        //{
        //    Vector3 velocity = players.GetPlayer(nearestPlayer).GetComponent<PlayerVehicleScript>().vehicleRB.velocity;
        //    velocity = new Vector3(velocity.x, 0, velocity.z);
        //    navMeshAgent.speed = velocity.magnitude - 0.2f;
        //}
        //else if(navMeshAgent.speed < savedSpeed)
        //    navMeshAgent.speed += Time.deltaTime * 0.5f;
        //else if(navMeshAgent.speed > savedSpeed)
        //    navMeshAgent.speed -= Time.deltaTime * 0.5f;

        //Debug.Log(Vector3.Distance(transform.position, cameraCheckpoints[0]));
        //Debug.Log(navMeshAgent.destination);

        //if (cameraCheckpoints.Count > 0 && Vector3.Distance(transform.position, cameraCheckpoints[0]) < MARGIN)
        //{
        //    if(cameraCheckpoints.Count > 1)
        //        cameraCheckpoints.RemoveAt(0);
        //    UpdateDestination();
        //}

        //if (cameraCheckpoints.Count > 0)
        //{
        //    var destination = navMeshAgent.destination;
        //    var lookAtTargetDirection = (destination - transform.position).normalized;
        //    var lookAt = Vector3.ProjectOnPlane(lookAtTargetDirection, Vector3.up).normalized;

        //    Quaternion lookOnLook = Quaternion.LookRotation(lookAt);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * rotationSmoothness);
        //}





    }

    private int GetNearestPlayerFromLimit()
    {
        int nearestPlayer = -1;
        float distance = 100;
        for (int i = 0; i < players.numOfPlayers; i++)
        {
            PlayerVehicleScript _player = players.GetPlayer(i).GetComponent<PlayerVehicleScript>();
            float calculatedDistance = _player.transform.InverseTransformDirection((transform.position + transform.TransformDirection(Vector3.forward * 10)) - _player.transform.position).z;
            if (_player.transform.InverseTransformDirection(_player.vehicleRB.velocity).z > 6 && calculatedDistance < distance) 
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
