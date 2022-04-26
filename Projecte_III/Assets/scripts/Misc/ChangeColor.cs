using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    KeyValuePair<int, Material> currentColor;

    public int currentValue;

    static List<KeyValuePair<int, Material>> colorList = new List<KeyValuePair<int, Material>>();
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
                colorList.Add(new KeyValuePair<int, Material>(i, mats[i]));
            }
        }

        currentColor = colorList[0];
        colorList.RemoveAt(0);

        player.DefaultMaterial = currentColor.Value;
        currentValue = currentColor.Key;
    }

    public void SetNewColor(int _direction)
    {
        int _index = currentColor.Key + _direction;
        if (_index < 0) _index += colorList.Count;
        else if (_index >= colorList.Count) _index = 0;

        KeyValuePair<int, Material> _currentColor = colorList[_index];
        colorList.RemoveAt(_index);

        if (currentColor.Key > colorList[colorList.Count - 1].Key) colorList.Add(currentColor);
        else
        {
            for (int i = 0; i < colorList.Count; i++)
            {
                if (currentColor.Key < colorList[i].Key)
                {
                    colorList.Insert(i, currentColor);
                    break;
                }
            }
        }
       
        Debug.Log(colorList[_index].Value);

        currentColor = _currentColor;

        player.DefaultMaterial = currentColor.Value;
    }
}
