using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyVehicleScript : MonoBehaviour
{
    public Rigidbody vehicleRB;
    public int rotationSpeed;
    public float vehicleAcceleration;
    public float vehicleTorque;
    public float vehicleMaxSpeed;
    public float vehicleMaxTorque;
    public WheelCollider[] wheelCollider;
    public GameObject[] wheels;
    public int lifeVehicle;
    public bool touchingGround;
    public bool vehicleReversed;
    public bool explosion;
    public bool ignoreCollisionBetweenCoreAndEnemy;
    public AudioClip explodeClip;
    private Material chasisMat;
    private GameObject player;

    private Quaternion initialRotation;

    private Transform playerPos;

    [SerializeField] private bool editorModeActive;

    // Start is called before the first frame update
    void Start()
    {
        chasisMat = new Material(this.transform.GetChild(0).GetComponent<MeshRenderer>().material);
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material = chasisMat;
        chasisMat.color = Color.red;

        playerPos = GameObject.Find("Player").transform;
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        vehicleRB = this.GetComponent<Rigidbody>();

        initialRotation = this.transform.rotation;

        EditorStart();
    }

    private void Awake()
    {
        this.transform.name = "Enemy";
    }

    void Update()
    {
        if (!editorModeActive)
            BattleUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!editorModeActive)
            BattleFixedUpdate();
    }

    public void BattleStart()
    {
        vehicleRB.useGravity = true;

        this.transform.position = new Vector3(0,0,37);
        this.transform.rotation = initialRotation;

        editorModeActive = false;
    }

    public void EditorStart()
    {
        vehicleRB.useGravity = false;

        vehicleRB.velocity = Vector3.zero;
        vehicleRB.angularVelocity = Vector3.zero;

        editorModeActive = true;
    }

    void BattleUpdate()
    {
        if(lifeVehicle <= 0 && Input.GetKeyDown(KeyCode.Backspace)) 
        {
            GameObject playerAux = GameObject.Find("Player");
            if(playerAux.GetComponent<PlayerVehicleScript>().lifeVehicle > 0)
                PrefabUtility.SaveAsPrefabAsset(playerAux, "Assets/prefabs/vehicleTest.prefab");
            SceneManager.LoadScene("testScene");
        }

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
        if (Vector3.Distance(this.transform.position, new Vector3(0, -15, 0)) > 150)
        {
            lifeVehicle = 0;
            BattleCollision(null);
        }
    }

    void BattleFixedUpdate()
    {
        if (lifeVehicle > 0)
        {
            if(!ignoreCollisionBetweenCoreAndEnemy)
            {
                //--------Velocity Direction--------//
                Vector3 vectorBetweenPlayerAndEnemy = playerPos.position - this.transform.position;
                vectorBetweenPlayerAndEnemy = Vector3.Normalize(vectorBetweenPlayerAndEnemy);
                Vector3 finalForce = Vector3.Normalize(Vector3.Lerp(transform.forward, vectorBetweenPlayerAndEnemy, Time.deltaTime * vehicleMaxSpeed));
                finalForce *= vehicleMaxSpeed;

                if (!touchingGround)
                    finalForce = new Vector3(finalForce.x, -9.81f, finalForce.z);
                else
                    finalForce = new Vector3(finalForce.x, 0, finalForce.z);

                if (touchingGround)
                    vehicleRB.velocity = finalForce;


                //--------Rotation--------//
                if (touchingGround)
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(finalForce), Time.deltaTime * rotationSpeed);
            }
        }
        else
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionStay(Collision other)
    {
        if (!editorModeActive && other.gameObject.tag.Equals("ground"))
            vehicleReversed = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!editorModeActive)
            BattleCollision(other);
    }

    void BattleCollision(Collision other)
    {
        if (other != null && !other.gameObject.tag.Equals("ground") && !ignoreCollisionBetweenCoreAndEnemy)
            lifeVehicle = 0;
        if (lifeVehicle <= 0 && !explosion)
        {
            this.GetComponent<Light>().enabled = true;
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

                if (child.name != "vehicleChasis")
                {
                    for (int j = 0; j < child.transform.childCount; j++)
                    {
                        GameObject grandChild = child.transform.GetChild(j).gameObject;

                        if (grandChild.name.Contains("block"))
                        {
                            grandChild.GetComponent<BlockScript>().lifeBlock = 0;
                            Physics.IgnoreCollision(playerPos.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<BoxCollider>());
                        }
                        else
                        {
                            if (grandChild.GetComponent<BoxCollider>() == null)
                                grandChild.AddComponent<BoxCollider>();
                            grandChild.GetComponent<BoxCollider>().isTrigger = false;
                            if (grandChild.GetComponent<Rigidbody>() == null)
                                grandChild.AddComponent<Rigidbody>();
                            Physics.IgnoreCollision(playerPos.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<BoxCollider>());
                            Physics.IgnoreCollision(playerPos.GetChild(0).GetComponent<BoxCollider>(), grandChild.GetComponent<WheelCollider>());
                            grandChild.GetComponent<Rigidbody>().AddExplosionForce(100, this.transform.position, 50, 50, ForceMode.Force);
                        }
                    }

                    for (int o = 0; o < child.transform.childCount; o++)
                    {
                        child.transform.GetChild(o).parent = null;
                    }
                }
                else
                {
                    Physics.IgnoreCollision(playerPos.GetChild(0).GetComponent<BoxCollider>(), child.GetComponent<BoxCollider>());
                }
            }

            explosion = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (!editorModeActive)
            vehicleReversed = false;
    }

    public void changeVehicleBlockResistance(Slider slider)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;

            if (child.name != "vehicleChasis")
            {
                for (int j = 0; j < child.transform.childCount; j++)
                {
                    GameObject grandChild = child.transform.GetChild(j).gameObject;

                    if (grandChild.name.Contains("block"))
                    {
                        grandChild.GetComponent<BlockScript>().lifeBlock = slider.value;
                    }
                }
            }
        }
    }

    public void changeVehicleSpeed(Slider slider)
    {
        vehicleMaxSpeed = slider.value;
    }
}
