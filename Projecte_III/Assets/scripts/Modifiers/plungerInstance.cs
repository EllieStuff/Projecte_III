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
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        AudioManager.Instance.Play_SFX("Plunger_Hit_SFX");
        startRot = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        
        if(normalDir != Vector3.zero)
            body.velocity = new Vector3(plungerVelocity * normalDir.x, normalDir.y + 0.75f, plungerVelocity * normalDir.z);
        else
            body.velocity = transform.TransformDirection(new Vector3(0, 0.5f, plungerVelocity));
        
        if (transform.InverseTransformDirection(body.velocity).z < 0)
            Destroy(gameObject);
    }

    private void Update()
    {
        line.SetPosition(0, transform.GetChild(1).position);
        line.SetPosition(1, playerShotPlunger.transform.position);

        if (plungerHit && !destroyPlunger)
        {
            if (otherQuad != null)
            {
                Rigidbody otherQuadRB = otherQuad.GetComponent<Rigidbody>();
                transform.position = otherQuad.transform.position;
                Vector3 inverseTransformDir = otherQuad.transform.InverseTransformDirection(otherQuadRB.velocity);
                
                if (inverseTransformDir.z >= -5 && otherQuadRB.velocity.y < 1)
                    otherQuadRB.velocity += otherQuad.transform.TransformDirection(new Vector3(0, 0, -0.5f));
                
                if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 2)
                    Destroy(gameObject);
                else if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) >= 25)
                    Destroy(gameObject);
            }

            if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) > 2 && !collisionTag.Equals("ground"))
            {
                Rigidbody playerRB = playerShotPlunger.GetComponent<Rigidbody>();
                timerDestroy -= Time.deltaTime;
                
                if (timerDestroy <= 0 || prepareToDestroy || Vector3.Distance(transform.position, playerShotPlunger.transform.position) >= 25)
                    destroyPlunger = true;

                float oldSpeedY = playerRB.velocity.y;

                if (Mathf.Abs(transform.TransformDirection(playerRB.velocity).z) > 0 && playerRB.velocity.y < 5)
                {
                    if (!prepareToDestroy)
                        playerShotPlunger.GetComponent<PlayerVehicleScript>().vehicleMaxSpeed = 35;

                    playerRB.velocity += playerShotPlunger.transform.TransformDirection(new Vector3(0, 0, 0.5f));
                    playerRB.velocity = new Vector3(playerRB.velocity.x, oldSpeedY, playerRB.velocity.z);
                }
            }
            else if (!collisionTag.Equals("ground"))
            {
                prepareToDestroy = true;
            }
            else if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 2 || Vector3.Distance(transform.position, playerShotPlunger.transform.position) > 20)
            {
                destroyPlunger = true;
            }

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, endQuad, Time.deltaTime * plungerSpeedLerp);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * plungerSpeedLerp);
        }
        else if (Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 2 || Vector3.Distance(transform.position, playerShotPlunger.transform.position) <= 50 && !destroyPlunger)
            this.transform.rotation = startRot;
        else if(!destroyPlunger)
            destroyPlunger = true;
        else
        {
            Vector3 savedScale = transform.localScale;
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);

            if (transform.localScale.magnitude > savedScale.magnitude)
                Destroy(gameObject);

            if (transform.localScale.magnitude <= 0.5f)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!plungerHit)
        {
            AudioManager.Instance.Play_SFX("Plunger_Arrived_SFX");
            collisionTag = collision.gameObject.tag;

            if(collision.gameObject.tag.Contains("Player") && collision.gameObject != playerShotPlunger)
            {
                otherQuad = collision.gameObject;
                AudioManager.Instance.Play_SFX("Plunger_Arrived_SFX");
            }

            transform.parent = collision.transform;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Death Zone"))
            Destroy(gameObject);
    }
}
