using UnityEngine;
using UnityEngine.InputSystem;

public class ModifierManager : MonoBehaviour
{
    private GameObject target;
    QuadControls controls;
    private PlayerStatsManager stats;
    private StatsSliderManager statsSliders;

    private GameObject player;

    private LayerMask layerMask;

    void Start()
    {
        Active(true);
        
        ShowTarget(false);

        player = GameObject.FindGameObjectWithTag("Player");

        stats = player.GetComponent<PlayerStatsManager>();

        layerMask = LayerMask.GetMask("Modifiers");

        controls = new QuadControls();
        controls.Enable();

        statsSliders = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsSliderManager>();
    }

    void Update()
    {
        Stats.Data playerStats = stats.transform.GetComponent<Stats>().GetStats();
        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().GetSceneName() != "Building Scene" && (target == null || !target.activeSelf))
        {
            Transform chasis = player.transform.parent.GetChild(0);

            Quaternion playerEuler = chasis.GetChild(chasis.childCount - 1).rotation;

            transform.rotation = playerEuler;

            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Vector3 newPos = ray.origin + ray.direction * (transform.position.z + Mathf.Abs(Camera.main.transform.position.z));

        target.transform.position = newPos;

        SetNewValues(playerStats);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            
            target.transform.position = raycastHit.transform.position;
            target.transform.localScale = raycastHit.transform.lossyScale;
            target.transform.rotation = raycastHit.transform.rotation;
            
            //Place button ------ Left mouse click ------ 
            if(controls.ConstructionMenu.ConstructModifier.ReadValue<float>() > 0)
            {
                if (target.transform.childCount > 0 && raycastHit.transform.GetComponent<ModifierSpotData>().IsAvailable(target.transform.GetChild(0).gameObject.tag))
                {
                    PlaceModifier(raycastHit.transform);

                    stats.SetStats();
                    SetNewValues(stats.transform.GetComponent<Stats>().GetStats(), true);

                    return;
                }
            }
            //Delete button ------ Right mouse click ------ 
            else if (controls.ConstructionMenu.DeleteModifier.ReadValue<float>() > 0)
            {
                for (int i = 0; i < raycastHit.transform.childCount; i++)
                {
                    raycastHit.transform.GetComponent<MeshRenderer>().enabled = true;

                    Destroy(raycastHit.transform.GetChild(i).gameObject);
                }
                stats.SetStats();
                SetNewValues(stats.transform.GetComponent<Stats>().GetStats(), true);
                return;
            }

            if(target.transform.childCount > 0 && 
                (raycastHit.transform.childCount == 0 ||
                (raycastHit.transform.childCount > 0 && 
                target.transform.GetChild(0).tag != raycastHit.transform.GetChild(0).tag)))
            {
                SetNewValues(playerStats + target.transform.GetComponentInChildren<Stats>().GetStats());
            }
                
        }

        if (controls.ConstructionMenu.DeleteModifier.ReadValue<float>() > 0)
        {
            if (target.transform.childCount > 0)
            {
                Destroy(target.transform.GetChild(0).gameObject);
            }
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
        if (clone.GetComponent<MeshCollider>() != null) clone.GetComponent<MeshCollider>().enabled = false;

        spot.GetComponent<MeshRenderer>().enabled = false;

        clone.transform.localScale = clone.transform.parent.parent.localScale;

        clone.transform.position = Vector3.zero;
        clone.transform.localPosition = Vector3.zero;

        stats.SetStats();

        SetNewValues(stats.transform.GetComponent<Stats>().GetStats(), true);
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

    public void SetNewModifierSpots(Transform newModfs)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        newModfs.parent = transform;
        newModfs.localPosition = transform.localPosition;
        newModfs.localScale = transform.localScale;
        newModfs.localRotation = transform.localRotation;
    }

    public void Active(bool active)
    {
        if (!active)
            Destroy(target);
        else
        {
            target = Instantiate(new GameObject());
            target.name = "Mouse";
        }
    }


    private void SetNewValues(Stats.Data _stats, bool placed = false)
    {
        statsSliders.SetSliderValue(_stats, placed);
    }
}
