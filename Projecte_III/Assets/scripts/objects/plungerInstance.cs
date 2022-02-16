using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plungerInstance : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    [SerializeField] private float plungerVelocity;
    [SerializeField] private float plungerSpeedLerp;
    private bool plungerHit;
    public GameObject playerShotPlunger;
    Quaternion startRot;
    Quaternion endQuad;

    private void Start()
    {
        startRot = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
        body.velocity = transform.TransformDirection(new Vector3(0, 10, plungerVelocity));
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, playerShotPlunger.transform.position);

        if(plungerHit)
        {
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
        this.transform.parent = collision.transform;
        if (!plungerHit)
        {
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
