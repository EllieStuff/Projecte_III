using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{
    public GameObject settingsMenu;
    public Slider localVoiceValue;
    private bool changeSceneToBuilding;
    private float timerChangeScene = 5;
    private float menuCutSceneTimer = 10;
    [SerializeField] Transform[] vehicles;
    [SerializeField] GameObject menuCutScene;

    private void Update()
    {
        if (menuCutSceneTimer > 0)
            menuCutSceneTimer -= Time.deltaTime;
        else if(menuCutScene != null)
            Destroy(menuCutScene);

        if(changeSceneToBuilding)
        {
            timerChangeScene -= Time.deltaTime;
            for (int i = 0; i < transform.childCount; i++)
            {
                TextMeshProUGUI text1 = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                if (text1 != null)
                    text1.color -= new Color(0, 0, 0, Time.deltaTime);
                Image childImage = transform.GetChild(i).GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.color -= new Color(0, 0, 0, Time.deltaTime);
                    if (transform.GetChild(i).childCount > 0)
                    {
                        TextMeshProUGUI text2 = transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
                        if (text2 != null)
                            text2.color -= new Color(0, 0, 0, Time.deltaTime);
                    }
                }
            }
        }
        if(timerChangeScene <= 0)
            SceneManager.LoadScene("Building Scene");
        else if(timerChangeScene <= 3)
        {
            for(int i = 0; i < 2; i++)
            {
                AudioSource audio = vehicles[i].GetChild(0).GetComponent<AudioSource>();
                if (audio != null && !audio.enabled)
                    audio.enabled = true;
                vehicles[i].position += vehicles[i].TransformDirection(0, 0, Time.deltaTime * 50);
            }
        }

    }

    public void OpenSettings()
    {
        //localVoiceValue.value = AudioListener.volume;
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void Play()
    {
        transform.parent.GetComponent<Animator>().enabled = true;
        changeSceneToBuilding = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
