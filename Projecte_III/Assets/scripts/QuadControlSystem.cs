using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControlSystem : MonoBehaviour
{

    public P1 Quad = new P1();
    public P2 QuadP2 = new P2();

    public struct P1
    {
        public float Forward;
        public float Backward;
        public float Left;
        public float Right;
        public float Drift;
        public float UseActualGadget;
        public float LookBackwards;
        public float ChasisElevation;
        public float AlaDelta;
    }

    public struct P2
    {
        public float Forward;
        public float Backward;
        public float Left;
        public float Right;
        public float Drift;
        public float UseActualGadget;
        public float LookBackwards;
        public float ChasisElevation;
        public float AlaDelta;
    }

    public void getAllInput(int playerNum)
    {
        switch(playerNum)
        {
            case 1:
                //FORWARD
                if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.W))
                    Quad.Forward = 1;
                else
                    Quad.Forward = 0;
                //______________________________________________
                //BACKWARD
                if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.S))
                    Quad.Backward = 1;
                else
                    Quad.Backward = 0;
                //______________________________________________
                //LEFT
                if (Input.GetAxis("HorizontalP1") <= -1 || Input.GetKey(KeyCode.A))
                    Quad.Left = 1;
                else
                    Quad.Left = 0;
                //______________________________________________
                //RIGHT
                if (Input.GetAxis("HorizontalP1") >= 1 || Input.GetKey(KeyCode.D))
                    Quad.Right = 1;
                else
                    Quad.Right = 0;
                //______________________________________________
                //DRIFT
                if (Input.GetAxis("DriftP1") >= 1 || Input.GetKey(KeyCode.Space))
                    Quad.Drift = 1;
                else
                    Quad.Drift = 0;
                //______________________________________________
                //CHASIS ELEVATION
                if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.LeftControl))
                    Quad.ChasisElevation = 1;
                else
                    Quad.ChasisElevation = 0;
                //______________________________________________
                //ALADELTA
                if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.LeftShift))
                    Quad.AlaDelta = 1;
                else
                    Quad.AlaDelta = 0;
                //______________________________________________
                //SWITCH CAMERA
                if (Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Mouse1))
                    Quad.LookBackwards = 1;
                else
                    Quad.LookBackwards = 0;
                //______________________________________________
                break;
            case 2:
                //FORWARD
                if (Input.GetKey(KeyCode.Joystick2Button5))
                    QuadP2.Forward = 1;
                else
                    QuadP2.Forward = 0;
                //______________________________________________
                //BACKWARD
                if (Input.GetKey(KeyCode.Joystick2Button4))
                    QuadP2.Backward = 1;
                else
                    QuadP2.Backward = 0;
                //______________________________________________
                //LEFT
                if (Input.GetAxis("HorizontalP2") <= -1)
                    QuadP2.Left = 1;
                else
                    QuadP2.Left = 0;
                //______________________________________________
                //RIGHT
                if (Input.GetAxis("HorizontalP2") >= 1)
                    QuadP2.Right = 1;
                else
                    QuadP2.Right = 0;
                //______________________________________________
                //DRIFT
                if (Input.GetAxis("DriftP2") >= 1)
                    QuadP2.Drift = 1;
                else
                    QuadP2.Drift = 0;
                //______________________________________________
                //CHASIS ELEVATION
                if (Input.GetKey(KeyCode.Joystick2Button1))
                    QuadP2.ChasisElevation = 1;
                else
                    QuadP2.ChasisElevation = 0;
                //______________________________________________
                //ALADELTA
                if (Input.GetKey(KeyCode.Joystick2Button0))
                    QuadP2.AlaDelta = 1;
                else
                    QuadP2.AlaDelta = 0;
                //______________________________________________
                //SWITCH CAMERA
                if (Input.GetKey(KeyCode.Joystick2Button3))
                    QuadP2.LookBackwards = 1;
                else
                    QuadP2.LookBackwards = 0;
                //______________________________________________
                break;
        }
    }
}
