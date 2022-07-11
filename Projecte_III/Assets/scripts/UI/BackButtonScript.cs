using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour
{
    [SerializeField] float waitTime = 2;
    [SerializeField] PressedButton bttn;
    [SerializeField] Image bg;
    [SerializeField] bool backToMenu;

    float timer = 0.0f;
    Image bttnImage;
    //int peoplePressing = 0;

    // Start is called before the first frame update
    void Start()
    {
        bttnImage = bttn.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int inputsAmount = 0;
        inputsAmount += PressedByInputs();
        if (bttn.Pressed) inputsAmount++;
        if (inputsAmount <= 0)
        {
            bg.fillAmount = timer = 0;
            bttnImage.color = Color.white;
            return;
        }

        bttnImage.color = new Color(0.8f, 0.8f, 0.8f, 1);
        timer += Time.deltaTime * inputsAmount;
        Debug.Log("Timer: " + timer);
        bg.fillAmount = timer / waitTime;
        if (timer > waitTime)
        {
            bttnImage.color = Color.green;
            if(backToMenu)
                GoBack();
            this.enabled = false;
        }
    }

    void GoBack()
    {
        Debug.Log("Succesfully entered");
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager != null) gameManager.GetComponent<GameManager>().StopParsec();
        PlayerManager[] players = GameObject.FindObjectsOfType<PlayerManager>();
        for (int i = 0; i < players.Length; i++)
            Destroy(players[i].gameObject);

        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Menu");
    }

    int PressedByInputs()
    {
        int inputsAmount = 0;
        for (int i = 0; i < InputSystem.controls.Quad.Exit.controls.Count; i++)
        {
            if (InputSystem.controls.Quad.Exit.controls[i].EvaluateMagnitude() > 0.3f)
                inputsAmount++;
        }
        return inputsAmount;
    }

}
