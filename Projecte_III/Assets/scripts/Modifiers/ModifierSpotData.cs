using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierSpotData : MonoBehaviour
{
    enum ModifierTypes { AlaDelta, Floater, OilGun, PaintGun, Plunger, Umbrella}

    [SerializeField] private ModifierTypes[] availableTypes;

    public bool IsAvailable(string type)
    {
        for (int i = 0; i < availableTypes.Length; i++)
        {
            if (type == availableTypes[i].ToString())
            {
                return true;
            }
        }

        return false;
    }
}
