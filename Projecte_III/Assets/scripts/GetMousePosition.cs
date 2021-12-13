using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private ModifiersManager modifiers;
    private bool placed;

    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        placed = false;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            if(!placed)
            {
                //Debug.Log(raycastHit.rigidbody.gameObject.name);
                this.transform.position = raycastHit.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    this.transform.parent = raycastHit.transform;
                    this.transform.position = Vector3.zero;
                    this.transform.localPosition = Vector3.zero;

                    placed = true;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if(this.transform.parent != null)
                    {
                        if(raycastHit.transform == this.transform.parent)
                        {
                            this.transform.parent = null;
                            placed = false;
                        }
                    }
                }
            }
        }
        else
        {
            if(!placed)
                this.transform.position = new Vector3(1000, 0 ,0);
        }
    }
}
