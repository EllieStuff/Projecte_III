using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishRoundMenu : MonoBehaviour
{
    public enum FinishMenuButtons { NEXT_ROUND, EXIT, COUNT }

    [SerializeField] RoundManager roundManager;
    [SerializeField] Color baseColor, hoverColor;

    Button[] buttons;
    GlobalMenuInputs inputs;

    int idx = 0;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<GlobalMenuInputs>();

        buttons = new Button[(int)FinishMenuButtons.COUNT];
        buttons[(int)FinishMenuButtons.NEXT_ROUND] = transform.GetChild(0).Find("NextRoundButton").GetComponent<Button>();
        buttons[(int)FinishMenuButtons.EXIT] = transform.GetChild(0).Find("ExitButton").GetComponent<Button>();

        ResetButtonColors();
        SetButtonColor(0, idx);
    }

    // Update is called once per frame
    void Update()
    {
        int lastIdx = idx;
        if (inputs.UpPressed)
        {
            idx--;
            if (idx < 0) idx = buttons.Length - 1;
            SetButtonColor(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.DownPressed)
        {
            idx++;
            if (idx >= buttons.Length) idx = 0;
            SetButtonColor(lastIdx, idx);
            AudioManager.Instance.Play_SFX("Hover_SFX");
        }
        else if (inputs.AcceptPressed)
        {
            buttons[idx].onClick.Invoke();
        }
    }

    void SetButtonColor(int lastIdx, int currIdx)
    {
        buttons[lastIdx].GetComponent<Image>().color = baseColor;
        buttons[currIdx].GetComponent<Image>().color = hoverColor;
    }
    void ResetButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = baseColor;
        }
    }


    public void HoverSound()
    {
        AudioManager.Instance.Play_SFX("Hover_SFX");
    }

    public void ExitButton()
    {
        AudioManager.Instance.Play_SFX("Click_SFX");
        roundManager.StopAllCoroutines();
        //Time.timeScale = 1.0f;
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Menu");
    }
    public void ResetGame()
    {
        AudioManager.Instance.Play_SFX("Click_SFX");
        Destroy(GameObject.FindGameObjectWithTag("PlayersManager"));
        roundManager.StopAllCoroutines();
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Current Building Scene");
        //SceneManager.LoadScene("Current Building Scene");
    }

}
