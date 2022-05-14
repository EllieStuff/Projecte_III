// GENERATED AUTOMATICALLY FROM 'Assets/scripts/Misc/Inputs/QuadControls.inputactions'

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
                    ""name"": ""ActivateController"",
                    ""type"": ""Button"",
                    ""id"": ""ba62cb60-1e68-40a5-8c3c-955a12901a36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
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
                    ""name"": ""EnableRadialMenu"",
                    ""type"": ""Button"",
                    ""id"": ""83fc756a-9a53-4a35-b665-2493fde591c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChooseItemRight"",
                    ""type"": ""Value"",
                    ""id"": ""c6e2a57c-8d40-4cc3-b771-3ed9d07e62dd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChooseItemLeft"",
                    ""type"": ""Value"",
                    ""id"": ""2dfa6c52-0c96-43be-b22a-c14da96c4794"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChooseItemUp"",
                    ""type"": ""Value"",
                    ""id"": ""24d55a77-66ad-4eb1-bfdb-c70ebbea374a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChooseItemDown"",
                    ""type"": ""Value"",
                    ""id"": ""372df455-a740-4a03-8304-850a210c50a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChooseItemMouse"",
                    ""type"": ""Value"",
                    ""id"": ""0e21cede-741e-4272-8417-554d5ec16a3c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ConfirmChosenGadget"",
                    ""type"": ""Value"",
                    ""id"": ""2432c0d6-0af4-491a-918f-b73ac6447bec"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseChosenGadget"",
                    ""type"": ""Button"",
                    ""id"": ""18a3717e-c594-4bc1-bbf4-027d623bade9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""63cc4883-68c7-4bc8-a272-f559b2decb60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""91967d67-3696-4f71-b8c8-7279f9dc8227"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""d4f6ebf5-38b1-401e-a9e4-0fc74aaf4fa0"",
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
                },
                {
                    ""name"": ""ShootForward"",
                    ""type"": ""Button"",
                    ""id"": ""1aa5b9d0-690f-4e7c-8a14-31679fb40aae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootBackwards"",
                    ""type"": ""Button"",
                    ""id"": ""1df0e78a-6f07-4820-ace5-395dc1fcdde4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f2cf0e3e-84de-444d-b21a-18d5c45fc3ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootRight"",
                    ""type"": ""Button"",
                    ""id"": ""788bdb96-ddee-49aa-999f-5641e4c8af11"",
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
                    ""id"": ""f682c0ab-6282-4936-80d1-6d6785fced7f"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ChooseItemRight"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""722faaad-4800-4f0e-b11c-c30ea0062dcb"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseItemUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dd957aa-244e-476b-a588-e3c7bfd27693"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ChooseItemLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a27f3a91-d0ba-4232-b5c7-a2f8f5d1be8e"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseItemDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd334b8b-53aa-4305-9124-e1a02563c448"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseItemMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a617234-c963-4eba-9851-440ce266d55f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChosenGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93546f0a-5d3a-49e9-a23b-a03e1cd442c4"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ConfirmChosenGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21f87173-a435-424c-9663-fe2eaf3ea964"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0709a77f-765b-461e-bfa8-bcd7b6b21072"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dff272f-460c-4948-b068-9a4f1709646e"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateController"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bf001bc-b172-421b-a77c-36c9be0abc7b"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edf4c836-59be-4f53-a4ad-6ff7c2c40380"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableRadialMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fad30964-d4dc-4650-a5da-3909c606e34b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""UseChosenGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c584fdd9-1d0f-4884-b9b5-164e6bf0f467"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""UseChosenGadget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a911d508-e42a-42f0-9a02-c02fcf3670a3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""855c763d-2112-4307-ae89-4cfe68da83c0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c81bc6e-292c-4093-af72-ca5f75085e66"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fca1ade0-23bf-44cf-a43f-790e603ef829"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3687b39-e506-414a-9949-200ea855a7b8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63bd6f0d-ce9e-4867-b450-018f29da7f47"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0194434d-25e5-45b4-bd7d-d8d5c45021e4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a773c1e-d791-423d-bca7-a32fd9edf254"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18635467-0ed6-47e2-a8d6-3c5cfcd716c5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37ad6bdf-e817-494a-907d-ea4ef6e88f92"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd215e1b-2760-4009-a88c-1d05b55030e2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""162936f7-1c3d-481e-bd6c-d6f39bb2de94"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player1"",
                    ""action"": ""ShootRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90d5e1f9-228e-4336-9190-5af548f2d07f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36086cd8-ece8-44b5-8957-506187cdb9eb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05b3846e-e4e9-4247-836e-c19bd9867dc8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efc6f769-3f00-4906-9664-c391ba342ed9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
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
                },
                {
                    ""name"": ""ShootForward"",
                    ""type"": ""Button"",
                    ""id"": ""97d89aff-4099-4b69-b553-edc66ba3d347"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootBackwards"",
                    ""type"": ""Button"",
                    ""id"": ""96e2a37a-d0c0-4f04-a61c-495f55f736f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootLeft"",
                    ""type"": ""Button"",
                    ""id"": ""c22d0480-5ec7-4f77-abe9-39161acd0160"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootRight"",
                    ""type"": ""Button"",
                    ""id"": ""0a043466-cebb-4e2c-bb85-a7c15427baee"",
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
                    ""path"": ""<Gamepad>/leftShoulder"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""2f4073c4-7d63-4080-87aa-acb1dc02ea3d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""ShootForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c074937-6337-42e3-ab31-bc448a4a49fe"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""ShootBackwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbcf836b-985f-4bc7-be61-89ee112f6678"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""ShootLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""747d5653-bb89-4cea-bf0d-e53b3c06f0e4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player2"",
                    ""action"": ""ShootRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuildingMenu"",
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
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Value"",
                    ""id"": ""793dfda1-9ff8-4a28-a78b-dc8388c91855"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Value"",
                    ""id"": ""6e067de4-9c54-4bf0-863c-6e108fb22ea1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Value"",
                    ""id"": ""11fb762e-a285-44d6-9c6e-c78cdecea168"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Value"",
                    ""id"": ""e4e656ca-2a29-45e3-9577-a203c3b572d0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Value"",
                    ""id"": ""98a9ee8a-54bb-46da-9953-62b0388d57dd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Decline"",
                    ""type"": ""Value"",
                    ""id"": ""1e79a2d4-3490-4f31-bfa3-75774e6df337"",
                    ""expectedControlType"": """",
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
                    ""id"": ""187e51c2-1238-452b-b3f5-48d540dfa2b3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ce523f8-8a89-49a0-90ed-cf17a1d391ce"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67e242f7-de9a-4fb1-b475-bece8c8d92f3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23362a7b-0a61-4f61-83ce-6197f08c7db6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""558efbbc-3f7b-492e-803c-d20c45a98a36"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4825827-e3f7-4c76-9bf3-c6e1806aff3a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72706a0d-4b2f-4b9c-9541-91c78fa393c8"",
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
                    ""id"": ""3a01f28f-9a62-4cbf-843a-ca7105ac0e4f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24666659-d470-4586-b0fb-9a3e6cfa4b07"",
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
                    ""id"": ""95fa6fd2-75f5-4850-93f0-b18a012ba5ea"",
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
                    ""id"": ""808207f8-d201-46cf-8b45-5881d532d5f7"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47d86e41-6668-43e8-8959-4d9e0597e46a"",
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
                    ""id"": ""7f53fd89-039f-4c50-a10c-571923e78355"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64082ec0-6508-4748-ac5c-fa57e3b1fc56"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""846adecc-cab8-43a2-87ae-7464fa9829e0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decline"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72e59ece-a63d-4b5d-8f3e-c9034bc511ad"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decline"",
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
        m_Quad_ActivateController = m_Quad.FindAction("ActivateController", throwIfNotFound: true);
        m_Quad_Forward = m_Quad.FindAction("Forward", throwIfNotFound: true);
        m_Quad_Backward = m_Quad.FindAction("Backward", throwIfNotFound: true);
        m_Quad_Left = m_Quad.FindAction("Left", throwIfNotFound: true);
        m_Quad_Right = m_Quad.FindAction("Right", throwIfNotFound: true);
        m_Quad_Drift = m_Quad.FindAction("Drift", throwIfNotFound: true);
        m_Quad_EnableRadialMenu = m_Quad.FindAction("EnableRadialMenu", throwIfNotFound: true);
        m_Quad_ChooseItemRight = m_Quad.FindAction("ChooseItemRight", throwIfNotFound: true);
        m_Quad_ChooseItemLeft = m_Quad.FindAction("ChooseItemLeft", throwIfNotFound: true);
        m_Quad_ChooseItemUp = m_Quad.FindAction("ChooseItemUp", throwIfNotFound: true);
        m_Quad_ChooseItemDown = m_Quad.FindAction("ChooseItemDown", throwIfNotFound: true);
        m_Quad_ChooseItemMouse = m_Quad.FindAction("ChooseItemMouse", throwIfNotFound: true);
        m_Quad_ConfirmChosenGadget = m_Quad.FindAction("ConfirmChosenGadget", throwIfNotFound: true);
        m_Quad_UseChosenGadget = m_Quad.FindAction("UseChosenGadget", throwIfNotFound: true);
        m_Quad_Start = m_Quad.FindAction("Start", throwIfNotFound: true);
        m_Quad_Escape = m_Quad.FindAction("Escape", throwIfNotFound: true);
        m_Quad_Exit = m_Quad.FindAction("Exit", throwIfNotFound: true);
        m_Quad_ChasisElevation = m_Quad.FindAction("ChasisElevation", throwIfNotFound: true);
        m_Quad_AlaDelta = m_Quad.FindAction("AlaDelta", throwIfNotFound: true);
        m_Quad_ShootForward = m_Quad.FindAction("ShootForward", throwIfNotFound: true);
        m_Quad_ShootBackwards = m_Quad.FindAction("ShootBackwards", throwIfNotFound: true);
        m_Quad_ShootLeft = m_Quad.FindAction("ShootLeft", throwIfNotFound: true);
        m_Quad_ShootRight = m_Quad.FindAction("ShootRight", throwIfNotFound: true);
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
        m_QuadP2_ShootForward = m_QuadP2.FindAction("ShootForward", throwIfNotFound: true);
        m_QuadP2_ShootBackwards = m_QuadP2.FindAction("ShootBackwards", throwIfNotFound: true);
        m_QuadP2_ShootLeft = m_QuadP2.FindAction("ShootLeft", throwIfNotFound: true);
        m_QuadP2_ShootRight = m_QuadP2.FindAction("ShootRight", throwIfNotFound: true);
        // BuildingMenu
        m_BuildingMenu = asset.FindActionMap("BuildingMenu", throwIfNotFound: true);
        m_BuildingMenu_DeleteModifier = m_BuildingMenu.FindAction("Delete Modifier", throwIfNotFound: true);
        m_BuildingMenu_ConstructModifier = m_BuildingMenu.FindAction("Construct Modifier", throwIfNotFound: true);
        m_BuildingMenu_Up = m_BuildingMenu.FindAction("Up", throwIfNotFound: true);
        m_BuildingMenu_Down = m_BuildingMenu.FindAction("Down", throwIfNotFound: true);
        m_BuildingMenu_Right = m_BuildingMenu.FindAction("Right", throwIfNotFound: true);
        m_BuildingMenu_Left = m_BuildingMenu.FindAction("Left", throwIfNotFound: true);
        m_BuildingMenu_Accept = m_BuildingMenu.FindAction("Accept", throwIfNotFound: true);
        m_BuildingMenu_Decline = m_BuildingMenu.FindAction("Decline", throwIfNotFound: true);
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
    private readonly InputAction m_Quad_ActivateController;
    private readonly InputAction m_Quad_Forward;
    private readonly InputAction m_Quad_Backward;
    private readonly InputAction m_Quad_Left;
    private readonly InputAction m_Quad_Right;
    private readonly InputAction m_Quad_Drift;
    private readonly InputAction m_Quad_EnableRadialMenu;
    private readonly InputAction m_Quad_ChooseItemRight;
    private readonly InputAction m_Quad_ChooseItemLeft;
    private readonly InputAction m_Quad_ChooseItemUp;
    private readonly InputAction m_Quad_ChooseItemDown;
    private readonly InputAction m_Quad_ChooseItemMouse;
    private readonly InputAction m_Quad_ConfirmChosenGadget;
    private readonly InputAction m_Quad_UseChosenGadget;
    private readonly InputAction m_Quad_Start;
    private readonly InputAction m_Quad_Escape;
    private readonly InputAction m_Quad_Exit;
    private readonly InputAction m_Quad_ChasisElevation;
    private readonly InputAction m_Quad_AlaDelta;
    private readonly InputAction m_Quad_ShootForward;
    private readonly InputAction m_Quad_ShootBackwards;
    private readonly InputAction m_Quad_ShootLeft;
    private readonly InputAction m_Quad_ShootRight;
    public struct QuadActions
    {
        private @QuadControls m_Wrapper;
        public QuadActions(@QuadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActivateController => m_Wrapper.m_Quad_ActivateController;
        public InputAction @Forward => m_Wrapper.m_Quad_Forward;
        public InputAction @Backward => m_Wrapper.m_Quad_Backward;
        public InputAction @Left => m_Wrapper.m_Quad_Left;
        public InputAction @Right => m_Wrapper.m_Quad_Right;
        public InputAction @Drift => m_Wrapper.m_Quad_Drift;
        public InputAction @EnableRadialMenu => m_Wrapper.m_Quad_EnableRadialMenu;
        public InputAction @ChooseItemRight => m_Wrapper.m_Quad_ChooseItemRight;
        public InputAction @ChooseItemLeft => m_Wrapper.m_Quad_ChooseItemLeft;
        public InputAction @ChooseItemUp => m_Wrapper.m_Quad_ChooseItemUp;
        public InputAction @ChooseItemDown => m_Wrapper.m_Quad_ChooseItemDown;
        public InputAction @ChooseItemMouse => m_Wrapper.m_Quad_ChooseItemMouse;
        public InputAction @ConfirmChosenGadget => m_Wrapper.m_Quad_ConfirmChosenGadget;
        public InputAction @UseChosenGadget => m_Wrapper.m_Quad_UseChosenGadget;
        public InputAction @Start => m_Wrapper.m_Quad_Start;
        public InputAction @Escape => m_Wrapper.m_Quad_Escape;
        public InputAction @Exit => m_Wrapper.m_Quad_Exit;
        public InputAction @ChasisElevation => m_Wrapper.m_Quad_ChasisElevation;
        public InputAction @AlaDelta => m_Wrapper.m_Quad_AlaDelta;
        public InputAction @ShootForward => m_Wrapper.m_Quad_ShootForward;
        public InputAction @ShootBackwards => m_Wrapper.m_Quad_ShootBackwards;
        public InputAction @ShootLeft => m_Wrapper.m_Quad_ShootLeft;
        public InputAction @ShootRight => m_Wrapper.m_Quad_ShootRight;
        public InputActionMap Get() { return m_Wrapper.m_Quad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QuadActions set) { return set.Get(); }
        public void SetCallbacks(IQuadActions instance)
        {
            if (m_Wrapper.m_QuadActionsCallbackInterface != null)
            {
                @ActivateController.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnActivateController;
                @ActivateController.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnActivateController;
                @ActivateController.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnActivateController;
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
                @EnableRadialMenu.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnEnableRadialMenu;
                @EnableRadialMenu.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnEnableRadialMenu;
                @EnableRadialMenu.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnEnableRadialMenu;
                @ChooseItemRight.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemRight;
                @ChooseItemRight.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemRight;
                @ChooseItemRight.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemRight;
                @ChooseItemLeft.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemLeft;
                @ChooseItemLeft.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemLeft;
                @ChooseItemLeft.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemLeft;
                @ChooseItemUp.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemUp;
                @ChooseItemUp.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemUp;
                @ChooseItemUp.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemUp;
                @ChooseItemDown.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemDown;
                @ChooseItemDown.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemDown;
                @ChooseItemDown.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemDown;
                @ChooseItemMouse.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemMouse;
                @ChooseItemMouse.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemMouse;
                @ChooseItemMouse.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChooseItemMouse;
                @ConfirmChosenGadget.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnConfirmChosenGadget;
                @ConfirmChosenGadget.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnConfirmChosenGadget;
                @ConfirmChosenGadget.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnConfirmChosenGadget;
                @UseChosenGadget.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseChosenGadget;
                @UseChosenGadget.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseChosenGadget;
                @UseChosenGadget.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnUseChosenGadget;
                @Start.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnStart;
                @Escape.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnEscape;
                @Exit.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnExit;
                @ChasisElevation.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @ChasisElevation.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnChasisElevation;
                @AlaDelta.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
                @AlaDelta.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnAlaDelta;
                @ShootForward.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootForward;
                @ShootForward.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootForward;
                @ShootForward.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootForward;
                @ShootBackwards.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootBackwards;
                @ShootBackwards.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootBackwards;
                @ShootBackwards.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootBackwards;
                @ShootLeft.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootLeft;
                @ShootLeft.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootLeft;
                @ShootLeft.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootLeft;
                @ShootRight.started -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootRight;
                @ShootRight.performed -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootRight;
                @ShootRight.canceled -= m_Wrapper.m_QuadActionsCallbackInterface.OnShootRight;
            }
            m_Wrapper.m_QuadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ActivateController.started += instance.OnActivateController;
                @ActivateController.performed += instance.OnActivateController;
                @ActivateController.canceled += instance.OnActivateController;
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
                @EnableRadialMenu.started += instance.OnEnableRadialMenu;
                @EnableRadialMenu.performed += instance.OnEnableRadialMenu;
                @EnableRadialMenu.canceled += instance.OnEnableRadialMenu;
                @ChooseItemRight.started += instance.OnChooseItemRight;
                @ChooseItemRight.performed += instance.OnChooseItemRight;
                @ChooseItemRight.canceled += instance.OnChooseItemRight;
                @ChooseItemLeft.started += instance.OnChooseItemLeft;
                @ChooseItemLeft.performed += instance.OnChooseItemLeft;
                @ChooseItemLeft.canceled += instance.OnChooseItemLeft;
                @ChooseItemUp.started += instance.OnChooseItemUp;
                @ChooseItemUp.performed += instance.OnChooseItemUp;
                @ChooseItemUp.canceled += instance.OnChooseItemUp;
                @ChooseItemDown.started += instance.OnChooseItemDown;
                @ChooseItemDown.performed += instance.OnChooseItemDown;
                @ChooseItemDown.canceled += instance.OnChooseItemDown;
                @ChooseItemMouse.started += instance.OnChooseItemMouse;
                @ChooseItemMouse.performed += instance.OnChooseItemMouse;
                @ChooseItemMouse.canceled += instance.OnChooseItemMouse;
                @ConfirmChosenGadget.started += instance.OnConfirmChosenGadget;
                @ConfirmChosenGadget.performed += instance.OnConfirmChosenGadget;
                @ConfirmChosenGadget.canceled += instance.OnConfirmChosenGadget;
                @UseChosenGadget.started += instance.OnUseChosenGadget;
                @UseChosenGadget.performed += instance.OnUseChosenGadget;
                @UseChosenGadget.canceled += instance.OnUseChosenGadget;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @ChasisElevation.started += instance.OnChasisElevation;
                @ChasisElevation.performed += instance.OnChasisElevation;
                @ChasisElevation.canceled += instance.OnChasisElevation;
                @AlaDelta.started += instance.OnAlaDelta;
                @AlaDelta.performed += instance.OnAlaDelta;
                @AlaDelta.canceled += instance.OnAlaDelta;
                @ShootForward.started += instance.OnShootForward;
                @ShootForward.performed += instance.OnShootForward;
                @ShootForward.canceled += instance.OnShootForward;
                @ShootBackwards.started += instance.OnShootBackwards;
                @ShootBackwards.performed += instance.OnShootBackwards;
                @ShootBackwards.canceled += instance.OnShootBackwards;
                @ShootLeft.started += instance.OnShootLeft;
                @ShootLeft.performed += instance.OnShootLeft;
                @ShootLeft.canceled += instance.OnShootLeft;
                @ShootRight.started += instance.OnShootRight;
                @ShootRight.performed += instance.OnShootRight;
                @ShootRight.canceled += instance.OnShootRight;
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
    private readonly InputAction m_QuadP2_ShootForward;
    private readonly InputAction m_QuadP2_ShootBackwards;
    private readonly InputAction m_QuadP2_ShootLeft;
    private readonly InputAction m_QuadP2_ShootRight;
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
        public InputAction @ShootForward => m_Wrapper.m_QuadP2_ShootForward;
        public InputAction @ShootBackwards => m_Wrapper.m_QuadP2_ShootBackwards;
        public InputAction @ShootLeft => m_Wrapper.m_QuadP2_ShootLeft;
        public InputAction @ShootRight => m_Wrapper.m_QuadP2_ShootRight;
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
                @ShootForward.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootForward;
                @ShootForward.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootForward;
                @ShootForward.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootForward;
                @ShootBackwards.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootBackwards;
                @ShootBackwards.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootBackwards;
                @ShootBackwards.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootBackwards;
                @ShootLeft.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootLeft;
                @ShootLeft.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootLeft;
                @ShootLeft.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootLeft;
                @ShootRight.started -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootRight;
                @ShootRight.performed -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootRight;
                @ShootRight.canceled -= m_Wrapper.m_QuadP2ActionsCallbackInterface.OnShootRight;
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
                @ShootForward.started += instance.OnShootForward;
                @ShootForward.performed += instance.OnShootForward;
                @ShootForward.canceled += instance.OnShootForward;
                @ShootBackwards.started += instance.OnShootBackwards;
                @ShootBackwards.performed += instance.OnShootBackwards;
                @ShootBackwards.canceled += instance.OnShootBackwards;
                @ShootLeft.started += instance.OnShootLeft;
                @ShootLeft.performed += instance.OnShootLeft;
                @ShootLeft.canceled += instance.OnShootLeft;
                @ShootRight.started += instance.OnShootRight;
                @ShootRight.performed += instance.OnShootRight;
                @ShootRight.canceled += instance.OnShootRight;
            }
        }
    }
    public QuadP2Actions @QuadP2 => new QuadP2Actions(this);

    // BuildingMenu
    private readonly InputActionMap m_BuildingMenu;
    private IBuildingMenuActions m_BuildingMenuActionsCallbackInterface;
    private readonly InputAction m_BuildingMenu_DeleteModifier;
    private readonly InputAction m_BuildingMenu_ConstructModifier;
    private readonly InputAction m_BuildingMenu_Up;
    private readonly InputAction m_BuildingMenu_Down;
    private readonly InputAction m_BuildingMenu_Right;
    private readonly InputAction m_BuildingMenu_Left;
    private readonly InputAction m_BuildingMenu_Accept;
    private readonly InputAction m_BuildingMenu_Decline;
    public struct BuildingMenuActions
    {
        private @QuadControls m_Wrapper;
        public BuildingMenuActions(@QuadControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DeleteModifier => m_Wrapper.m_BuildingMenu_DeleteModifier;
        public InputAction @ConstructModifier => m_Wrapper.m_BuildingMenu_ConstructModifier;
        public InputAction @Up => m_Wrapper.m_BuildingMenu_Up;
        public InputAction @Down => m_Wrapper.m_BuildingMenu_Down;
        public InputAction @Right => m_Wrapper.m_BuildingMenu_Right;
        public InputAction @Left => m_Wrapper.m_BuildingMenu_Left;
        public InputAction @Accept => m_Wrapper.m_BuildingMenu_Accept;
        public InputAction @Decline => m_Wrapper.m_BuildingMenu_Decline;
        public InputActionMap Get() { return m_Wrapper.m_BuildingMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildingMenuActions set) { return set.Get(); }
        public void SetCallbacks(IBuildingMenuActions instance)
        {
            if (m_Wrapper.m_BuildingMenuActionsCallbackInterface != null)
            {
                @DeleteModifier.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDeleteModifier;
                @DeleteModifier.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDeleteModifier;
                @DeleteModifier.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDeleteModifier;
                @ConstructModifier.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnConstructModifier;
                @ConstructModifier.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnConstructModifier;
                @ConstructModifier.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnConstructModifier;
                @Up.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDown;
                @Right.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnLeft;
                @Accept.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnAccept;
                @Decline.started -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDecline;
                @Decline.performed -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDecline;
                @Decline.canceled -= m_Wrapper.m_BuildingMenuActionsCallbackInterface.OnDecline;
            }
            m_Wrapper.m_BuildingMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DeleteModifier.started += instance.OnDeleteModifier;
                @DeleteModifier.performed += instance.OnDeleteModifier;
                @DeleteModifier.canceled += instance.OnDeleteModifier;
                @ConstructModifier.started += instance.OnConstructModifier;
                @ConstructModifier.performed += instance.OnConstructModifier;
                @ConstructModifier.canceled += instance.OnConstructModifier;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Decline.started += instance.OnDecline;
                @Decline.performed += instance.OnDecline;
                @Decline.canceled += instance.OnDecline;
            }
        }
    }
    public BuildingMenuActions @BuildingMenu => new BuildingMenuActions(this);
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
        void OnActivateController(InputAction.CallbackContext context);
        void OnForward(InputAction.CallbackContext context);
        void OnBackward(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
        void OnEnableRadialMenu(InputAction.CallbackContext context);
        void OnChooseItemRight(InputAction.CallbackContext context);
        void OnChooseItemLeft(InputAction.CallbackContext context);
        void OnChooseItemUp(InputAction.CallbackContext context);
        void OnChooseItemDown(InputAction.CallbackContext context);
        void OnChooseItemMouse(InputAction.CallbackContext context);
        void OnConfirmChosenGadget(InputAction.CallbackContext context);
        void OnUseChosenGadget(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnChasisElevation(InputAction.CallbackContext context);
        void OnAlaDelta(InputAction.CallbackContext context);
        void OnShootForward(InputAction.CallbackContext context);
        void OnShootBackwards(InputAction.CallbackContext context);
        void OnShootLeft(InputAction.CallbackContext context);
        void OnShootRight(InputAction.CallbackContext context);
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
        void OnShootForward(InputAction.CallbackContext context);
        void OnShootBackwards(InputAction.CallbackContext context);
        void OnShootLeft(InputAction.CallbackContext context);
        void OnShootRight(InputAction.CallbackContext context);
    }
    public interface IBuildingMenuActions
    {
        void OnDeleteModifier(InputAction.CallbackContext context);
        void OnConstructModifier(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnDecline(InputAction.CallbackContext context);
    }
}
