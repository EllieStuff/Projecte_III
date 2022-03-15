using UnityEngine;
using UnityEngine.InputSystem;

public class ModifierManager : MonoBehaviour
{
    private GameObject target = null;
    QuadControls controls;
    private PlayerStatsManager stats;
    private StatsSliderManager statsSliders;

    private PlayersManager playersManager;
    private PlayerInputs inputs;
    private Camera usedCamera;
    private Transform rendererCamera;
    private GameObject player;
    private int playerId;

    private LayerMask layerMask;
    private bool controllerInited = false;

    void Start()
    {
        //PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        player = transform.parent.GetComponentInChildren<PlayerVehicleScript>().gameObject;

        stats = player.GetComponent<PlayerStatsManager>();

        layerMask = LayerMask.GetMask("Modifiers");

        controls = new QuadControls();
        controls.Enable();

        /// Todo
        //   - Canviar getcomponents amb el playerManager
        //   - Adaptar funcionament ratoli segons id del jugador que l'utilitzi
        playersManager = transform.parent.GetComponentInParent<PlayersManager>();
        playerId = transform.GetComponentInParent<QuadSceneManager>().playerId;
        inputs = playersManager.GetPlayer(playerId).GetComponent<PlayerInputs>();
        usedCamera = GameObject.FindGameObjectWithTag("CamerasManager").GetComponent<CameraManager>().GetCamera(playerId);
        rendererCamera = GameObject.FindGameObjectWithTag("CamerasManager").GetComponent<CameraManager>().GetRendererCamera(playerId);
        statsSliders = GameObject.FindGameObjectWithTag("StatsManager").GetComponent<StatsManager>().GetPlayerStats(playerId);

    }

    void Update()
    {
        if(!controllerInited && inputs.ControlData[0] != null)
        {
            controllerInited = true;
            Active(true);
            ShowTarget(false);
        }
        else if (controllerInited)
        {
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        Stats.Data playerStats = stats.transform.GetComponent<Stats>().GetStats();

        if (GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().GetSceneName() != "Building Scene")
        {
            Transform chasis = player.transform.parent.GetChild(0);

            Quaternion playerEuler = chasis.GetChild(chasis.childCount - 1).rotation;

            transform.rotation = playerEuler;

            return;
        }
        else if (target == null || !target.activeSelf) return;
        Vector3 newPos = Vector3.zero;
        Ray ray = new Ray();
        if (playersManager.gameMode == PlayersManager.GameModes.MONO)
        {
            ray = usedCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            newPos = ray.origin + ray.direction * (transform.position.z + Mathf.Abs(usedCamera.transform.position.z));
        }
        else if (playersManager.gameMode == PlayersManager.GameModes.MULTI_LOCAL)
        {
            Debug.Log(playerId + " has " + inputs.ControlData[0].deviceType.ToString());
            if (inputs.ControlData[0].deviceType == InputSystem.DeviceTypes.KEYBOARD)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();// * 2.0f;
                //mousePos.y -= Screen.height;
                //mousePos.z = usedCamera.transform.position.z;
                // ToDo: Trobar les distancies entre quad i sumar-les, potser agafar distancies entre initPoints pot ser bona idea
                //float quadDistances = Vector3.Distance(playersManager.GetPlayer(0).position, playersManager.GetPlayer(playerId).position);
                //mousePos.x += quadDistances;
                Vector3 rendererMousePos = mousePos - rendererCamera.position;
                rendererMousePos *= 2;
                rendererMousePos.z = 0;
                if(playerId == 0 || playerId == 2)
                    rendererMousePos.x += Screen.width;
                if (playerId == 2 || playerId == 3)
                    rendererMousePos.y += Screen.height * 1.25f;

                ray = usedCamera.ScreenPointToRay(rendererMousePos);

                newPos = ray.origin + ray.direction * (transform.position.z + Mathf.Abs(usedCamera.transform.position.z));
            }
        }
        target.transform.position = newPos;

        SetNewValues(playerStats);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {

            target.transform.position = raycastHit.transform.position;
            target.transform.localScale = raycastHit.transform.lossyScale;
            target.transform.rotation = raycastHit.transform.rotation;

            //Place button ------ Left mouse click ------ 
            if (controls.ConstructionMenu.ConstructModifier.ReadValue<float>() > 0)
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

            if (target.transform.childCount > 0 &&
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
        if (target != null)
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

            if (!show && target.transform.childCount > 0)
            {
                Destroy(target.transform.GetChild(0).gameObject);
            }
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
        if (target != null && target.transform.childCount > 0)
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
            if (inputs.ControlData[0].deviceType == InputSystem.DeviceTypes.KEYBOARD)
            {
                target.name = "Mouse";
            }
            else if (inputs.ControlData[0].deviceType == InputSystem.DeviceTypes.CONTROLLER)
            {
                target.name = "Controller";
            }
        }
    }


    private void SetNewValues(Stats.Data _stats, bool placed = false)
    {
        statsSliders.SetSliderValue(_stats, placed);
    }
}
