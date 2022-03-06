using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public const float INPUT_THRESHOLD = 0.3f;

    public enum KeyCodes { FORWARD, BACKWARD, LEFT, RIGHT, DRIFT, ENABLE_GADGET_MENU, CONFIRM_GADGET };
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

        }

    }


    static QuadControls controls;
    
    Dictionary<int, Vector2> j2Dirs = new Dictionary<int, Vector2>();
    Dictionary<int, Vector2> lateJ2Dirs = new Dictionary<int, Vector2>();
    int notUpdatedIts = 0, itsToUpdate = 4;

    List<ControlData> activatedControllers = new List<ControlData>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

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
        if (activatedControllers.Find(_control => _control.mainDeviceId == deviceId) != null)
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

    public float GetKeyFloat(KeyCodes _key, ControlData[] _controlData)   // Si dones problemes, adaptar tots perque vagin amb la "complexCasesReturnAux" en contres de amb returns
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

    public bool GetKey(KeyCodes _key, ControlData[] _controlData)   // Si dones problemes, adaptar tots perque vagin amb la "complexCasesReturnAux" en contres de amb returns
    {
        bool complexCasesReturnAux = false;
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
                            if (controls.Quad.Forward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.BACKWARD:
                    for (int i = 0; i < controls.Quad.Backward.controls.Count; i++)
                    {
                        if (controls.Quad.Backward.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Backward is " + controls.Quad.Backward.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Backward.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.LEFT:
                    for (int i = 0; i < controls.Quad.Left.controls.Count; i++)
                    {
                        if (controls.Quad.Left.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Left is " + controls.Quad.Left.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Left.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.RIGHT:
                    for (int i = 0; i < controls.Quad.Right.controls.Count; i++)
                    {
                        if (controls.Quad.Right.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Right is " + controls.Quad.Right.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Right.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.DRIFT:
                    for (int i = 0; i < controls.Quad.Drift.controls.Count; i++)
                    {
                        if (controls.Quad.Drift.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("Drift is " + controls.Quad.Drift.controls[i].EvaluateMagnitude());
                            if (controls.Quad.Drift.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.ENABLE_GADGET_MENU:
                    for (int i = 0; i < controls.Quad.EnableRadialMenu.controls.Count; i++)
                    {
                        if (controls.Quad.EnableRadialMenu.controls[i].device.deviceId == mainDeviceId)
                        {
                            //Debug.Log("EnableRadialMenu is " + controls.Quad.EnableRadialMenu.controls[i].EvaluateMagnitude());
                            if (controls.Quad.EnableRadialMenu.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD) return true;
                        }
                    }

                    break;

                case KeyCodes.CONFIRM_GADGET:
                    // If using Keyboard
                    if (_controlData[idx].deviceType == DeviceTypes.KEYBOARD)
                    {
                        for (int i = 0; i < controls.Quad.ConfirmChosenGadget.controls.Count; i++)
                        {
                            if (controls.Quad.ConfirmChosenGadget.controls[i].EvaluateMagnitude() > INPUT_THRESHOLD
                                && controls.Quad.ConfirmChosenGadget.controls[i].device.deviceId == _controlData[idx].mouseDeviceId)
                                complexCasesReturnAux = true;
                                //return true;
                        }
                    }
                    // If using Controller
                    else
                    {
                        for (int i = 0; i < controls.Quad.ConfirmChosenGadget.controls.Count; i++)
                        {
                            if (controls.Quad.ConfirmChosenGadget.controls[i].device.deviceId == mainDeviceId)
                            {
                                if (lateJ2Dirs.ContainsKey(mainDeviceId))
                                {
                                    complexCasesReturnAux = IsInThreshold(j2Dirs[mainDeviceId]) && !IsInThreshold(lateJ2Dirs[mainDeviceId]);
                                }
                            }
                        }
                    }

                    break;

                default:
                    break;
            }

        }

        if (_key == KeyCodes.CONFIRM_GADGET)
            return complexCasesReturnAux;

        return false;

    }

    public Vector2 GetAxis(AxisCodes _joystick, ControlData[] _controlData)
    {
        for (int idx = 0; idx < _controlData.Length; idx++)
        {
            int mainDeviceId = _controlData[idx].mainDeviceId;
            switch (_joystick)
            {
                case AxisCodes.CHOOSE_ITEM:
                    // If using Keyboard
                    if (_controlData[idx].deviceType == DeviceTypes.KEYBOARD)
                    {
                        for (int i = 0; i < controls.Quad.ChooseItemMouse.controls.Count; i++)
                        {
                            if (controls.Quad.ChooseItemMouse.controls[i].device.deviceId == _controlData[idx].mouseDeviceId)
                            {
                                Vector3 screenCenter = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
                                return (Input.mousePosition - screenCenter).normalized;
                            }
                        }
                    }
                    else    // If using Controller
                    {
                        for (int i = 0; i < controls.Quad.ChooseItemRight.controls.Count; i++)
                        {
                            if (controls.Quad.ChooseItemRight.controls[i].device.deviceId == mainDeviceId)
                            {
                                Vector2 tmpVec = new Vector2(
                                    controls.Quad.ChooseItemRight.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemLeft.controls[i].EvaluateMagnitude(),
                                    controls.Quad.ChooseItemUp.controls[i].EvaluateMagnitude() - controls.Quad.ChooseItemDown.controls[i].EvaluateMagnitude()
                                );
                                RefreshJ2Dirs(mainDeviceId, tmpVec);

                                if (!IsInThreshold(tmpVec))
                                    return tmpVec;

                            }
                        }
                    }

                    break;


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
