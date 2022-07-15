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
        public PercentageModificators[] modificator = { PercentageModificators.NONE };
        public bool invertModificator = false;
    }

    [Space(10)]
    public ModifierData plunger;

    [Space(10)]
    public ModifierData umbrella;

    [Space(10)]
    public ModifierData oil;

    [Space(10)]
    public ModifierData paintGun;

    [Space(10)]
    public ModifierData saltoBomba;

    [Space(10)]
    public ModifierData boost;
}
