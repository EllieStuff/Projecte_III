using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpots : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            float distance = Vector3.Distance(raycastHit.point, this.transform.position);

            Debug.Log(distance);        
        }
    }
}
