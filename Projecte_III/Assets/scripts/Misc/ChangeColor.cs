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

        int _rand = Random.Range(0, colorList.Count);

        currentColor = colorList[_rand];
        colorList.RemoveAt(_rand);

        player.DefaultMaterial = currentColor;
    }

    public void SetNewColor(int _direction)
    {
        Material _currentColor = null;

        if(_direction > 0)
        {
            _currentColor = colorList[colorList.Count - 1];
            colorList.RemoveAt(colorList.Count - 1);
            colorList.Insert(0, currentColor);
        }
        else if(_direction < 0)
        {
            _currentColor = colorList[0];
            colorList.RemoveAt(0);
            colorList.Add(currentColor);
        }
        currentColor = _currentColor;

        player.DefaultMaterial = currentColor;
    }
}
