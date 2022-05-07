using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    Image blackImage;

    string currentSceneName;
    float fadeTime = 0.5f;
    //public string newScene;
    bool changeScene = false;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        blackImage = GameObject.FindGameObjectWithTag("TransitionFadeImage").GetComponent<Image>();

        if (currentSceneName == "Menu")
            blackImage.gameObject.SetActive(false);
        else
            StartCoroutine(StartSceneCoroutine());
    }

    //private void Update()
    //{
    //    if (changeScene && timer > 0)
    //    {
    //        timer -= Time.deltaTime;
    //        blackImage
    //    }

    //    if (/*newScene != currentSceneName*/ changeScene && timer <= 0)
    //    {
    //        //if (newScene == "Menu") PlayerPrefs.SetString("InitCutsceneEnabled", "false");
    //        if (newScene != "ProceduralMapSceneTest") 
    //            Destroy(GameObject.FindGameObjectWithTag("PlayersManager"));
    //        changeScene = false;
    //        SceneManager.LoadScene(newScene);
    //    }
    //}

    public void ChangeScene(string _newScene)
    {
        StartCoroutine(ChangeSceneCoroutine(_newScene));

        //PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        //if (currentSceneName.Contains("Menu"))
        //{
        //    //for (int i = 0; i < playersManager.numOfPlayers; i++)
        //        //playersManager.GetPlayer(i).GetComponent<PlayerStatsManager>().SetStats();
        //}

        //changeScene = true;
        //newScene = _newScene;
    }

    IEnumerator ChangeSceneCoroutine(string _newScene)
    {
        blackImage.gameObject.SetActive(true);

        float timer = 0, maxTime = fadeTime;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            blackImage.color = Color.Lerp(Color.clear, Color.black, timer / maxTime);
        }
        blackImage.color = Color.black;

        if (_newScene != "ProceduralMapSceneTest")
            Destroy(GameObject.FindGameObjectWithTag("PlayersManager"));
        SceneManager.LoadScene(_newScene);
    }
    IEnumerator StartSceneCoroutine()
    {
        float timer = 0, maxTime = fadeTime;
        blackImage.color = Color.black;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            blackImage.color = Color.Lerp(Color.black, Color.clear, timer / maxTime);
        }
        blackImage.color = Color.clear;

        blackImage.gameObject.SetActive(false);
    }
    IEnumerator ChangeFadeImageColor(Color _from, Color _to)
    {
        float timer = 0, maxTime = fadeTime;
        blackImage.color = _from;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            blackImage.color = Color.Lerp(_from, _to, timer / maxTime);
        }
        blackImage.color = _to;
    }


    public string GetSceneName() { return currentSceneName; }
}
