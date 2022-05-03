using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseGradientMaterials : MonoBehaviour
{
    [System.Serializable]
    struct ColorData
    {
        public string colorName;
        public Color color;
    }

    public bool gradient = false;

    [SerializeField] ColorData[] UsedColors;

    public Color GetColor(string _colorName)
    {
        foreach (var color in UsedColors)
        {
            if(_colorName.Contains(color.colorName))
            {
                return color.color;
            }
        }
        return Color.black;
    }
}
