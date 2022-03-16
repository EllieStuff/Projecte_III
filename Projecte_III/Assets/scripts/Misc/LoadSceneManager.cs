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
            SceneManager.LoadScene(newScene);
            changeScene = false;
        }
    }

    public void ChangeScene(string _newScene)
    {
        newScene = _newScene;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>().SetStats();
        changeScene = true;
    }

    public string GetSceneName() { return currentSceneName; }
}
