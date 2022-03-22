using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierSpotData : MonoBehaviour
{
    enum ModifierTypes { AlaDelta, Floater, OilGun, PaintGun, Plunger, Umbrella}

    [SerializeField] private ModifierTypes[] availableTypes;

    Color mainColor, errorColor, correctColor;

    private void Start()
    {
        mainColor = GetComponent<MeshRenderer>().material.color;

        errorColor = Color.red * 0.8f;
        errorColor.a = 1;

        correctColor = Color.green * 0.8f;
        correctColor.a = 1;
    }

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

    public void ResetAlpha()
    {
        Material mat = GetComponent<MeshRenderer>().material;

        Color tmp = mat.color;
        tmp.a = 1;
        mat.color = tmp;

        GetComponent<MeshRenderer>().material = mat;
    }

    public void  SetColor(string tag, bool active)
    {
        Material mat = GetComponent<MeshRenderer>().material;
        float a = mat.color.a;
        Color tmp = mainColor;
        if(active)
        {
            Debug.Log("IsAvailable color");
            if(IsAvailable(tag))
                tmp = correctColor;
            else
                tmp = errorColor;
        }

        tmp.a = a;
        mat.color = tmp;

        GetComponent<MeshRenderer>().material = mat;
    }
}
