using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject playerVehicle;
    public GameObject battleUI;
    public float camSpeed;
    private Vector3 editorRotation;
    private Vector3 editorDepth;
    private Quaternion initialRot;
    [SerializeField] private GameObject BattleControls;

    private bool editorModeActive;

    [SerializeField] private GameObject editorUI;

    private GameObject editorTerrain;
    [SerializeField] private Material terrainMat;

    private void Start()
    {
        editorTerrain = GameObject.Find("terrainEditorPivot").transform.GetChild(0).gameObject;
        editorTerrain.GetComponent<MeshRenderer>().material = new Material(terrainMat);
        editorTerrain.GetComponent<MeshRenderer>().material.color = new Color(editorTerrain.GetComponent<MeshRenderer>().material.color.r, editorTerrain.GetComponent<MeshRenderer>().material.color.g, editorTerrain.GetComponent<MeshRenderer>().material.color.b, 0.2f);
        initialRot = this.transform.rotation;
        editorModeActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (editorModeActive && !editorUI.active)
        {
            battleUI.SetActive(false);
            editorUI.SetActive(true);
            editorTerrain.SetActive(true);
        }
        else if(!editorModeActive && editorUI.active)
        {
            editorUI.SetActive(false);
            editorTerrain.SetActive(false);
            battleUI.SetActive(true);
        }

        if (playerVehicle.transform.parent != null && playerVehicle.transform.parent.GetComponent<PlayerVehicleScript>().lifeVehicle > 0 && !editorModeActive)
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(playerVehicle.transform.position.x , playerVehicle.transform.position.y + 2, playerVehicle.transform.position.z), Time.deltaTime * camSpeed);
        if(!editorModeActive)
        {
            if(editorRotation != Vector3.zero)
            {
                editorRotation = Vector3.zero;
                this.transform.rotation = Quaternion.Euler(editorRotation);
            }
            if(Input.GetKey(KeyCode.Tab))
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w) * Quaternion.Euler(new Vector3(0, 180, 0)), Time.deltaTime * camSpeed);
            else
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, playerVehicle.transform.rotation.y, this.transform.rotation.z, playerVehicle.transform.rotation.w), Time.deltaTime * camSpeed);
        }
        else
        {
            if (Time.timeScale < 1)
                Time.timeScale = 1;

            bool input = false;

            if (Input.GetKey(KeyCode.F))
                editorRotation = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && Vector3.Distance(this.transform.position, playerVehicle.transform.position) >= 0.001f)
            {
                //editorRotation = Vector3.zero;
                editorDepth += this.transform.TransformDirection(new Vector3(0, 0, 0.2f));
                input = true;
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
            {
                editorRotation += Vector3.right * 1;
                if (editorRotation.x > -90)
                    editorRotation = new Vector3(editorRotation.x, 0, 0);
                else
                    editorRotation = new Vector3(editorRotation.x, 0, 180);
                input = true;
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && Vector3.Distance(this.transform.position, playerVehicle.transform.position) <= 20)
            {
                //editorRotation = Vector3.zero;
                editorDepth -= this.transform.TransformDirection(new Vector3(0, 0, 0.2f));
                input = true;
            }
            else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
            {
                editorRotation += Vector3.left * 1;
                if(editorRotation.x > -90)
                    editorRotation = new Vector3(editorRotation.x, 0, 0);
                else
                    editorRotation = new Vector3(editorRotation.x, 0, 180);
                input = true;
            }
            if (Input.GetKey(KeyCode.D))
            { 
                editorRotation += Vector3.up * 1;
                editorRotation = new Vector3(0, editorRotation.y, 0);
                input = true;
            }
            if (Input.GetKey(KeyCode.A))
            { 
                editorRotation += Vector3.down * 1;
                editorRotation = new Vector3(0, editorRotation.y, 0);
                input = true;
            }

            if (editorRotation.x < -360)
                editorRotation.x += 360;
            else if (editorRotation.x > 360)
                editorRotation.x -= 360;

            if (editorRotation.y < -360)
                editorRotation.y += 360;
            else if (editorRotation.y > 360)
                editorRotation.y -= 360; 

            if (editorRotation.z < -360)
                editorRotation.z += 360;
            else if (editorRotation.z > 360)
                editorRotation.z -= 360;

            this.transform.rotation = initialRot;
            this.transform.position = new Vector3(playerVehicle.transform.position.x, playerVehicle.transform.position.y, playerVehicle.transform.position.z) + editorDepth;
            playerVehicle.transform.parent.rotation = Quaternion.Euler(editorRotation);
            editorTerrain.transform.parent.rotation = Quaternion.Euler(editorRotation);
        }
    }

    public void enableOrDisableControls(GameObject UIControls)
    {
        if (UIControls.active)
        {
            UIControls.SetActive(false);
            if(!editorModeActive)
                Time.timeScale = 1;
        }
        else if (!UIControls.active)
        {
            UIControls.SetActive(true);
            if (!editorModeActive)
                Time.timeScale = 0;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!editorModeActive)
                Time.timeScale = 1;
            UIControls.SetActive(false);
        }
    }
    public void ChangeMode()
    {
        BattleControls.SetActive(false);
        editorModeActive = !editorModeActive;
    }
}
