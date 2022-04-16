using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlaDelta : MonoBehaviour
{
    public bool usingAlaDelta;
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;
    PlayerVehicleScript player;
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
        alaDeltaDuration = 2;
        alaDeltaTimer = 1;
    }

    private void FixedUpdate()
    {
        if(hasAlaDelta)
        {
            AlaDeltaUpdate();
            CheckAlaDeltaGround(player.touchingGround);
        }
    }

    public void CheckAlaDeltaGround(bool touchingGround)
    {
        if (alaDeltaTimer < alaDeltaDuration - 0.9f && touchingGround)
        {
            alaDeltaTimer = alaDeltaDuration;
            usingAlaDelta = false;
        }
    }

    public void AlaDeltaUpdate()
    {
        if (!usingAlaDelta && player.touchingGround && (alaDeltaEnabled || inputs.ShootAny))
        {
            alaDeltaEnabled = false;
            usingAlaDelta = true;
        }
        else
            alaDeltaEnabled = false;

        if (usingAlaDelta && alaDeltaTimer >= 0)
        {
            alaDeltaTimer -= Time.deltaTime;
            if (alaDeltaTimer >= alaDeltaDuration - 0.9f)
            {
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                player.vehicleRB.velocity = new Vector3(player.vehicleRB.velocity.x, 5, player.vehicleRB.velocity.z);
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
        }
    }
}
