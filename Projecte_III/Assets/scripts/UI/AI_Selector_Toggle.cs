using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Selector_Toggle : SettingsOptionButton
{
    Toggle AI_Selector;
    [SerializeField] ColorsAndAISelector AI_selector_logic;
    [SerializeField] GlobalMenuInputs inputs;

    private void Start()
    {
        AI_Selector = GetComponent<Toggle>();
    }

    private void Update()
    {
        if (inputs.SelectAIsPressed) Interact_Accept(true);
    }

    public override void Interact_Accept(bool _calledFromScript = false)
    {
        base.Interact_Accept(_calledFromScript);

        AI_Selector.isOn = !AI_Selector.isOn;

        AI_selector_logic.SetAI_Active(AI_Selector);
    }
}
