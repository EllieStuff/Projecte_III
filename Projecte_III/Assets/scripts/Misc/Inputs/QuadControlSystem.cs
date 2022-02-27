using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControlSystem : MonoBehaviour
{

    public QuadStruct Quad = new QuadStruct();
    public QuadStruct QuadP2 = new QuadStruct();

    public struct QuadStruct
    {
        public bool Forward;
        public bool Backward;
        public float Left;
        public float Right;
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
                //FORWARD
                    Quad.Forward = (Input.GetAxis("ForwardP1") >= 0.5f || Input.GetKey(KeyCode.W));
                //______________________________________________
                //BACKWARD
                    Quad.Backward = (Input.GetAxis("BackwardP1") >= 0.5f || Input.GetKey(KeyCode.S));
                //______________________________________________
                //LEFT
                if (Input.GetAxis("HorizontalP1") <= -0.2f || Input.GetKey(KeyCode.A))
                    Quad.Left = -Input.GetAxis("HorizontalP1");
                else
                    Quad.Left = 0;

                if (Input.GetKey(KeyCode.A))
                    Quad.Left = 1;
                //______________________________________________
                //RIGHT
                if (Input.GetAxis("HorizontalP1") >= 0.2f || Input.GetKey(KeyCode.D))
                    Quad.Right = Input.GetAxis("HorizontalP1");
                else
                    Quad.Right = 0;

                if (Input.GetKey(KeyCode.D))
                    Quad.Right = 1;
                //______________________________________________
                //DRIFT
                /*Quad.Drift = (Input.GetAxis("DriftP1") >= 1 || Input.GetKey(KeyCode.Space));*/
                    Quad.Drift = (Input.GetKey(KeyCode.Space));
                //______________________________________________
                //CHASIS ELEVATION
                Quad.ChasisElevation = (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.LeftControl));
                //______________________________________________
                //ALADELTA
                    Quad.AlaDelta = (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.LeftShift));
                //______________________________________________
                //SWITCH CAMERA
                    Quad.LookBackwards = (Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Mouse1));
                //______________________________________________
                //PLUNGER
                    Quad.plunger = (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Q));
                //______________________________________________
                break;
            case 2:
                //FORWARD
                    QuadP2.Forward = (Input.GetAxis("ForwardP2") >= 0.5f);
                //______________________________________________
                //BACKWARD
                    QuadP2.Backward = (Input.GetAxis("BackwardP2") >= 0.5f);
                //______________________________________________
                //LEFT
                if (Input.GetAxis("HorizontalP2") <= -0.2f)
                    QuadP2.Left = -Input.GetAxis("HorizontalP2");
                else
                    QuadP2.Left = 0;
                //______________________________________________
                //RIGHT
                if (Input.GetAxis("HorizontalP2") >= 0.2f)
                    QuadP2.Right = Input.GetAxis("HorizontalP2");
                else
                    QuadP2.Right = 0;
                //______________________________________________
                //DRIFT
                /*QuadP2.Drift = (Input.GetAxis("DriftP2") >= 1);*/
                //______________________________________________
                //CHASIS ELEVATION
                    QuadP2.ChasisElevation = (Input.GetKey(KeyCode.Joystick2Button1));
                //______________________________________________
                //ALADELTA
                    QuadP2.AlaDelta = (Input.GetKey(KeyCode.Joystick2Button0));
                //______________________________________________
                //SWITCH CAMERA
                    QuadP2.LookBackwards = (Input.GetKey(KeyCode.Joystick2Button3));
                //______________________________________________
                //PLUNGER
                    QuadP2.plunger = (Input.GetKey(KeyCode.Joystick2Button4));
                //______________________________________________
                break;
        }
    }
}
