using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    string currentSceneName;
    float timer = 2;
    public string newScene;
    bool changeScene = false;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (changeScene && timer > 0)
            timer -= Time.deltaTime;

        if (newScene != currentSceneName && timer <= 0)
        {
            if (newScene == "Menu") PlayerPrefs.SetString("InitCutsceneEnabled", "false");
            SceneManager.LoadScene(newScene);
            changeScene = false;
        }
    }

    public void ChangeScene(string _newScene)
    {
        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        if (currentSceneName.Contains("Menu"))
        {
            //for (int i = 0; i < playersManager.numOfPlayers; i++)
                //playersManager.GetPlayer(i).GetComponent<PlayerStatsManager>().SetStats();
        }

        changeScene = true;
        newScene = _newScene;
    }

    public string GetSceneName() { return currentSceneName; }
}
