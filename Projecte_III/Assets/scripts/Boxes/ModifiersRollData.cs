using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Modifiers_Data", menuName = "ScriptableObjects/ModifiersRollData", order = 1)]
public class ModifiersRollData : ScriptableObject
{
    public enum PercentageModificators { LESS_LIFES, MORE_POSITION, NONE };

    [System.Serializable]
    public class ModifierData
    {
        public string ModifierName = "";

        public PercentageModificators[] modificator = { PercentageModificators.NONE };
        public bool invertModificator = false;
    }

    public ModifierData[] Modifiers;
}
