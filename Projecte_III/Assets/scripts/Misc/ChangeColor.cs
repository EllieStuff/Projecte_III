using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Material currentColor;

    public int currentValue;

    static List<Material> colorList = new List<Material>();
    [SerializeField] VehicleTriggerAndCollisionEvents player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("List size: " + colorList.Count);
        if(colorList.Count <= 0)
        {
            Material[] mats = Resources.LoadAll<Material>("Materials/CarMaterials");
            for (int i = 0; i < mats.Length; i++)
            {
                colorList.Add(mats[i]);
            }
        }

        currentColor = colorList[0];
        colorList.RemoveAt(0);

        player.DefaultMaterial = currentColor;
    }

    public void SetNewColor(int _direction)
    {
        colorList.Add(currentColor);

        if(_direction > 0)
        {
            currentColor = colorList[colorList.Count - 1];
            colorList.RemoveAt(colorList.Count - 1);
        }
        else if(_direction < 0)
        {
            currentColor = colorList[0];
            colorList.RemoveAt(0);
        }

        player.DefaultMaterial = currentColor;
    }
}
