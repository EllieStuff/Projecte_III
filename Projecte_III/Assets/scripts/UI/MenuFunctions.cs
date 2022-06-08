using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{
    const float ANIMS_MARGIN = 0.8f;

    public GameObject creditsMenu;
    public SettingsMenuScript settingsMenu;
    public Slider localVoiceValue;
    private float changeSceneTime = 5;
    [SerializeField] Transform[] vehicles;
    [SerializeField] GameObject menuCutScene;
    [SerializeField] Transform menuTitle;
    [SerializeField] Transform mainMenuButtonsFather;
    //[SerializeField] Transform tunelTransform;

    bool enableButtons = false;


    private void Update()
    {
        if (!enableButtons)
        {
            if (menuCutScene == null || !menuCutScene.activeSelf)
                enableButtons = true;
        }
    }

    public void HoverSound()
    {
        AudioManager.Instance.Play_SFX("Hover_SFX");
    }

    public void OpenSettings()
    {
        if (!enableButtons) return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        settingsMenu.gameObject.SetActive(true);
        MainMenuManageInputs mainMenuInputs = GetComponent<MainMenuManageInputs>();
        mainMenuInputs.menuState = MainMenuManageInputs.MenuState.SETTINGS;
        mainMenuInputs.mainIdx = (int)MainMenuManageInputs.MenuState.SETTINGS;
        settingsMenu.Init(GetComponent<GlobalMenuInputs>(), null);
    }

    public void CloseSettings()
    {
        AudioManager.Instance.Play_SFX("Click_SFX");
        settingsMenu.gameObject.SetActive(false);
        MainMenuManageInputs mainMenuInputs = GetComponent<MainMenuManageInputs>();
        mainMenuInputs.menuState = MainMenuManageInputs.MenuState.MAIN;
        mainMenuInputs.mainButtons[(int)MainMenuManageInputs.MenuState.SETTINGS].Select();
    }

    public void ActiveCredits(bool active)
    {
        if (!enableButtons) return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        creditsMenu.SetActive(active);
        if (!active)
        {
            MainMenuManageInputs mainMenuInputs = GetComponent<MainMenuManageInputs>();
            mainMenuInputs.menuState = MainMenuManageInputs.MenuState.MAIN;
            mainMenuInputs.mainButtons[(int)MainMenuManageInputs.MenuState.CREDITS].Select();
        }
    }

    public void Play()
    {
        if (!enableButtons) return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        transform.parent.GetComponent<Animator>().enabled = true;
        StartCoroutine(ExitMenuAnimationCoroutine());
    }
    //public void PlaySingle()
    //{
    //    transform.parent.GetComponent<Animator>().enabled = true;
    //    StartCoroutine(ExitMenuAnimationCoroutine());
    //    PlayerPrefs.SetString("GameMode", "Single");
    //}
    //public void PlayMultiLocal()
    //{
    //    transform.parent.GetComponent<Animator>().enabled = true;
    //    StartCoroutine(ExitMenuAnimationCoroutine());
    //    PlayerPrefs.SetString("GameMode", "MultiLocal");
    //}

    public void ExitGame()
    {
        if (!enableButtons) return;

        AudioManager.Instance.Play_SFX("Click_SFX");
        Application.Quit();
    }


    IEnumerator ExitMenuAnimationCoroutine()
    {
        // Gets the gameobjects to fade
        int arrayDiff = 1;
        Transform[] mainMenuUIItems = new Transform[mainMenuButtonsFather.childCount + arrayDiff];
        Color[] initImagesColors = new Color[mainMenuUIItems.Length];
        Color[] initTextsColors = new Color[mainMenuUIItems.Length];
        for (int i = 0; i < mainMenuUIItems.Length - arrayDiff; i++) {
            mainMenuUIItems[i] = mainMenuButtonsFather.GetChild(i);
            initImagesColors[i] = mainMenuUIItems[i].GetComponent<Image>().color;
            initTextsColors[i] = mainMenuUIItems[i].GetComponentInChildren<TextMeshProUGUI>().color;
        }
        int titleIdx = mainMenuUIItems.Length - arrayDiff;
        mainMenuUIItems[titleIdx] = menuTitle;
        initImagesColors[titleIdx] = menuTitle.GetComponent<Image>().color;
        //initTextsColors[titleIdx] = menuTitle.GetComponentInChildren<TextMeshProUGUI>().color;


        // Run animations
        float timer = 0, moveCarsTime = 2.0f;
        while(timer < changeSceneTime)
        {
            //tunelTransform.position += new Vector3(0, 0, Time.deltaTime * 20);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            if (timer > moveCarsTime)
            {
                for (int i = 0; i < vehicles.Length; i++)
                {
                    AudioSource audio = vehicles[i].GetChild(0).GetComponent<AudioSource>();
                    if (audio != null && !audio.enabled)
                        audio.enabled = true;
                    vehicles[i].position += vehicles[i].TransformDirection(0, 0, Time.deltaTime * 50);
                }
            }
            else
            {
                for (int i = 0; i < mainMenuUIItems.Length; i++)
                {
                    mainMenuUIItems[i].GetComponent<Image>().color = Color.Lerp(initImagesColors[i], Color.clear, timer / (moveCarsTime - ANIMS_MARGIN));
                    if(mainMenuUIItems[i].childCount > 0)
                        mainMenuUIItems[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.Lerp(initTextsColors[i], Color.clear, timer / (moveCarsTime - ANIMS_MARGIN));
                }
            }

        }

        Destroy(menuCutScene);
        GameObject.FindGameObjectWithTag("SceneManager").GetComponent<LoadSceneManager>().ChangeScene("Current Building Scene");
        //SceneManager.LoadScene("Current Building Scene");

    }

}
