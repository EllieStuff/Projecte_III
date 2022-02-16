using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plungerInstance : MonoBehaviour
{
    public int playerNum;
    [SerializeField] Rigidbody body;
    [SerializeField] private float plungerVelocity;
    [SerializeField] private float plungerSpeedLerp;
    public bool plungerHit;
    public GameObject playerShotPlunger;
    private GameObject otherQuad;
    Quaternion startRot;
    Quaternion endQuad;
    float timerDestroy = 5;
    private bool prepareToDestroy;

    private void Start()
    {
        startRot = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        body.velocity = transform.TransformDirection(new Vector3(0, 0.5f, plungerVelocity));
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, playerShotPlunger.transform.position);

        if(plungerHit)
        {
            if (otherQuad != null)
            {
                transform.position = otherQuad.transform.position;
            }

            if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) > 5)
            {
                timerDestroy -= Time.deltaTime;
                if (timerDestroy <= 0 || prepareToDestroy || Vector3.Distance(transform.position, playerShotPlunger.transform.position) >= 25)
                    Destroy(gameObject);
                Debug.Log(transform.TransformDirection(playerShotPlunger.GetComponent<Rigidbody>().velocity).z);
                float oldSpeedY = playerShotPlunger.GetComponent<Rigidbody>().velocity.y;
                if (Mathf.Abs(transform.TransformDirection(playerShotPlunger.GetComponent<Rigidbody>().velocity).z) > 0 && playerShotPlunger.GetComponent<Rigidbody>().velocity.y < 5)
                {
                    if (playerNum == 1 && !prepareToDestroy)
                        playerShotPlunger.GetComponent<PlayerVehicleScript>().vehicleMaxSpeed = 50;
                    if (playerNum == 2 && !prepareToDestroy)
                        playerShotPlunger.GetComponent<PlayerVehicleScriptP2>().vehicleMaxSpeed = 50;

                    playerShotPlunger.GetComponent<Rigidbody>().velocity += playerShotPlunger.transform.TransformDirection(new Vector3(0, 0, 0.5f));
                    playerShotPlunger.GetComponent<Rigidbody>().velocity = new Vector3(playerShotPlunger.GetComponent<Rigidbody>().velocity.x, oldSpeedY, playerShotPlunger.GetComponent<Rigidbody>().velocity.z);
                }
                Debug.Log(playerShotPlunger.GetComponent<Rigidbody>().velocity);
            }
            else
            {
                prepareToDestroy = true;
            }

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, endQuad, Time.deltaTime * plungerSpeedLerp);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * plungerSpeedLerp);
        }
        else
            this.transform.rotation = startRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (!plungerHit)
        {
            if(collision.gameObject.tag.Contains("Player"))
                otherQuad = collision.gameObject;

            this.transform.parent = collision.transform;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit raycastHit, 100))
            {
                Debug.Log(raycastHit.normal);
                float angle = Vector3.Dot(raycastHit.point.normalized, transform.position.normalized);
                angle *= Mathf.Rad2Deg;
                endQuad = Quaternion.AngleAxis(angle, raycastHit.normal);
            }

            body.useGravity = false;
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            plungerHit = true;
        }
    }
}
