// GENERATED AUTOMATICALLY FROM 'Assets/scripts/QuadControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @QuadControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @QuadControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""QuadControls"",
    ""maps"": [
        {
            ""name"": ""Quad"",
            ""id"": ""5920d9fb-cb81-4def-9171-362e9a51f3e8"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""69e866fe-1932-48de-b9fe-259104c7fafc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Value"",
                    ""id"": ""bbbb3444-77fa-4214-a8c4-86e1cf9f8d8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Value"",
                    ""id"": ""f7540f73-5297-4979-8a60-436418eac4d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Value"",
                    ""id"": ""3fc468ca-8a23-48aa-af88-799376322c46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Value"",
                    ""id"": ""d6561ffe-ce4d-4299-8ae3-36482de2a523"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseActualGadget"",
                    ""type"": ""Button"",
                    ""id"": ""18a3717e-c594-4bc1-bbf4-027d623bade9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookBackwards"",
                    ""type"": ""Value"",
                    ""id"": ""c6e2a57c-8d40-4cc3-b771-3ed9d07e62dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChasisElevation"",
                    ""type"": ""Button"",
                    ""id"": ""6e1b1bb5-77ff-493c-9f3c-692aa3e32ec0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AlaDelta"",
                    ""type"": ""Button"",
                    ""id"": ""183df223-cdd4-4beb-9465-d9577c47fd7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3f4583ce-b200-4c91-8f08-740aba049176"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f08b5502-4059-4f1f-87bb-90666a1d0a58"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""202560e2-7127-4d0b-9884-e4d18122fca6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4894e6c2-925e-49c1-836a-e99a00a7f0f8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e473770-d894-419e-9740-c2f0d3188a14"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d7f2119-0c9a-49a0-bcba-1a9cb5cb08d4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9278e506-2da4-459b-b8bd-e4865a2bdea9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90aeb706-7807-4779-818f-d078fd8d12da"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""110fda46-2192-4e8b-b440-9e4f08c65cde"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d7539bf-4baa-48cf-96f4-396b5aa493e3"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fad30964-d4dc-4650-a5da-3909c606e34b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseActualGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c584fdd9-1d0f-4884-b9b5-164e6bf0f467"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseActualGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""867fe093-8aba-4c9b-a12d-a9513e45c799"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f682c0ab-6282-4936-80d1-6d6785fced7f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""483fbb31-2a07-4c63-b3b7-fbfb81657d89"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChasisElevation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""326b1345-901d-460a-b5a4-f7aba8c38d3f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChasisElevation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01ffc2be-886e-49f9-a450-4c1b2c38ae39"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AlaDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33e50063-66e4-42f5-b228-60199c79e861"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AlaDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Construction Menu"",
            ""id"": ""0f660e42-79b2-456b-ab56-e54bf92be21f"",
            ""actions"": [
                {
                    ""name"": ""Delete Modifier"",
                    ""type"": ""Button"",
                    ""id"": ""e603e5c5-f50c-42eb-8c88-ececdfd73bcb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Construct Modifier"",
                    ""type"": ""Button"",
                    ""id"": ""7b0c8abf-29c9-4c24-ac9d-3c07a0627fd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7ff355b3-f958-4a14-ac5e-340223429ddd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8768f4b0-cce3-489c-999f-4aa54a74f346"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Delete Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c77dabcf-77b5-4d7e-8dbe-72482ea8caec"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Construct Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cf39890-7080-4b3a-a0d7-db5b29893b32"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Construct Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Quad
        m_Quad = asset.FindActionMap("Quad", throwIfNotFound: true);
        m_Quad_Forward = m_Quad.FindAction("Forward", throwIfNotFound: true);
        m_Quad_Backward = m_Quad.FindAction("Backward", throwIfNotFound: true);
        m_Quad_Left = m_Quad.FindAction("Left", throwIfNotFound: true);
        m_Quad_Right = m_Quad.FindAction("Right", throwIfNotFound: true);
        m_Quad_Drift = m_Quad.FindAction("Drift", throwIfNotFound: true);
        m_Quad_UseActualGadget = m_Quad.FindAction("UseActualGadget", throwIfNotFound: true);
        m_Quad_LookBackwards = m_Quad.FindAction("LookBackwards", throwIfNotFound: true);
        m_Quad_ChasisElevation = m_Quad.FindAction("ChasisElevation", throwIfNotFound: true);
        m_Quad_AlaDelta = m_Quad.FindAction("AlaDelta", throwIfNotFound: true);
        // Construction Menu
        m_ConstructionMenu = asset.FindActionMap("Construction Menu", throwIfNotFound: true);
        m_ConstructionMenu_DeleteModifier = m_ConstructionMenu.FindAction("Delete Modifier", throwIfNotFound: true);
        m_ConstructionMenu_ConstructModifier = m_ConstructionMenu.FindAction("Construct Modifier", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Quad
    private readonly InputActionMap m_Quad;
    private IQuadActions m_QuadActionsCallbackInterface;
    private readonly InputAction m_Quad_Forward;
    private readonly InputAction m_Quad_Backward;
    private readonly InputAction m_Quad_Left;
    private readonly InputAction m_Quad_Right;
    private readonly InputAction m_Quad_Drift;
    private readonly InputAction m_Quad_UseActualGadget;
    private readonly InputAction m_Quad_LookBackwards;
    private readonly InputAction m_Quad_ChasisElevation;
    private readonly InputAction m_Quad_AlaDelta;
    public struct QuadActions
    {
        private @QuadControls m_Wrapper;
        public QuadActions(@QuadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_Quad_Forward;
        public InputAction @Backward => m_Wrapper.m_Quad_Backward;
        public InputAction @Left => m_Wrapper.m_Quad_Left;
        public InputAction @Right => m_Wrapper.m_Quad_Right;
        public InputAction @Drift => m_Wrapper.m_Quad_Drift;
        public InputAction @UseActualGadget => m_Wrapper.m_Quad_UseActualGadget;
        public InputAction @LookBackwards => m_Wrapper.m_Quad_LookBackwards;
        public InputAction @ChasisElevation => m_Wrapper.m_Quad_ChasisElevation;
        public InputAction @AlaDelta => m_Wrapper.m_Quad_AlaDelta;
        public InputActionMap Get() { return m_Wrapper.m_Quad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QuadActions set) { return set.Get(); }
        public void SetCallbacks(IQuadActions instance)
        {
            if (m_Wrapper.m_QuadActionsCallbackInterface != null)
            {
                @Forward.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnBackward;
                @Left.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnRight;
                @Drift.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnDrift;
                @Drift.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnDrift;
                @Drift.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnDrift;
                @UseActualGadget.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseActualGadget;
                @UseActualGadget.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseActualGadget;
                @UseActualGadget.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseActualGadget;
                @LookBackwards.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnLookBackwards;
                @LookBackwards.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnLookBackwards;
                @LookBackwards.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnLookBackwards;
                @ChasisElevation.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @AlaDelta.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
            }
            m_Wrapper.m_QuadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Backward.started += instance.OnBackward;
                @Backward.performed += instance.OnBackward;
                @Backward.canceled += instance.OnBackward;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Drift.started += instance.OnDrift;
                @Drift.performed += instance.OnDrift;
                @Drift.canceled += instance.OnDrift;
                @UseActualGadget.started += instance.OnUseActualGadget;
                @UseActualGadget.performed += instance.OnUseActualGadget;
                @UseActualGadget.canceled += instance.OnUseActualGadget;
                @LookBackwards.started += instance.OnLookBackwards;
                @LookBackwards.performed += instance.OnLookBackwards;
                @LookBackwards.canceled += instance.OnLookBackwards;
                @ChasisElevation.started += instance.OnChasisElevation;
                @ChasisElevation.performed += instance.OnChasisElevation;
                @ChasisElevation.canceled += instance.OnChasisElevation;
                @AlaDelta.started += instance.OnAlaDelta;
                @AlaDelta.performed += instance.OnAlaDelta;
                @AlaDelta.canceled += instance.OnAlaDelta;
            }
        }
    }
    public QuadActions @Quad => new QuadActions(this);

    // Construction Menu
    private readonly InputActionMap m_ConstructionMenu;
    private IConstructionMenuActions m_ConstructionMenuActionsCallbackInterface;
    private readonly InputAction m_ConstructionMenu_DeleteModifier;
    private readonly InputAction m_ConstructionMenu_ConstructModifier;
    public struct ConstructionMenuActions
    {
        private @QuadControls m_Wrapper;
        public ConstructionMenuActions(@QuadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DeleteModifier => m_Wrapper.m_ConstructionMenu_DeleteModifier;
        public InputAction @ConstructModifier => m_Wrapper.m_ConstructionMenu_ConstructModifier;
        public InputActionMap Get() { return m_Wrapper.m_ConstructionMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConstructionMenuActions set) { return set.Get(); }
        public void SetCallbacks(IConstructionMenuActions instance)
        {
            if (m_Wrapper.m_ConstructionMenuActionsCallbackInterface != null)
            {
                @DeleteModifier.started -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnDeleteModifier;
                @DeleteModifier.performed -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnDeleteModifier;
                @DeleteModifier.canceled -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnDeleteModifier;
                @ConstructModifier.started -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnConstructModifier;
                @ConstructModifier.performed -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnConstructModifier;
                @ConstructModifier.canceled -= m_Wrapper.m_ConstructionMenuActionsCallbackInterface.OnConstructModifier;
            }
            m_Wrapper.m_ConstructionMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DeleteModifier.started += instance.OnDeleteModifier;
                @DeleteModifier.performed += instance.OnDeleteModifier;
                @DeleteModifier.canceled += instance.OnDeleteModifier;
                @ConstructModifier.started += instance.OnConstructModifier;
                @ConstructModifier.performed += instance.OnConstructModifier;
                @ConstructModifier.canceled += instance.OnConstructModifier;
            }
        }
    }
    public ConstructionMenuActions @ConstructionMenu => new ConstructionMenuActions(this);
    public interface IQuadActions
    {
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
        void OnUseActualGadget(InputAction.CallbackContext context);
        void OnLookBackwards(InputAction.CallbackContext context);
        void OnChasisElevation(InputAction.CallbackContext context);
        void OnAlaDelta(InputAction.CallbackContext context);
    }
    public interface IConstructionMenuActions
    {
        void OnDeleteModifier(InputAction.CallbackContext context);
        void OnConstructModifier(InputAction.CallbackContext context);
    }
}
