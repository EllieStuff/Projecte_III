using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlaDelta : MonoBehaviour
{
    public bool alaDelta;
    [SerializeField] float alaDeltaDuration;
    [SerializeField] float alaDeltaTimer;

    private void Start()
    {
        alaDeltaDuration = 1;
        alaDeltaTimer = 1;
    }

    public void CheckAlaDeltaGround(bool touchingGround)
    {
        if (touchingGround)
        {
            alaDeltaTimer = alaDeltaDuration;
            alaDelta = false;
        }
    }

    public void AlaDeltaFunction(PlayerVehicleScript player, QuadControlSystem controls ,PlayerInputs inputs, bool touchingGround, bool alaDeltaEnabled)
    {
        if (!alaDelta && touchingGround && (alaDeltaEnabled || controls.Quad.AlaDelta))
            alaDelta = true;
        else
            alaDeltaEnabled = false;

        if (alaDelta && alaDeltaTimer >= 0)
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
                alaDelta = false;
            }
        }
    }
}
