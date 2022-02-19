using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plungerInstance : MonoBehaviour
{
    public int playerNum;
    public Vector3 normalDir;
    public bool destroyPlunger;
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
    private string collisionTag;

    private void Start()
    {
        startRot = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        if(normalDir != Vector3.zero)
            body.velocity = new Vector3(plungerVelocity * normalDir.x, normalDir.y, plungerVelocity * normalDir.z);
        else
            body.velocity = transform.TransformDirection(new Vector3(0, 0.5f, plungerVelocity));
        if (transform.InverseTransformDirection(body.velocity).z < 0)
            Destroy(gameObject);
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.GetChild(1).position);
        GetComponent<LineRenderer>().SetPosition(1, playerShotPlunger.transform.position);

        if (plungerHit && !destroyPlunger)
        {
            if (otherQuad != null)
            {
                transform.position = otherQuad.transform.position;
                Vector3 inverseTransformDir = otherQuad.transform.InverseTransformDirection(otherQuad.GetComponent<Rigidbody>().velocity);
                if (inverseTransformDir.z >= -5 && otherQuad.GetComponent<Rigidbody>().velocity.y < 3)
                    otherQuad.GetComponent<Rigidbody>().velocity += otherQuad.transform.TransformDirection(new Vector3(0, 0, -0.5f));
            }

            if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) > 5 && !collisionTag.Equals("ground"))
            {
                timerDestroy -= Time.deltaTime;
                if (timerDestroy <= 0 || prepareToDestroy || Vector3.Distance(transform.position, playerShotPlunger.transform.position) >= 25)
                    destroyPlunger = true;
                float oldSpeedY = playerShotPlunger.GetComponent<Rigidbody>().velocity.y;
                if (Mathf.Abs(transform.TransformDirection(playerShotPlunger.GetComponent<Rigidbody>().velocity).z) > 0 && playerShotPlunger.GetComponent<Rigidbody>().velocity.y < 5)
                {
                    if (playerNum == 1 && !prepareToDestroy)
                        playerShotPlunger.GetComponent<PlayerVehicleScript>().vehicleMaxSpeed = 30;
                    if (playerNum == 2 && !prepareToDestroy)
                        playerShotPlunger.GetComponent<PlayerVehicleScriptP2>().vehicleMaxSpeed = 30;

                    playerShotPlunger.GetComponent<Rigidbody>().velocity += playerShotPlunger.transform.TransformDirection(new Vector3(0, 0, 0.5f));
                    playerShotPlunger.GetComponent<Rigidbody>().velocity = new Vector3(playerShotPlunger.GetComponent<Rigidbody>().velocity.x, oldSpeedY, playerShotPlunger.GetComponent<Rigidbody>().velocity.z);
                }
                Debug.Log(playerShotPlunger.GetComponent<Rigidbody>().velocity);
            }
            else if (!collisionTag.Equals("ground"))
            {
                prepareToDestroy = true;
            }
            else if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 5 || Vector3.Distance(transform.position, playerShotPlunger.transform.position) > 20)
            {
                destroyPlunger = true;
            }

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, endQuad, Time.deltaTime * plungerSpeedLerp);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * plungerSpeedLerp);
        }
        else if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 5 || Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 50 && !destroyPlunger)
            this.transform.rotation = startRot;
        else if(!destroyPlunger)
            destroyPlunger = true;
        else
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            if (transform.localScale.magnitude <= 0.5f || transform.localScale.magnitude > 2f)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!plungerHit)
        {
            collisionTag = collision.gameObject.tag;

            if(collision.gameObject.tag.Contains("Player"))
                otherQuad = collision.gameObject;

            this.transform.parent = collision.transform;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit raycastHit, 100))
            {
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
