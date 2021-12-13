using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public int blockMaxLife;
    public float lifeBlock;
    private Rigidbody blockRB;
    private Vector3 localPos;
    private bool explosion;
    private float timerExplosion = 5;
    private Material mat;
    public Material savedMat;
    [SerializeField] private GameObject hitSoundInstance;

    public bool touchingAnother;

    public bool playerMode;

    private Vector3 chasisInitPosition;

    private Vector3 blockSize;

    public Vector3 depthVector;

    [SerializeField] private bool blockPlaced;

    [SerializeField] private bool editorModeActive;

    public GameObject buildingScriptGameObject;

    void Start()
    {
        this.GetComponent<MeshRenderer>().material = new Material(savedMat);

        if (blockPlaced)
        {
            if(this.transform.GetChild(0).gameObject.active)
            this.transform.GetChild(0).gameObject.SetActive(false);
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.parent.GetChild(0).GetComponent<BoxCollider>());
            for (int i = 0; i < this.transform.parent.childCount; i++)
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.GetChild(i).GetComponent<BoxCollider>());
            }

            for (int i = 0; i < this.transform.parent.parent.GetChild(1).childCount; i++)
            {
                Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.parent.GetChild(1).GetChild(i).GetComponent<WheelCollider>());
            }
        }
        else
            this.transform.GetChild(0).gameObject.SetActive(true);

        explosion = false;
        localPos = this.transform.localPosition;
        lifeBlock = blockMaxLife;
        blockRB = this.GetComponent<Rigidbody>();
        mat = new Material(this.GetComponent<MeshRenderer>().material);
        this.GetComponent<MeshRenderer>().material = mat;

        blockSize = this.GetComponent<Renderer>().bounds.size;


        chasisInitPosition = GameObject.Find("Player").GetComponent<PlayerVehicleScript>().GetEditorInitialChasisPos();

        if(!blockPlaced)
        EditorStart();
    }

    void Update() 
    {
        if(!editorModeActive)
        {
            BattleUpdate();
        }
        else
        {
            EditorUpdate();
        }
    }

    public void EditorStart()
    {
        editorModeActive = true;
    }

    public void BattleStart()
    {
        editorModeActive = false;
    }

    public void EditorUpdate()
    {
        blockRB.velocity = Vector3.zero;
        blockRB.angularVelocity = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Backspace))
            editorModeActive = false;

        if(!blockPlaced)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0)
                depthVector += new Vector3(0, 0, 1);
            else if(Input.GetAxis("Mouse ScrollWheel") < 0)
                depthVector += new Vector3(0, 0, -1);

            buildingScriptGameObject.GetComponent<BuildingScript>().depthVector = depthVector;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 15;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 roundedVector = mousePos;

            roundedVector.x = blockSize.x * (int)(Mathf.Sqrt(Mathf.Pow(chasisInitPosition.x - mousePos.x, 2)));
            if (chasisInitPosition.x - mousePos.x > 0)
                roundedVector.x *= -1;
            roundedVector.y = blockSize.y * (int)(Mathf.Sqrt(Mathf.Pow(chasisInitPosition.y - mousePos.y, 2)));
            if (chasisInitPosition.y - mousePos.y > 0)
                roundedVector.y *= -1;

            this.transform.position = chasisInitPosition + roundedVector + depthVector;

            Color c = mat.color;
            c.r = .5f;
            mat.color = c;

            this.transform.rotation = this.transform.parent.rotation;

            this.transform.localPosition = new Vector3(Mathf.Round(this.transform.localPosition.x), Mathf.Round(this.transform.localPosition.y), Mathf.Round(this.transform.localPosition.z));

            localPos = this.transform.localPosition;
        }
        else
        {
            blockRB.velocity = Vector3.zero;
            blockRB.angularVelocity = Vector3.zero;
            if (this.transform.parent != null)
                this.transform.rotation = this.transform.parent.parent.rotation;
            else
                this.transform.rotation = Quaternion.identity;
            this.transform.localPosition = localPos;
        }
    }

    public void BattleUpdate()
    {
        if (lifeBlock <= 0 && !explosion)
        {
            if (playerMode)
                GameObject.Find("Player").GetComponent<PlayerVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = false;
            else
                GameObject.Find("Enemy").GetComponent<EnemyVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = false;

            this.GetComponent<Light>().enabled = true;
            this.transform.parent = null;
            blockRB.useGravity = true;
            this.GetComponent<ParticleSystem>().Play();
            this.GetComponent<AudioSource>().Play();
            blockRB.AddExplosionForce(50, blockRB.transform.position, 100);
            explosion = true;
        }
        else if (!explosion)
        {
            //blockRB.velocity = Vector3.zero;
            //blockRB.angularVelocity = Vector3.zero;
            if (this.transform.parent != null)
                this.transform.rotation = blockRB.transform.parent.rotation;
            this.transform.localPosition = localPos;
        }
        else
        {
            timerExplosion -= Time.deltaTime;
            if (timerExplosion <= 0)
                Destroy(this.gameObject);
        }
    }

    public void DestroyBlock()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (!editorModeActive)
        Instantiate(hitSoundInstance, this.transform.position, this.transform.rotation);
        if (!editorModeActive && other.gameObject.tag.Equals("vehicleElement"))
        {
            lifeBlock -= (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 10) / 10;
        }
        if(other.gameObject.name.Equals("flareBullet(Clone)"))
        {
            lifeBlock -= (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 20) / 10;
            Destroy(other.gameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (Input.GetKey(KeyCode.Mouse1) && !blockPlaced && other.transform.parent != null && other.transform.parent.name.Contains("Block"))
            Destroy(other.gameObject);

        if (editorModeActive)
            touchingAnother = true;
        if(this.transform.parent != null && !other.gameObject.tag.Equals("ground") && other.gameObject.name.Contains("Block"))
        {
            if (playerMode)
                this.transform.parent.parent.GetComponent<PlayerVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = true;
            else
                this.transform.parent.parent.GetComponent<EnemyVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = true;

            this.transform.parent.parent.GetComponent<Rigidbody>().velocity += blockRB.velocity;
            this.transform.parent.parent.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(this.transform.parent.parent.GetComponent<Rigidbody>().velocity.x, -50, 50), Mathf.Clamp(this.transform.parent.parent.GetComponent<Rigidbody>().velocity.y, -50, 50), Mathf.Clamp(this.transform.parent.parent.GetComponent<Rigidbody>().velocity.z, -50, 50));

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (editorModeActive)
            touchingAnother = false;
        if(this.transform.parent != null && collision.gameObject.name.Contains("Block"))
        {
            if (playerMode)
                this.transform.parent.parent.GetComponent<PlayerVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = false;
            else
                this.transform.parent.parent.GetComponent<EnemyVehicleScript>().ignoreCollisionBetweenCoreAndEnemy = false;
        }
    }

    public void BlockPlaced(bool value)
    {
        Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.parent.GetChild(0).GetComponent<BoxCollider>());
        for (int i = 0; i < this.transform.parent.childCount; i++)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.GetChild(i).GetComponent<BoxCollider>());
        }

        for (int i = 0; i < this.transform.parent.parent.GetChild(1).childCount; i++)
        {
            Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), this.transform.parent.parent.GetChild(1).GetChild(i).GetComponent<WheelCollider>());
        }
        blockPlaced = value;
    }
}
