using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGradientMaterials : MonoBehaviour
{
    [System.Serializable]
    public struct ColorData
    {
        public string colorName;
        public Color color;
    }

    public bool gradient = false;

    [SerializeField] ColorData[] UsedColors;

    static ColorData[] sharedUsedColors;

    private void Start()
    {
        if(sharedUsedColors == null)
        {
            sharedUsedColors = new ColorData[UsedColors.Length];
            for (int i = 0; i < UsedColors.Length; i++)
            {
                sharedUsedColors[i] = UsedColors[i];
            }
        }
        
    }

    static public Color GetColor(string _colorName)
    {
        foreach (var color in sharedUsedColors)
        {
            if(_colorName.Contains(color.colorName))
            {
                return color.color;
            }
        }
        return Color.black;
    }

    static public Color GetColor(ref int _id)
    {
        if (_id >= sharedUsedColors.Length) _id = 0;
        return sharedUsedColors[_id].color;
    }
}
