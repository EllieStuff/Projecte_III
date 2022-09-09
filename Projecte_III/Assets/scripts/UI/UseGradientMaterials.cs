using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] Color AI_Color = Color.gray;
    static ColorData[] sharedUsedColors;

    PlayersManager playersManager;

    static bool activeAI = false;
    static Color shared_AI_Color;

    private void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        if (sharedUsedColors == null)
        {
            sharedUsedColors = new ColorData[UsedColors.Length];
            for (int i = 0; i < UsedColors.Length; i++)
            {
                sharedUsedColors[i] = UsedColors[i];
            }
        }

        if (shared_AI_Color == null) shared_AI_Color = AI_Color;
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

    static public Color GetAIBgColor()
    {
        return Color.gray;
    }

    public void SetAI_Active(UnityEngine.UI.Toggle _toggle)
    {
        Debug.Log("Num of players: " + playersManager.numOfPlayers.ToString());
        //Activate AIs
        if(_toggle.isOn)
        {
            for (int i = playersManager.numOfPlayers; i < playersManager.players.Length; i++)
            {
                transform.GetChild(i).GetComponent<ChangeColor>().enabled = true;
                Transform playerTrans = playersManager.GetPlayer(i);
                if (playerTrans == null)
                {
                    playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
                    playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = true;
                }
                else
                {
                    playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = true;
                }
                TextMeshPro tmPro = playersManager.GetPlayer(i).Find("vehicleChasis").Find("PlayerNumText").GetComponent<TextMeshPro>();
                tmPro.text = "CPU";
                tmPro.fontSize = 22;
                tmPro.transform.localPosition =
                    new Vector3(tmPro.transform.localPosition.x, tmPro.transform.localPosition.y + 0.1f, tmPro.transform.localPosition.z - 0.1f);
            }
        }
        //Disactivate AIs
        else
        {
            for (int i = playersManager.numOfPlayers; i < playersManager.players.Length; i++)
            {
                transform.GetChild(i).GetComponent<ChangeColor>().enabled = false;
                Transform playerTrans = playersManager.GetPlayer(i);
                if (playerTrans == null)
                {
                    playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
                    playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = false;
                }
                else
                {
                    playersManager.GetPlayer(i).GetComponent<PlayerVehicleScript>().iaEnabled = false;
                }
                TextMeshPro tmPro = playersManager.GetPlayer(i).Find("vehicleChasis").Find("PlayerNumText").GetComponent<TextMeshPro>();
                tmPro.text = "Player " + i.ToString();
                tmPro.fontSize = 27;
                tmPro.transform.localPosition =
                    new Vector3(tmPro.transform.localPosition.x, tmPro.transform.localPosition.y - 0.1f, tmPro.transform.localPosition.z + 0.1f);
            }
        }

        activeAI = _toggle.isOn;
    }

    static public bool GetAi_Active()
    {
        return activeAI;
    }

    static public Color GetColor(ref int _id)
    {
        if (_id >= sharedUsedColors.Length) _id = 0;
        return sharedUsedColors[_id].color;
    }
}
