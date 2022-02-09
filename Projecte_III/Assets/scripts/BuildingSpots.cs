using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BuildingSpots : MonoBehaviour
{
    private ModifiersManager modifiers;
    [SerializeField] private bool placed;

    [SerializeField] private LayerMask layerMask;

    public Camera playerCamP1;
    public Camera playerCamP2;

    QuadControls controls;

   [SerializeField] private bool multiplayerMode;

    private void Awake()
    {
        placed = false;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        controls = new QuadControls();
        controls.Enable();
        modifiers = QuadSceneManager.Instance.GetComponentInChildren<ModifiersManager>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(1,1,1);

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(multiplayerMode && SceneManager.GetActiveScene().name.Equals("Building Scene Multiplayer"))
        {
            if(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).GetPoint(0).x < 0)
                ray = playerCamP1.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue() + new Vector2(500, 0));
            else if (Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).GetPoint(0).x >= 0)
                ray = playerCamP2.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue() - new Vector2(500, 0));
        }

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            
            if (!placed)
            {

                transform.position = raycastHit.transform.position;
                transform.localScale = raycastHit.transform.lossyScale;
                transform.rotation = raycastHit.transform.rotation;


                if (controls.ConstructionMenu.ConstructModifier.ReadValue<float>() > 0 && raycastHit.transform.childCount == 0)    //Instantiate Object
                {
                    GameObject clone = Instantiate(gameObject, raycastHit.transform).gameObject;
                    if (clone.transform.GetComponentInChildren<MeshCollider>() != null)
                        clone.transform.GetComponentInChildren<MeshCollider>().enabled = false;

                    raycastHit.transform.GetComponent<MeshRenderer>().enabled = false;

                    clone.transform.localScale = clone.transform.parent.parent.localScale;
                    clone.transform.localRotation = clone.transform.parent.parent.localRotation;

                    clone.transform.position = Vector3.zero;
                    clone.transform.localPosition = Vector3.zero;

                    clone.GetComponent<BuildingSpots>().SetPlaced();
                }
            }
            else
            {
                if (controls.ConstructionMenu.DeleteModifier.ReadValue<float>() > 0)                                            //Remove Object
                {
                    if(transform.parent != null)
                    {
                        if(raycastHit.transform == transform.parent)
                        {
                            raycastHit.transform.GetComponent<MeshRenderer>().enabled = true;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            if(!placed)
            {
                Vector3 newPos = ray.origin + ray.direction * (modifiers.transform.position.z + Mathf.Abs(Camera.main.transform.position.z));
                //newPos.z = modifiers.transform.parent.transform.localPosition.z;

                //Debug.Log(Camera.main.nearClipPlane);
                transform.position = newPos;
            }
        }
        Debug.DrawLine(ray.origin, transform.position, Color.red);
    }

    public void SetPlaced()
    {
        placed = true;
    }

    public void ChangeGameObject(GameObject obj)
    {
        if(transform.childCount > 0)
        {
            GameObject currentChild = transform.GetChild(0).gameObject;
            if (obj.name == currentChild.name)
            {
                return;
            }
            Destroy(currentChild);
        }
        
        Instantiate(obj, transform);
    }
}
