using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostTrigger : MonoBehaviour
{
    VehicleTriggerAndCollisionEvents playerEvents;
    Rigidbody playerRB;
    PlayerVehicleScript playerScript;

    private void Start()
    {
        playerEvents = transform.parent.parent.GetComponent<VehicleTriggerAndCollisionEvents>();
        playerRB = playerEvents.GetComponent<Rigidbody>();
        playerScript = playerEvents.GetComponent<PlayerVehicleScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerEvents.inmunity && other.gameObject.tag.Contains("Player"))
        {
            if(playerEvents.TimerRespawn < 0.5f)
            {
                playerScript.dash = true;
                float _speed = 10;
                Vector3 dir = -(transform.position - other.transform.position);
                dir = new Vector3(dir.x, 0.0f, 0.0f).normalized;
                Vector3 dirLocal = transform.InverseTransformDirection(dir);
                playerRB.velocity = transform.TransformDirection(dirLocal.x * _speed, dirLocal.y * _speed, transform.InverseTransformDirection(playerRB.velocity).z);
            }
            playerEvents.StopGhost = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerEvents.inmunity && other.gameObject.tag.Contains("Player"))
        {
            playerScript.dash = false;
            playerEvents.StopGhost = true;
        }
    }
}
