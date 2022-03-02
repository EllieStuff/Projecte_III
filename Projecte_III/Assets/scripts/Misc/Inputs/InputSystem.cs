using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    const float INPUT_THRESHOLD = 0.3f;

    public enum KeyCodes { FORWARD, BACKWARD, LEFT, RIGHT, DRIFT, OPEN_GADGET_MENU, CONFIRM_GADGET };
    public enum AxisCodes { CHOOSE_ITEM };
    public enum DeviceTypes { DEFAULT, KEYBOARD, CONTROLLER };

    public class ControlData
    {
        public int controlId = -1, deviceId = -1;
        public DeviceTypes deviceType = DeviceTypes.DEFAULT;
        public ControlData() { }
        public ControlData(int _controlId, int _deviceId, string _path)
        {
            controlId = _controlId;
            deviceId = _deviceId;
            if (_path.Contains("Mouse") || _path.Contains("Keyboard")) deviceType = DeviceTypes.KEYBOARD;
            else deviceType = DeviceTypes.CONTROLLER;
        }
    }
    //public class ControlData
    //{
    //    public int mainControlId, mouseControlId;
    //    public int mainDeviceId, mouseDeviceId;
    //    public DeviceTypes deviceType;

    //    public ControlData() { }
    //    public ControlData(string _path)
    //    {
    //        if (_path.Contains("Mouse") || _path.Contains("Keyboard")) deviceType = DeviceTypes.KEYBOARD;
    //        else deviceType = DeviceTypes.CONTROLLER;
    //    }

    //    public void InitKeyboard(QuadControls _controls, int _mouseIdx, int _keyboardIdx)
    //    {
    //        //if(deviceType != DeviceTypes.KEYBOARD)
    //        //{
    //        //    Debug.LogError("Device Type wasn't assigned properly or Wrong function called");
    //        //    return;
    //        //}


    //        deviceType = DeviceTypes.KEYBOARD;

    //        mouseControlId = _mouseIdx;
    //        mouseDeviceId = _controls.Quad.ActivateController.controls[_mouseIdx].device.deviceId;
    //        mainControlId = _keyboardIdx;
    //        mainDeviceId = _controls.Quad.Forward.controls[0].device.deviceId;

    //    }
    //    public void InitController(QuadControls _controls, int _controllerIdx)
    //    {
    //        //if (deviceType != DeviceTypes.CONTROLLER)
    //        //{
    //        //    Debug.LogError("Device Type wasn't assigned properly or Wrong function called");
    //        //    return;
    //        //}


    //        deviceType = DeviceTypes.CONTROLLER;

    //        mainControlId = _controllerIdx;
    //        mainDeviceId = _controls.Quad.ActivateController.controls[_controllerIdx].device.deviceId;

    //    }

    //}

    QuadControls controls;
    Dictionary<int, Vector2> j2Dirs = new Dictionary<int, Vector2>();
    Dictionary<int, Vector2> lateJ2Dirs = new Dictionary<int, Vector2>();

    List<ControlData> activatedControllers = new List<ControlData>();

    // Start is called before the first frame update
    void Start()
    {
        controls = new QuadControls();
        controls.Enable();

        //controllers = GetAllControllersData();
    }

    private void Update()
    {
        ControlData controller = GetActiveControlData();
        Debug.Log("Control Active: " + controller.deviceId);

        //GetAxis(AxisCodes.CHOOSE_ITEM, controllers[i]);
        //Debug.Log("Controller " + i + ": " + GetKey(KeyCodes.CONFIRM_GADGET, controllers[i]));

        //GetAxis(AxisCodes.CHOOSE_ITEM, controllers[i]);
        //Debug.Log("Controller " + 0 + ": " + GetAxis(AxisCodes.CHOOSE_ITEM, new ControlData(0, 2, "Mouse")));
        //Debug.Log("Controller " + 1 + ": " + GetAxis(AxisCodes.CHOOSE_ITEM, new ControlData(0, 15, "Controller")));

        //Debug.Log("Controller " + 0 + ": " + GetAxis(AxisCodes.CHOOSE_ITEM, new ControlData(0, 15)));
        //Debug.Log("Controller " + 0 + ": " + GetKey(KeyCodes.CONFIRM_GADGET, controllers[i]));

    }

    private void LateUpdate()
    {
        lateJ2Dirs = new Dictionary<int, Vector2>(j2Dirs);
    }

    // Plantejat perque puguis fer inputs amb tots el devices en el mode d'un jugador
    public ControlData[] GetAllControllersData()
    {
        ControlData[] controllers = new ControlData[controls.Quad.ActivateController.controls.Count];
        for (int i = 0; i < controllers.Length; i++)
        {
            Debug.Log("Path " + i + ": " + controls.Quad.ActivateController.controls[i].path);
            controllers[i] = new ControlData(i, controls.Quad.ActivateController.controls[i].device.deviceId, controls.Quad.ActivateController.controls[i].path);
        }

        return controllers;
    }

    // Plantejat pel mode multijugador
    public ControlData GetActiveControlData()
    {
        if (controls.Quad.ActivateController.activeControl == null) 
            return new ControlData();

        int deviceId = controls.Quad.ActivateController.activeControl.device.deviceId;
        if (activatedControllers.Find(_control => _control.deviceId == deviceId) != null)
            return new ControlData();

        for (int i = 0; i < controls.Quad.ActivateController.controls.Count; i++)
        {
            if (controls.Quad.ActivateController.controls[i].device.deviceId == deviceId)
            {
                if (controls.Quad.ActivateController.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD)
                {
                    ControlData control = new ControlData(i, deviceId, controls.Quad.ActivateController.controls[i].path);
                    activatedControllers.Add(control);
                    return control;
                }
            }

        }

        return null;
    }


    public bool GetKey(KeyCodes _key, ControlData _controlData)
    {
        int ctrIdx = _controlData.controlId;
        switch (_key)
        {
            case KeyCodes.FORWARD:
                return controls.Quad.Forward.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.BACKWARD:
                return controls.Quad.Backward.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.LEFT:
                return controls.Quad.Left.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.RIGHT:
                return controls.Quad.Right.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.DRIFT:
                return controls.Quad.Drift.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.OPEN_GADGET_MENU:
                return controls.Quad.UseActualGadget.controls[ctrIdx].EvaluateMagnitude() > INPUT_THRESHOLD;

            case KeyCodes.CONFIRM_GADGET:
                // If using Keyboard
                if (_controlData.deviceType == DeviceTypes.KEYBOARD)
                {
                    for (int i = 0; i < controls.Quad.ConfirmChosenGadget.controls.Count; i++)
                    {
                        if (controls.Quad.ConfirmChosenGadget.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD 
                            && controls.Quad.ConfirmChosenGadget.controls[i].device.deviceId == _controlData.deviceId)
                            return true;
                    }
                }

                for (int i = 0; i < controls.Quad.ConfirmChosenGadget.controls.Count; i++)
                {
                    if (_controlData.deviceType == DeviceTypes.CONTROLLER && controls.Quad.ConfirmChosenGadget.controls[i].device.deviceId == _controlData.deviceId)
                    {
                        if(lateJ2Dirs.ContainsKey(_controlData.deviceId))
                        {
                            return !IsInThreshold(j2Dirs[_controlData.deviceId]) && IsInThreshold(lateJ2Dirs[_controlData.deviceId]);
                        }
                    }
                }

                break;

            default:
                break;
        }


        return false;

    }

    public Vector2 GetAxis(AxisCodes _joystick, ControlData _controlData)
    {
        switch (_joystick)
        {
            case AxisCodes.CHOOSE_ITEM:
                // If using Keyboard
                if(_controlData.deviceType == DeviceTypes.KEYBOARD){
                    for (int i = 0; i < controls.Quad.ChooseItemMouse.controls.Count; i++)
                    {
                        if (controls.Quad.ChooseItemMouse.controls[i].device.deviceId == _controlData.deviceId)
                        {
                            Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
                            return (Input.mousePosition - screenCenter).normalized;
                        }
                    }
                }

                // If using Controller
                for (int i = 0; i < controls.Quad.ChooseItemRight.controls.Count; i++)
                {
                    if (controls.Quad.ChooseItemRight.controls[i].device.deviceId == _controlData.deviceId)
                    {
                        Vector2 tmpVec = new Vector2(
                            controls.Quad.ChooseItemRight.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemLeft.controls[i].EvaluateMagnitude(),
                            controls.Quad.ChooseItemUp.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemDown.controls[i].EvaluateMagnitude()
                        );
                        RefreshJ2Dirs(_controlData.deviceId, tmpVec);

                        if (!IsInThreshold(tmpVec))
                            return tmpVec;

                    }
                }

                break;


            default:
                break;
        }


        return new Vector2();
    }


    bool IsInThreshold(Vector2 _v) => Mathf.Abs(_v.x) > INPUT_THRESHOLD || Mathf.Abs(_v.y) > INPUT_THRESHOLD;


    void RefreshJ2Dirs(int _key, Vector2 _v)
    {
        if (j2Dirs.ContainsKey(_key)) j2Dirs[_key] = _v;
        else j2Dirs.Add(_key, _v);
    }


}
