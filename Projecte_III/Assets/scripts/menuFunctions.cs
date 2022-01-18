using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuFunctions : MonoBehaviour
{
    public GameObject settingsMenu;
    public Slider localVoiceValue;

    public void OpenSettings()
    {
        localVoiceValue.value = AudioListener.volume;
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Snaky Mounty");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
