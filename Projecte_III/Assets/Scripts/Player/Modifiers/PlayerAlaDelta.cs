using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlaDelta : MonoBehaviour
{
    public bool usingAlaDelta;
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;
    PlayerVehicleScript player;
    bool touchingGround;
    internal bool alaDeltaEnabled, hasAlaDelta;
    PlayerInputs inputs;

    public void Init(bool _active)
    {
        hasAlaDelta = _active;
    }

    public void Activate()
    {
        if (hasAlaDelta)
            alaDeltaEnabled = true;
    }

    private void Start()
    {
        player = GetComponent<PlayerVehicleScript>();
        inputs = GetComponent<PlayerInputs>();
        alaDeltaDuration = 1;
        alaDeltaTimer = 1;
    }

    private void Update()
    {
        if(hasAlaDelta)
        {
            AlaDeltaUpdate();
            CheckAlaDeltaGround(player.touchingGround);
        }
    }

    public void CheckAlaDeltaGround(bool touchingGround)
    {
        if (touchingGround)
        {
            alaDeltaTimer = alaDeltaDuration;
            usingAlaDelta = false;
        }
    }

    public void AlaDeltaUpdate()
    {
        if (!usingAlaDelta && touchingGround && (alaDeltaEnabled || player.controls.Quad.AlaDelta))
        {
            alaDeltaEnabled = false;
            usingAlaDelta = true;
        }
        else
            alaDeltaEnabled = false;

        if (usingAlaDelta && alaDeltaTimer >= 0)
        {
            alaDeltaTimer -= Time.deltaTime;
            if (alaDeltaTimer >= alaDeltaDuration - 0.6f)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                player.vehicleRB.velocity += new Vector3(0, 1, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                if (inputs.Right)
                    player.vehicleRB.AddTorque(new Vector3(0, player.vehicleTorque / 10, 0));
                else if (inputs.Left)
                    player.vehicleRB.AddTorque(new Vector3(0, -player.vehicleTorque / 10, 0));

                player.savedVelocity = transform.TransformDirection(new Vector3(0, 0, 25));

                player.vehicleRB.velocity = new Vector3(player.savedVelocity.x, -5, player.savedVelocity.z);
            }
            if (alaDeltaTimer <= 0)
            {
                alaDeltaTimer = alaDeltaDuration;
                usingAlaDelta = false;
            }
        }
    }
}
