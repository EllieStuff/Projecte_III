using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpots : MonoBehaviour
{
    [SerializeField] private ModifiersManager modifiers;
    [SerializeField] private bool placed;

    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        placed = false;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            
            if (!placed)
            {
                this.transform.position = raycastHit.transform.position;

                if (Input.GetMouseButtonDown(0) && raycastHit.transform.childCount == 0)    //Instantiate Object
                {
                    GameObject clone = GameObject.Instantiate(this, raycastHit.transform).gameObject;

                    clone.transform.position = Vector3.zero;
                    clone.transform.localPosition = Vector3.zero;

                    clone.GetComponent<BuildingSpots>().SetPlaced();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))                                            //Remove Object
                {
                    if(this.transform.parent != null)
                    {
                        if(raycastHit.transform == this.transform.parent)
                        {
                            Destroy(this.gameObject);
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

                Debug.Log(Camera.main.nearClipPlane);
                this.transform.position = newPos;
            }
        }
        Debug.DrawLine(ray.origin, this.transform.position, Color.red);
    }

    public void SetPlaced()
    {
        placed = true;
    }
}
