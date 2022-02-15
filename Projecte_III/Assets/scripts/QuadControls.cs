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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
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
                    ""groups"": ""Player1"",
                    ""action"": ""AlaDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""QuadP2"",
            ""id"": ""4dce1a56-1c80-46e4-b21d-40d51bfd1c15"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""dadd5d5b-4e0b-46d1-9dc0-9054808626dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Backward"",
                    ""type"": ""Value"",
                    ""id"": ""c2486feb-aa13-41d6-83f9-fb8047894511"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Value"",
                    ""id"": ""f5b0e3dc-ee83-4ee8-92ba-7cc43bab0955"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Value"",
                    ""id"": ""b9310908-9f7b-4f9a-95dd-ac5f41b4717c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Value"",
                    ""id"": ""2ca319fb-7227-4754-82ce-a98e7d9652b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseActualGadget"",
                    ""type"": ""Button"",
                    ""id"": ""f5208d7d-97c3-4a11-9e04-b060adc5c28e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookBackwards"",
                    ""type"": ""Value"",
                    ""id"": ""ff4b6c60-9caa-4bc4-abd6-58a3bc702bc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChasisElevation"",
                    ""type"": ""Button"",
                    ""id"": ""460acda9-958c-410c-ab46-353d3981a21f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AlaDelta"",
                    ""type"": ""Button"",
                    ""id"": ""ed3b67dd-4846-484a-a1df-70b6de44c966"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1a9d7756-fbbb-4c6d-ad3e-a8315b56d910"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c54a70e2-cb78-45e0-90ad-47bec2bad094"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Backward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4112e429-b0bf-4b6a-af28-12d6aef3dfd3"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37514ba3-da7d-4fb8-974a-9e96ea88722a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64fcfeed-3780-4d5d-9c8e-42a2e727d549"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd4afbe3-c953-4154-953c-2821f2c3300d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""UseActualGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c7c378c-1511-4634-89a3-ca9f22470eb1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""LookBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f39dd33-8220-449a-91dc-b6b594b56774"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""ChasisElevation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""464c25aa-8563-40fa-beee-eb9151c5ffd2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
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
    ""controlSchemes"": [
        {
            ""name"": ""Player1"",
            ""bindingGroup"": ""Player1"",
            ""devices"": []
        },
        {
            ""name"": ""Player2"",
            ""bindingGroup"": ""Player2"",
            ""devices"": []
        }
    ]
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
        // QuadP2
        m_QuadP2 = asset.FindActionMap("QuadP2", throwIfNotFound: true);
        m_QuadP2_Forward = m_QuadP2.FindAction("Forward", throwIfNotFound: true);
        m_QuadP2_Backward = m_QuadP2.FindAction("Backward", throwIfNotFound: true);
        m_QuadP2_Left = m_QuadP2.FindAction("Left", throwIfNotFound: true);
        m_QuadP2_Right = m_QuadP2.FindAction("Right", throwIfNotFound: true);
        m_QuadP2_Drift = m_QuadP2.FindAction("Drift", throwIfNotFound: true);
        m_QuadP2_UseActualGadget = m_QuadP2.FindAction("UseActualGadget", throwIfNotFound: true);
        m_QuadP2_LookBackwards = m_QuadP2.FindAction("LookBackwards", throwIfNotFound: true);
        m_QuadP2_ChasisElevation = m_QuadP2.FindAction("ChasisElevation", throwIfNotFound: true);
        m_QuadP2_AlaDelta = m_QuadP2.FindAction("AlaDelta", throwIfNotFound: true);
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

    // QuadP2
    private readonly InputActionMap m_QuadP2;
    private IQuadP2Actions m_QuadP2ActionsCallbackInterface;
    private readonly InputAction m_QuadP2_Forward;
    private readonly InputAction m_QuadP2_Backward;
    private readonly InputAction m_QuadP2_Left;
    private readonly InputAction m_QuadP2_Right;
    private readonly InputAction m_QuadP2_Drift;
    private readonly InputAction m_QuadP2_UseActualGadget;
    private readonly InputAction m_QuadP2_LookBackwards;
    private readonly InputAction m_QuadP2_ChasisElevation;
    private readonly InputAction m_QuadP2_AlaDelta;
    public struct QuadP2Actions
    {
        private @QuadControls m_Wrapper;
        public QuadP2Actions(@QuadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_QuadP2_Forward;
        public InputAction @Backward => m_Wrapper.m_QuadP2_Backward;
        public InputAction @Left => m_Wrapper.m_QuadP2_Left;
        public InputAction @Right => m_Wrapper.m_QuadP2_Right;
        public InputAction @Drift => m_Wrapper.m_QuadP2_Drift;
        public InputAction @UseActualGadget => m_Wrapper.m_QuadP2_UseActualGadget;
        public InputAction @LookBackwards => m_Wrapper.m_QuadP2_LookBackwards;
        public InputAction @ChasisElevation => m_Wrapper.m_QuadP2_ChasisElevation;
        public InputAction @AlaDelta => m_Wrapper.m_QuadP2_AlaDelta;
        public InputActionMap Get() { return m_Wrapper.m_QuadP2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QuadP2Actions set) { return set.Get(); }
        public void SetCallbacks(IQuadP2Actions instance)
        {
            if (m_Wrapper.m_QuadP2ActionsCallbackInterface != null)
            {
                @Forward.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnForward;
                @Backward.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnBackward;
                @Backward.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnBackward;
                @Backward.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnBackward;
                @Left.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnRight;
                @Drift.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnDrift;
                @Drift.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnDrift;
                @Drift.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnDrift;
                @UseActualGadget.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnUseActualGadget;
                @UseActualGadget.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnUseActualGadget;
                @UseActualGadget.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnUseActualGadget;
                @LookBackwards.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLookBackwards;
                @LookBackwards.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLookBackwards;
                @LookBackwards.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnLookBackwards;
                @ChasisElevation.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnChasisElevation;
                @AlaDelta.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnAlaDelta;
            }
            m_Wrapper.m_QuadP2ActionsCallbackInterface = instance;
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
    public QuadP2Actions @QuadP2 => new QuadP2Actions(this);

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
    private int m_Player1SchemeIndex = -1;
    public InputControlScheme Player1Scheme
    {
        get
        {
            if (m_Player1SchemeIndex == -1) m_Player1SchemeIndex = asset.FindControlSchemeIndex("Player1");
            return asset.controlSchemes[m_Player1SchemeIndex];
        }
    }
    private int m_Player2SchemeIndex = -1;
    public InputControlScheme Player2Scheme
    {
        get
        {
            if (m_Player2SchemeIndex == -1) m_Player2SchemeIndex = asset.FindControlSchemeIndex("Player2");
            return asset.controlSchemes[m_Player2SchemeIndex];
        }
    }
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
    public interface IQuadP2Actions
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
