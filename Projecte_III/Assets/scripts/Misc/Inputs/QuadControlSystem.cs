using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControlSystem : MonoBehaviour
{
    const float 
        H_SENSIBILITY = 0.3f,
        V_SENSIBILITY = 0.3f;

    public QuadStruct Quad = new QuadStruct();
    public QuadStruct QuadP2 = new QuadStruct();

    public struct QuadStruct
    {
        public bool Forward;
        public bool Backward;
        public float Left;
        public float Right;
        public Vector2 j2Axis;
        public bool Drift;
        public bool UseActualGadget;
        public bool LookBackwards;
        public bool ChasisElevation;
        public bool AlaDelta;
        public bool plunger;
    }

    public void getAllInput(int playerNum)
    {
        switch(playerNum)
        {
            case 1:
                GetInputFrom(out Quad, "P1");

                break;
            case 2:
                GetInputFrom(out QuadP2, "P2");

                break;

            default:
                break;
        }
    }
    

    void GetInputFrom(out QuadStruct _quad, string _quadId) //_quadId seria P1, P2, etc.
    {
        _quad = new QuadStruct();

        /// Joystick 1
        //FORWARD
        _quad.Forward = (Input.GetAxis("Forward" + _quadId) >= 0.5f || Input.GetKey(KeyCode.W));
        //______________________________________________
        //BACKWARD
        _quad.Backward = (Input.GetAxis("Backward" + _quadId) >= 0.5f || Input.GetKey(KeyCode.S));
        //______________________________________________
        //LEFT
        if (Input.GetAxis("Horizontal" + _quadId) <= -0.3f || Input.GetKey(KeyCode.A))
            _quad.Left = -Input.GetAxis("Horizontal" + _quadId);
        else
            _quad.Left = 0;

        if (Input.GetKey(KeyCode.A))
            _quad.Left = 1;
        //______________________________________________
        //RIGHT
        if (Input.GetAxis("Horizontal" + _quadId) >= 0.3f || Input.GetKey(KeyCode.D))
            _quad.Right = Input.GetAxis("Horizontal" + _quadId);
        else
            _quad.Right = 0;

        if (Input.GetKey(KeyCode.D))
            _quad.Right = 1;

        //______________________________________________


        /// Joystick 2
        //HORIZONTAL
        if (Mathf.Abs(Input.GetAxis("HorizontalJ2" + _quadId)) >= H_SENSIBILITY)
            _quad.j2Axis.x = Input.GetAxis("HorizontalJ2" + _quadId);
        else
            _quad.j2Axis.x = 0;
        //______________________________________________
        //HORIZONTAL
        if (Mathf.Abs(Input.GetAxis("VerticalJ2" + _quadId)) >= V_SENSIBILITY)
            _quad.j2Axis.y = -Input.GetAxis("VerticalJ2" + _quadId);
        else
            _quad.j2Axis.y = 0;

        //______________________________________________


        /// Drift
        /*Quad.Drift = (Input.GetAxis("DriftP1") >= 1 || Input.GetKey(KeyCode.Space));*/

        //______________________________________________


        /// Chasis Elevation
        _quad.ChasisElevation = (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.LeftControl));

        //______________________________________________


        /// AlaDelta
        _quad.AlaDelta = (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.LeftShift));

        //______________________________________________


        /// Switch Camera
        //ToDo: Check if that works at all
        _quad.LookBackwards = (Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Mouse1));

        //______________________________________________


        /// Plunger
        _quad.plunger = (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Q));

        //______________________________________________

    }

}
