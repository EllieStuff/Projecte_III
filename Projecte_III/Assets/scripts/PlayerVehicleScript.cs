using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVehicleScript : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass = new Vector3(0.0f, -0.7f, 0.0f);

    public Rigidbody vehicleRB;
    public float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    public WheelCollider[] wheelCollider;
    public GameObject[] wheels;
    public int lifeVehicle;
    public bool touchingGround;
    public bool vehicleReversed;
    private Material chasisMat;
    private Vector3 savedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().enabled = false;
        chasisMat = new Material(this.transform.GetChild(0).GetComponent<MeshRenderer>().material);
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = this.GetComponent<Rigidbody>();
        vehicleRB.centerOfMass = centerOfMass;
    }

    private void Awake()
    {
        this.transform.name = "Player";
    }

    void Update()
    {
        touchingGround = false;

        for (int i = 0; i < wheelCollider.Length; i++)
        {
            if (wheelCollider[i].GetGroundHit(out var touchingGroundV))
            {
                touchingGround = true;
            }
            wheelCollider[i].GetWorldPose(out var pos, out var rot);
            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = rot;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleMovement();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("ground"))
            vehicleReversed = true;
    }

    void vehicleMovement()
    {
        var locVel = transform.InverseTransformDirection(vehicleRB.velocity);
        if (touchingGround && lifeVehicle > 0)
        {
            //FORWARD
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, vehicleAcceleration));
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }

            //LEFT
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }
            //RIGHT
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }

            //BACKWARDS
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                vehicleRB.AddTorque(new Vector3(0, vehicleTorque, 0));
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
            {
                vehicleRB.velocity += transform.TransformDirection(new Vector3(0, 0, -vehicleAcceleration));
                vehicleRB.AddTorque(new Vector3(0, -vehicleTorque, 0));
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, 0, vehicleRB.angularVelocity.z);

            if (vehicleRB.angularVelocity.y > vehicleMaxTorque)
            {
                vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, vehicleMaxTorque, vehicleRB.angularVelocity.z);
            }
            else if (vehicleRB.angularVelocity.y < -vehicleMaxTorque)
            {
                vehicleRB.angularVelocity = new Vector3(vehicleRB.angularVelocity.x, -vehicleMaxTorque, vehicleRB.angularVelocity.z);
            }
            if (vehicleRB.velocity.z > vehicleMaxSpeed || vehicleRB.velocity.x > vehicleMaxSpeed)
            {
                if (vehicleRB.velocity.x > vehicleMaxSpeed && vehicleRB.velocity.z < vehicleMaxSpeed)
                {
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
                    if (Input.GetKey(KeyCode.S) && vehicleRB.velocity.x < 0)
                        vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                }
                if (vehicleRB.velocity.z > vehicleMaxSpeed)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
                if (Input.GetKey(KeyCode.S) && vehicleRB.velocity.z < 0)
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
            }
            else if (vehicleRB.velocity.z < -vehicleMaxSpeed || vehicleRB.velocity.x < -vehicleMaxSpeed)
            {
                if (!Input.GetKey(KeyCode.S))
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, vehicleMaxSpeed));
                else
                {
                    vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                    if (vehicleRB.velocity.z < 0)
                        vehicleRB.velocity = transform.TransformDirection(new Vector3(0, 0, -vehicleMaxSpeed));
                }
            }
            savedVelocity = vehicleRB.velocity;
        }
        else if (vehicleReversed && lifeVehicle > 0)
        {
            //LEFT
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                // if(vehicleRB.angularVelocity.z * Mathf.Rad2Deg > -10)
                vehicleRB.AddTorque(new Vector3(0, 0, -vehicleTorque));
            }
            //RIGHT
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                //if (vehicleRB.angularVelocity.z * Mathf.Rad2Deg < 10)
                vehicleRB.AddTorque(new Vector3(0, 0, vehicleTorque));
            }
        }
        else
        {
            if (savedVelocity.x > 0)
                savedVelocity -= new Vector3(0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f), 0, 0);
            else if (savedVelocity.x < 0)
                savedVelocity += new Vector3(0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f), 0, 0);
            if (savedVelocity.z > 0)
                savedVelocity -= new Vector3(0, 0, 0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f));
            else if (savedVelocity.z < 0)
                savedVelocity += new Vector3(0, 0, 0.1f + Mathf.Clamp(vehicleRB.velocity.y, 0, 0.2f));

            if (vehicleRB.velocity.y >= 0)
                vehicleRB.velocity = new Vector3(savedVelocity.x, vehicleRB.velocity.y, savedVelocity.z);
            else
                vehicleRB.velocity = new Vector3(savedVelocity.x, vehicleRB.velocity.y - 0.5f, savedVelocity.z);
        }

        if (lifeVehicle <= 0 && Input.GetKey(KeyCode.Backspace))
            SceneManager.LoadScene("testScene");

        if ((vehicleRB.velocity.magnitude > 1 || vehicleRB.velocity.magnitude < -1) && !this.GetComponent<AudioSource>().enabled && lifeVehicle > 0)
        {
            this.GetComponent<AudioSource>().enabled = true;
        }
        else if ((vehicleRB.velocity.magnitude <= 1 && vehicleRB.velocity.magnitude >= -1) && this.GetComponent<AudioSource>().enabled && lifeVehicle > 0)
            this.GetComponent<AudioSource>().enabled = false;

        if (this.GetComponent<AudioSource>().enabled)
        {
            this.GetComponent<AudioSource>().pitch = (vehicleRB.velocity.magnitude * 2) / vehicleMaxSpeed;
        }
    }

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
    }
}
