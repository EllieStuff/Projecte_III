using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    PlayerInputs inputs;
    Rigidbody vehicleRB;

    float vehicleMaxSpeed = 16.0f, speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerInputs>();

        vehicleRB = gameObject.AddComponent<Rigidbody>();
        vehicleRB.isKinematic = true;

        vehicleRB.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _velocity = vehicleRB.velocity;
        if(inputs.Forward)
        {
            if(_velocity.z + speed < vehicleMaxSpeed)
                _velocity.z += speed;
        }
        if(inputs.Backward)
        {
            if (_velocity.z - speed > -vehicleMaxSpeed)
                _velocity.z -= speed;
        }
        if(inputs.Left)
        {
            if (_velocity.x - speed > -vehicleMaxSpeed)
                _velocity.x -= speed;
        }
        if (inputs.Right)
        {
            if (_velocity.x + speed < vehicleMaxSpeed)
                _velocity.x += speed;
        }

        vehicleRB.velocity = _velocity;
    }
}
