// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Misc/Inputs/QuadControls.inputactions'

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
                    ""path"": ""<Gamepad>/buttonSouth"",
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
                    ""id"": ""4263d938-3c97-4846-8754-5b4fd59c04c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Value"",
                    ""id"": ""7f143eba-6ba8-408d-bfb6-c7774772dfda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Value"",
                    ""id"": ""43e1857d-870f-4c3a-a49b-2c400d6a81ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Value"",
                    ""id"": ""891df634-73be-4c49-ab2e-a5d2a5671ff7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Value"",
                    ""id"": ""59550993-d9d1-42dd-a87a-6ea0dfd57a1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Decline"",
                    ""type"": ""Value"",
                    ""id"": ""bcd9761b-acd7-4561-830b-277789b5a75b"",
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
                    ""id"": ""ea2f1680-5e96-4dc2-a92f-b2d2eff96fde"",
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
                    ""id"": ""00c52104-2b4f-4788-ac2b-95c59ee6df76"",
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
                    ""id"": ""04073a58-141c-4746-8415-0e434b5bea91"",
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
                    ""id"": ""ea430d8c-aa56-4b79-aba4-dc365238e1e4"",
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
                    ""id"": ""69961d87-e817-4063-9454-c979fd2ead41"",
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
                    ""id"": ""bdaaa40b-b4b4-4c9a-bd78-1d8cad2c9fc1"",
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
                    ""id"": ""4d18b4c3-de7b-4829-873d-d349a157b47a"",
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
                    ""id"": ""8edbed57-9b24-4e6a-8cbb-877116a63a2e"",
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
                    ""id"": ""9965813f-7e7c-4e4f-97da-9ab2f679adc4"",
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
                    ""id"": ""24964698-1f8f-4278-9c95-e7fe7addd6ce"",
                    ""path"": ""<Gamepad>/buttonEast"",
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
        m_Quad_ChasisElevation = m_Quad.FindAction("ChasisElevation", throwIfNotFound: true);
        m_Quad_AlaDelta = m_Quad.FindAction("AlaDelta", throwIfNotFound: true);
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
    private readonly InputAction m_Quad_ChasisElevation;
    private readonly InputAction m_Quad_AlaDelta;
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
        void OnChasisElevation(InputAction.CallbackContext context);
        void OnAlaDelta(InputAction.CallbackContext context);
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
