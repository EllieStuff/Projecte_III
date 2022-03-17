using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowPlunger : MonoBehaviour
{
    private Transform localTransform;
    private Vector3 savedDirection;
    private int plungerBaseDisappearCooldown = 5;
    private float plungerDisappearCooldown;
    private float timerPoint;
    private bool createMaterial;
    private bool plunger;
    private GameObject plungerInstance;
    [SerializeField] private GameObject plungerPrefab;
    private PlayerVehicleScript player;
    bool plungerEnabled = false;
    bool hasPlunger;
    Transform modifierTransform;
    public LineRenderer line;

    public void Init(Transform _modifier, bool _active)
    {
        hasPlunger = _active;
        modifierTransform = transform;
    }

    public void Activate()
    {
        if (hasPlunger)
            plungerEnabled = true;
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        player = GetComponent<PlayerVehicleScript>();
    }

    private void Update()
    {
        if (hasPlunger)
        {
            CheckPlungerThrow();
            PlungerUpdate();
        }
    }

    private void CheckPlungerThrow()
    {
        RaycastHit hit;
        if (Physics.SphereCast(modifierTransform.position, 10, modifierTransform.TransformDirection(Vector3.forward), out hit, 10))
        {
            if ((hit.transform.tag.Contains("Player") || hit.transform.tag.Contains("Tree") || hit.transform.tag.Contains("Valla")) && (hit.transform.position - modifierTransform.position).magnitude > 5 && hit.transform != transform)
                localTransform = hit.transform;
        }

        if (localTransform != null)
            savedDirection = (localTransform.position - modifierTransform.position).normalized;

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        bool isCorrect = transform.InverseTransformDirection(savedDirection).z > 0.75;

        if (savedDirection != Vector3.zero /*&& desatascadorCooldown <= 0*/  )
        {
            Vector3 sum = (modifierTransform.position + savedDirection * 3);

            if (isCorrect)
            {
                line.material.color = Color.green;
                timerPoint = 2;
                line.SetPosition(0, modifierTransform.position);
                line.SetPosition(1, sum);
            }
            else if (timerPoint > 0)
            {
                line.material.color = Color.red;
                line.SetPosition(0, modifierTransform.position);
                line.SetPosition(1, sum);
                timerPoint -= Time.deltaTime;
            }
            else
            {
                savedDirection = Vector3.zero;
            }
        }
    }

    public void PlungerUpdate()
    {
        if (plungerEnabled && !plunger /*&& desatascadorCooldown <= 0*/ && plungerInstance == null)
        {
            plungerEnabled = false;
            if (!createMaterial)
            {
                line.material = new Material(line.material);
                createMaterial = true;
            }

            plungerInstance = Instantiate(plungerPrefab, modifierTransform.position, this.transform.rotation);
            Physics.IgnoreCollision(plungerInstance.transform.GetChild(0).GetComponent<BoxCollider>(), transform.GetChild(0).GetComponent<BoxCollider>());
            plungerInstance.GetComponent<plungerInstance>().playerShotPlunger = this.gameObject;
            plungerInstance.GetComponent<plungerInstance>().playerNum = player.playerNum;
            plungerInstance.GetComponent<plungerInstance>().normalDir = savedDirection;
            plunger = true;
            plungerDisappearCooldown = plungerBaseDisappearCooldown;
        }
        else
            plungerEnabled = false;

        if (plungerDisappearCooldown > 0)
            plungerDisappearCooldown -= Time.deltaTime;

        if (plunger)
        {
            if (plungerDisappearCooldown <= plungerBaseDisappearCooldown / 2 && plungerInstance != null)
            {
                savedDirection = Vector3.zero;
                player.vehicleMaxSpeed = player.savedMaxSpeed;
                plungerInstance.GetComponent<plungerInstance>().destroyPlunger = true;
                plungerInstance = null;
                plunger = false;
                //plungerEnabled = false;
            }
            else if (plungerInstance == null)
            {
                savedDirection = Vector3.zero;
                player.vehicleMaxSpeed = player.savedMaxSpeed;
                plunger = false;
            }
            if (player.vehicleMaxSpeed > player.savedMaxSpeed)
            {
                player.vehicleMaxSpeed -= 0.5f;
            }
        }
    }
}
