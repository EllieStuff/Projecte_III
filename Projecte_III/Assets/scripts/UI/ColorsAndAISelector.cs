using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorsAndAISelector : MonoBehaviour
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
    [SerializeField] Material AI_Color_Mat;
    [SerializeField] Material blocked_Car_Mat;

    [SerializeField] Transform backgrounds;
    [SerializeField] InactiveScreensManager inactiveScreens;

    static ColorData[] sharedUsedColors;

    PlayersManager playersManager;

    static bool activeAI = false;

    private void Start()
    {
        AI_Color.a = 0.5f;

        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        if (sharedUsedColors == null)
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

    public void SetAI_Active(UnityEngine.UI.Toggle _toggle)
    {
        Debug.Log("Num of players: " + playersManager.numOfPlayers.ToString());

        for (int i = inactiveScreens.PlayersInited; i < playersManager.players.Length; i++)
        {
            SpriteRenderer aiBg = backgrounds.GetChild(i + 1).GetComponent<SpriteRenderer>();
            Transform aiText = transform.GetChild(i);
            TextMeshPro tmPro;

            //Activate AIs
            if (_toggle.isOn)
            {
                //Change car color
                aiBg.color = AI_Color;
                aiText.GetComponentInChildren<UnityEngine.UI.Image>().color = AI_Color;
                aiText.GetComponentInChildren<TextMeshProUGUI>().text = "CPU";
                playersManager.GetPlayer(i).GetComponentInChildren<VehicleTriggerAndCollisionEvents>().DefaultMaterial = AI_Color_Mat;

                //Change Text
                tmPro = playersManager.GetPlayer(i).Find("vehicleChasis").Find("PlayerNumText").GetComponent<TextMeshPro>();
                tmPro.text = "CPU";
                tmPro.fontSize = 22;
                tmPro.transform.localPosition =
                    new Vector3(tmPro.transform.localPosition.x, tmPro.transform.localPosition.y + 0.01f, tmPro.transform.localPosition.z - 0.1f);
            }
            //Disactivate AIs
            else
            {
                //Change car color
                Color _currentColor = GetDefaultBgColor(i);
                aiText.GetComponentInChildren<UnityEngine.UI.Image>().color = _currentColor;
                aiText.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (i + 1).ToString();
                _currentColor.a = 0.5f;
                aiBg.color = _currentColor;
                Transform playerTrans = playersManager.GetPlayer(i);
                if (playerTrans == null) continue;
                playerTrans.GetComponentInChildren<VehicleTriggerAndCollisionEvents>().DefaultMaterial = blocked_Car_Mat;

                //Change Text
                tmPro = playerTrans.Find("vehicleChasis").Find("PlayerNumText").GetComponent<TextMeshPro>();
                tmPro.text = "P" + (i + 1).ToString();
                tmPro.fontSize = 27;
                tmPro.transform.localPosition =
                    new Vector3(tmPro.transform.localPosition.x, tmPro.transform.localPosition.y - 0.01f, tmPro.transform.localPosition.z + 0.1f);
            }
        }

        activeAI = _toggle.isOn;
    }

    private Color GetDefaultBgColor(int id)
    {
        if(id == 0)
        {
            return UsedColors[7].color;
        }
        else if(id == 1)
        {
            return UsedColors[5].color;
        }
        else if (id == 2)
        {
            return UsedColors[11].color;
        }
        else if (id == 3)
        {
            return UsedColors[9].color;
        }
        return Color.white;
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
