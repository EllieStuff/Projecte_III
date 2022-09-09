using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public const float INPUT_THRESHOLD = 0.3f;

    public enum KeyCodes { FORWARD, BACKWARD, LEFT, RIGHT, START, SELECT_AIS, ESCAPE, DRIFT, SHOOT_FORWARD, SHOOT_BACKWARD, SHOOT_LEFT, SHOOT_RIGHT, MENU_UP, MENU_DOWN, MENU_RIGHT, MENU_LEFT, MENU_ACCEPT, MENU_DECLINE };
    public enum AxisCodes { CHOOSE_ITEM };
    public enum DeviceTypes { DEFAULT, KEYBOARD, CONTROLLER };

    public class ControlData
    {
        public int mainDeviceId, mouseDeviceId;
        public DeviceTypes deviceType;

        public ControlData() { }

        public void InitKeyboard(QuadControls _controls, int _keyboardIdx, int _mouseIdx)
        {
            deviceType = DeviceTypes.KEYBOARD;

            mainDeviceId = _controls.Quad.ActivateController.controls[_keyboardIdx].device.deviceId;
            mouseDeviceId = _controls.Quad.ActivateController.controls[_mouseIdx].device.deviceId;

        }
        public void InitController(QuadControls _controls, int _controllerIdx)
        {
            deviceType = DeviceTypes.CONTROLLER;

            mainDeviceId = _controls.Quad.ActivateController.controls[_controllerIdx].device.deviceId;
            mouseDeviceId = -1;

        }

    }
    public class KeyData
    {
        public int deviceId = -1;
        public DeviceTypes deviceType = DeviceTypes.DEFAULT;
        public bool pressed = false;
        public KeyData(int _deviceId, DeviceTypes _deviceType, bool _pressed) { deviceId = _deviceId; deviceType = _deviceType; pressed = _pressed; }
    }


    public static QuadControls controls;

    Dictionary<int, Vector2> j2Dirs = new Dictionary<int, Vector2>();
    Dictionary<int, Vector2> lateJ2Dirs = new Dictionary<int, Vector2>();
    int notUpdatedIts = 0, itsToUpdate = 4;

    List<ControlData> activatedControllers = new List<ControlData>();

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        controls = new QuadControls();
        controls.Enable();
    }

    private void LateUpdate()
    {
        if (notUpdatedIts < itsToUpdate)
            notUpdatedIts++;
        else
        {
            notUpdatedIts = 0;
            lateJ2Dirs = new Dictionary<int, Vector2>(j2Dirs);
        }
    }

    // Plantejat perque puguis fer inputs amb tots el devices en el mode d'un jugador
    public ControlData[] GetAllControllersData()
    {
        List<ControlData> controllers = new List<ControlData>();
        int keyboardIdx = -1, mouseIdx = -1;
        for (int i = 0; i < controls.Quad.ActivateController.controls.Count; i++)
        {
            if (controls.Quad.ActivateController.controls[i].path.Contains("Keyboard"))
                keyboardIdx = i;
            else if (controls.Quad.ActivateController.controls[i].path.Contains("Mouse"))
                mouseIdx = i;
            else
            {
                controllers.Add(new ControlData());
                controllers[controllers.Count - 1].InitController(controls, i);
            }

        }

        if (keyboardIdx >= 0 && mouseIdx >= 0)
        {
            controllers.Add(new ControlData());
            controllers[controllers.Count - 1].InitKeyboard(controls, keyboardIdx, mouseIdx);
        }

        return controllers.ToArray();
    }

    // Plantejat pel mode multijugador
    public ControlData GetActiveControllerData()
    {
        if (controls.Quad.ActivateController.activeControl == null)
            return null;

        int deviceId = controls.Quad.ActivateController.activeControl.device.deviceId;
        if (activatedControllers.Find(_control => _control.mainDeviceId == deviceId || _control.mouseDeviceId == deviceId) != null)
            return null;

        for (int i = 0; i < controls.Quad.ActivateController.controls.Count; i++)
        {
            if (controls.Quad.ActivateController.controls[i].device.deviceId == deviceId)
            {
                if (controls.Quad.ActivateController.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                {
                    ControlData control = new ControlData();
                    if (deviceId == 1)  // Search from Keyboard
                    {
                        for (int j = 0; j < controls.Quad.ActivateController.controls.Count; j++)
                        {
                            if (controls.Quad.ActivateController.controls[j].device.deviceId == 2)
                            {
                                control.InitKeyboard(controls, i, j);
                                activatedControllers.Add(control);
                                Debug.Log("Added " + control.deviceType + " " + control.mainDeviceId);
                                return control;
                            }
                        }
                    }
                    else if (deviceId == 2) // Search From Mouse
                    {
                        for (int j = 0; j < controls.Quad.ActivateController.controls.Count; j++)
                        {
                            if (controls.Quad.ActivateController.controls[j].device.deviceId == 1)
                            {
                                control.InitKeyboard(controls, j, i);
                                activatedControllers.Add(control);
                                Debug.Log("Added " + control.deviceType + " " + control.mainDeviceId);
                                return control;
                            }
                        }
                    }
                    else
                    {
                        control.InitController(controls, i);
                        activatedControllers.Add(control);
                        Debug.Log("Added " + control.deviceType + " " + control.mainDeviceId);
                        return control;
                    }

                }
            }

        }

        return null;
    }

    public ControlData GetControllerData(int _deviceId)
    {
        ControlData controller = new ControlData();
        for (int i = 0; i < controls.Quad.ActivateController.controls.Count; i++)
        {
            if (controls.Quad.ActivateController.controls[i].device.deviceId != _deviceId)
                continue;

            if (controls.Quad.ActivateController.controls[i].path.Contains("Keyboard"))
            {
                for (int j = 0; j < controls.Quad.ActivateController.controls.Count; j++)
                {
                    if (controls.Quad.ActivateController.controls[j].path.Contains("Mouse"))
                    {
                        controller.InitKeyboard(controls, i, j);
                        activatedControllers.Add(controller);
                        return controller;
                    }
                }
            }
            else
            {
                controller.InitController(controls, i);
                activatedControllers.Add(controller);
                return controller;
            }

        }

        return null;
    }


    public float GetKeyFloat(KeyCodes _key, ControlData[] _controlData)
    {
        for (int idx = 0; idx < _controlData.Length; idx++)
        {
            int mainDeviceId = _controlData[idx].mainDeviceId;
            switch (_key)
            {
                case KeyCodes.FORWARD:
                    for (int i = 0; i < controls.Quad.Forward.controls.Count; i++)
                    {
                        if (controls.Quad.Forward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Forward is " + controls.Quad.Forward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Forward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                                return controls.Quad.Forward.controls[i].EvaluateMagnitude();
                        }
                    }

                    break;

                case KeyCodes.BACKWARD:
                    for (int i = 0; i < controls.Quad.Backward.controls.Count; i++)
                    {
                        if (controls.Quad.Backward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Backward is " + controls.Quad.Backward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Backward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                                return controls.Quad.Backward.controls[i].EvaluateMagnitude();
                        }
                    }

                    break;

                case KeyCodes.LEFT:
                    for (int i = 0; i < controls.Quad.Left.controls.Count; i++)
                    {
                        if (controls.Quad.Left.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Left is " + controls.Quad.Left.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Left.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                                return controls.Quad.Left.controls[i].EvaluateMagnitude();
                        }
                    }

                    break;

                case KeyCodes.RIGHT:
                    for (int i = 0; i < controls.Quad.Right.controls.Count; i++)
                    {
                        if (controls.Quad.Right.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Right is " + controls.Quad.Right.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Right.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                                return controls.Quad.Right.controls[i].EvaluateMagnitude();
                        }
                    }

                    break;

                case KeyCodes.DRIFT:
                    for (int i = 0; i < controls.Quad.Drift.controls.Count; i++)
                    {
                        if (controls.Quad.Drift.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Drift is " + controls.Quad.Drift.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Drift.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                                return controls.Quad.Drift.controls[i].EvaluateMagnitude();
                        }
                    }

                    break;

                default:
                    break;
            }

        }

        return 0.0f;

    }

    public KeyData GetKeyData(KeyCodes _key, ControlData[] _controlData)
    {
        for (int idx = 0; idx < _controlData.Length; idx++)
        {
            int mainDeviceId = _controlData[idx].mainDeviceId;
            KeyData currKeyData = new KeyData(mainDeviceId, _controlData[idx].deviceType, true);
            switch (_key)
            {
                case KeyCodes.FORWARD:
                    for (int i = 0; i < controls.Quad.Forward.controls.Count; i++)
                    {
                        if (controls.Quad.Forward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Forward is " + controls.Quad.Forward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Forward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.BACKWARD:
                    for (int i = 0; i < controls.Quad.Backward.controls.Count; i++)
                    {
                        if (controls.Quad.Backward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Backward is " + controls.Quad.Backward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Backward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.LEFT:
                    for (int i = 0; i < controls.Quad.Left.controls.Count; i++)
                    {
                        if (controls.Quad.Left.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Left is " + controls.Quad.Left.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Left.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.RIGHT:
                    for (int i = 0; i < controls.Quad.Right.controls.Count; i++)
                    {
                        if (controls.Quad.Right.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Right is " + controls.Quad.Right.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Right.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.START:
                    for (int i = 0; i < controls.Quad.Start.controls.Count; i++)
                    {
                        if (controls.Quad.Start.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Start is " + controls.Quad.Start.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Start.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;
                
                case KeyCodes.SELECT_AIS:
                    for (int i = 0; i < controls.BuildingMenu.SelectAIs.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.SelectAIs.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Start is " + controls.Quad.Start.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.SelectAIs.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;
                
                case KeyCodes.ESCAPE:
                    for (int i = 0; i < controls.Quad.Escape.controls.Count; i++)
                    {
                        if (controls.Quad.Escape.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Escape is " + controls.Quad.Escape.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Escape.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.DRIFT:
                    for (int i = 0; i < controls.Quad.Drift.controls.Count; i++)
                    {
                        if (controls.Quad.Drift.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Drift is " + controls.Quad.Drift.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Drift.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.SHOOT_FORWARD:
                    for (int i = 0; i < controls.Quad.ShootForward.controls.Count; i++)
                    {
                        if (controls.Quad.ShootForward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Up is " + controls.Quad.ShootForward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.ShootForward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }
                    break;

                case KeyCodes.SHOOT_BACKWARD:
                    for (int i = 0; i < controls.Quad.ShootBackwards.controls.Count; i++)
                    {
                        if (controls.Quad.ShootBackwards.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Up is " + controls.Quad.ShootBackwards.controls[i].EvaluateMagnitude());
                            if (controls.Quad.ShootBackwards.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }
                    break;

                case KeyCodes.SHOOT_LEFT:
                    for (int i = 0; i < controls.Quad.ShootLeft.controls.Count; i++)
                    {
                        if (controls.Quad.ShootLeft.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Up is " + controls.Quad.ShootLeft.controls[i].EvaluateMagnitude());
                            if (controls.Quad.ShootLeft.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }
                    break;

                case KeyCodes.SHOOT_RIGHT:
                    for (int i = 0; i < controls.Quad.ShootRight.controls.Count; i++)
                    {
                        if (controls.Quad.ShootRight.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Up is " + controls.Quad.ShootRight.controls[i].EvaluateMagnitude());
                            if (controls.Quad.ShootRight.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }
                    break;

                //MENU_UP, MENU_DOWN, MENU_RIGHT, MENU_LEFT, MENU_ACCEPT, MENU_DECLINE
                case KeyCodes.MENU_UP:
                    for (int i = 0; i < controls.BuildingMenu.Up.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Up.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Up is " + controls.BuildingMenu.Up.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Up.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.MENU_DOWN:
                    for (int i = 0; i < controls.BuildingMenu.Down.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Down.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Down is " + controls.BuildingMenu.Down.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Down.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.MENU_RIGHT:
                    for (int i = 0; i < controls.BuildingMenu.Right.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Right.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Right is " + controls.BuildingMenu.Right.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Right.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.MENU_LEFT:
                    for (int i = 0; i < controls.BuildingMenu.Left.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Left.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Left is " + controls.BuildingMenu.Left.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Left.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.MENU_ACCEPT:
                    for (int i = 0; i < controls.BuildingMenu.Accept.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Accept.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Accept is " + controls.BuildingMenu.Accept.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Accept.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                case KeyCodes.MENU_DECLINE:
                    for (int i = 0; i < controls.BuildingMenu.Decline.controls.Count; i++)
                    {
                        if (controls.BuildingMenu.Decline.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("BuildingMenu.Decline is " + controls.BuildingMenu.Decline.controls[i].EvaluateMagnitude());
                            if (controls.BuildingMenu.Decline.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return currKeyData;
                        }
                    }

                    break;

                default:
                    break;

            }

        }

        return new KeyData(-1, DeviceTypes.DEFAULT, false);

    }

    public Vector2 GetAxis(AxisCodes _joystick, ControlData[] _controlData)
    {
        for (int idx = 0; idx < _controlData.Length; idx++)
        {
            int mainDeviceId = _controlData[idx].mainDeviceId;
            switch (_joystick)
            {
                //case AxisCodes.CHOOSE_ITEM:
                //    // If using Keyboard
                //    if (_controlData[idx].deviceType == DeviceTypes.KEYBOARD)
                //    {
                //        for (int i = 0; i < controls.Quad.ChooseItemMouse.controls.Count; i++)
                //        {
                //            if (controls.Quad.ChooseItemMouse.controls[i].device.deviceId == _controlData[idx].mouseDeviceId)
                //            {
                //                Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
                //                return (Input.mousePosition - screenCenter).normalized;
                //            }
                //        }
                //    }
                //    else    // If using Controller
                //    {
                //        for (int i = 0; i < controls.Quad.ChooseItemRight.controls.Count; i++)
                //        {
                //            if (controls.Quad.ChooseItemRight.controls[i].device.deviceId == mainDeviceId)
                //            {
                //                Vector2 tmpVec = new Vector2(
                //                    controls.Quad.ChooseItemRight.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemLeft.controls[i].EvaluateMagnitude(),
                //                    controls.Quad.ChooseItemUp.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemDown.controls[i].EvaluateMagnitude()
                //                );
                //                RefreshJ2Dirs(mainDeviceId, tmpVec);

                //                if (!IsInThreshold(tmpVec))
                //                    return tmpVec;

                //            }
                //        }
                //    }

                //    break;


                default:
                    break;
            }

        }


        return new Vector2();
    }

    public static QuadControls.QuadActions GetQuadActions()
    {
        return controls.Quad;
    }



    bool IsInThreshold(Vector2 _v) => !(Mathf.Abs(_v.x) > INPUT_THRESHOLD || Mathf.Abs(_v.y) > INPUT_THRESHOLD);


    void RefreshJ2Dirs(int _key, Vector2 _v)
    {
        if (j2Dirs.ContainsKey(_key)) j2Dirs[_key] = _v;
        else j2Dirs.Add(_key, _v);
    }


}
