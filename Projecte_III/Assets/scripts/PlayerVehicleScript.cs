using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVehicleScript : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().enabled = false;
        chasisMat = new Material(this.transform.GetChild(0).GetComponent<MeshRenderer>().material);
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;
        Physics.gravity = new Vector3(0, -9.8f * 2, 0);
        vehicleRB = this.GetComponent<Rigidbody>();
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

    /*void CollisionFunction(Collision other)
    {
        if (other != null && !other.gameObject.tag.Equals("ground") && !ignoreCollisionBetweenCoreAndEnemy && !other.gameObject.tag.Equals("Untagged"))
            lifeVehicle = 0;
        if (lifeVehicle <= 0 && !explosion)
        {
            this.GetComponent<Light>().enabled = true;
            Physics.gravity = new Vector3(0, -9.8f, 0);
            this.GetComponent<AudioSource>().enabled = false;
            AudioSource localSound = this.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
            localSound.clip = explodeClip;
            localSound.loop = false;
            localSound.volume = 0.5f;
            localSound.Play();

            this.gameObject.AddComponent<BoxCollider>();
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            this.gameObject.GetComponent<ParticleSystem>().Play();

            for (int i = 0; i < this.transform.childCount; i++)
            {
                GameObject child = this.transform.GetChild(i).gameObject;
                
                if(child.name != "vehicleChasis")
                {
                    for (int j = 0; j < child.transform.childCount; j++)
                    {
                        GameObject grandChild = child.transform.GetChild(j).gameObject;
                        
                        if(grandChild.name.Contains("Block"))
                        {
                            grandChild.GetComponent<BlockScript>().lifeBlock = 0;
                            Physics.IgnoreCollision(enemyTransform.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<BoxCollider>());
                        }
                        else
                        {
                            if (grandChild.GetComponent<BoxCollider>() == null)
                                grandChild.AddComponent<BoxCollider>();
                            grandChild.GetComponent<BoxCollider>().isTrigger = false;
                            if (grandChild.GetComponent<Rigidbody>() == null)
                                grandChild.AddComponent<Rigidbody>();
                            Physics.IgnoreCollision(enemyTransform.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<BoxCollider>());
                            Physics.IgnoreCollision(enemyTransform.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<WheelCollider>());
                            grandChild.GetComponent<Rigidbody>().AddExplosionForce(100, this.transform.position, 50, 50, ForceMode.Force);
                        }
                    }

                    for (int o = 0; o < child.transform.childCount; o++)
                    {
                        child.transform.GetChild(o).parent = null;
                    }
                }
                else
                    Physics.IgnoreCollision(enemyTransform.GetChild(0).GetComponent<BoxCollider>(), child.GetComponent<BoxCollider>());
            }
            explosion = true;
        }
    }*/

    void OnCollisionExit(Collision other)
    {
        vehicleReversed = false;
    }
}
