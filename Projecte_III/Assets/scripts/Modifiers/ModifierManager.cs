using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModifierManager : MonoBehaviour
{
    private GameObject target;
    QuadControls controls;
    private PlayerStatsManager stats;

    private LayerMask layerMask;

    bool isActive;

    void Start()
    {
        Active(true);
        
        ShowTarget(false);

        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();

        layerMask = LayerMask.GetMask("Modifiers");
        Debug.Log(layerMask.value);

        controls = new QuadControls();
        controls.Enable();
    }

    void Update()
    {
        if (!isActive) return;

        

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Vector3 newPos = ray.origin + ray.direction * (transform.position.z + Mathf.Abs(Camera.main.transform.position.z));

        target.transform.position = newPos;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            target.transform.position = raycastHit.transform.position;
            target.transform.localScale = raycastHit.transform.lossyScale;
            target.transform.rotation = raycastHit.transform.rotation;

            //Place button ------ Left mouse click ------ 
            if(controls.ConstructionMenu.ConstructModifier.ReadValue<float>() > 0)
            {

                if (target.transform.childCount > 0)
                {
                    PlaceModifier(raycastHit.transform);
                }
            }
            //Delete button ------ Right mouse click ------ 
            else if (controls.ConstructionMenu.DeleteModifier.ReadValue<float>() > 0)
            {
                for (int i = 0; i < raycastHit.transform.childCount; i++)
                {
                    raycastHit.transform.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(raycastHit.transform.GetChild(i).gameObject);
                    stats.SetStats();
                }
            }
        }

        if (controls.ConstructionMenu.DeleteModifier.ReadValue<float>() > 0)
        {
            if(target.transform.childCount > 0) Destroy(target.transform.GetChild(0).gameObject);
        }
    }

    public void ShowTarget(bool show)
    {
        if (target.activeSelf != show)
            target.SetActive(show);

        Transform modfs = transform.GetChild(0);
        for (int i = 0; i < modfs.childCount; i++)
        {
            GameObject child = modfs.GetChild(i).gameObject;
            if (child.transform.childCount > 0) continue;
            if (child.activeSelf != show) child.SetActive(show);
        }

        if(!show && target.transform.childCount > 0)
        {
            Destroy(target.transform.GetChild(0).gameObject);
        }
    }

    private void PlaceModifier(Transform spot)
    {
        for (int i = 0; i < spot.childCount; i++)
        {
            Destroy(spot.GetChild(i).gameObject);
        }

        GameObject clone = Instantiate(target.transform.GetChild(0).gameObject, spot);

        spot.GetComponent<MeshRenderer>().enabled = false;

        clone.transform.localScale = clone.transform.parent.parent.localScale;
        clone.transform.localRotation = clone.transform.parent.parent.localRotation;

        clone.transform.position = Vector3.zero;
        clone.transform.localPosition = Vector3.zero;

        stats.SetStats();
    }

    public void ChangeGameObject(GameObject obj)
    {
        if (target.transform.childCount > 0)
        {
            GameObject currentChild = target.transform.GetChild(0).gameObject;
            if (obj.name == currentChild.name)
                return;
            Destroy(currentChild);
        }

        Instantiate(obj, target.transform);
    }

    public void Active(bool active)
    {
        isActive = active;

        if (!active)
            Destroy(target);
        else
        {
            target = Instantiate(new GameObject());
            target.name = "Mouse";
        }
    }
}
