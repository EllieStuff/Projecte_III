using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    enum InGameButton { PLAY, SETTINGS, EXIT, COUNT };

    [SerializeField] GameObject menuSet;
    [SerializeField] Color baseColor, hoverColor;
    
    Button[] buttons;
    int idx = 0;
    bool verticalGot = false;


    // Start is called before the first frame update
    void Start()
    {
        buttons = new Button[(int)InGameButton.COUNT];
        buttons[(int)InGameButton.PLAY] = menuSet.transform.Find("Play Button").GetComponent<Button>();
        buttons[(int)InGameButton.SETTINGS] = menuSet.transform.Find("Settings Button").GetComponent<Button>();
        buttons[(int)InGameButton.EXIT] = menuSet.transform.Find("Exit Button").GetComponent<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = baseColor;
        }
        SetButtonColor(0, 0);

        menuSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int lastIdx = idx;
        if (OpenMenuInput())
        {
            menuSet.SetActive(!menuSet.activeSelf);
            idx = 0;
        }
        else if (UpMenuInput())
        {
            idx--;
            if (idx < 0) idx = buttons.Length - 1;
            SetButtonColor(lastIdx, idx);
        }
        else if (DownMenuInput())
        {
            idx++;
            if (idx >= buttons.Length) idx = 0;
            SetButtonColor(lastIdx, idx);
        }
    }

    void SetButtonColor(int lastIdx, int currIdx)
    {
        buttons[lastIdx].GetComponent<Image>().color = baseColor;
        buttons[currIdx].GetComponent<Image>().color = hoverColor;
    }

    bool OpenMenuInput()
    {
        return Input.GetKeyDown(KeyCode.Escape) 
            || Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Joystick2Button7) 
            || Input.GetKeyDown(KeyCode.Joystick3Button7) || Input.GetKeyDown(KeyCode.Joystick4Button7);
    }
    bool DownMenuInput()
    {
        //if(co)
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < -0.4f;
    }
    bool UpMenuInput()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0.4f;
    }
}
