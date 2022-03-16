using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string _newScene)
    {
        PlayersManager playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>(); 
        for(int i = 0; i < playersManager.numOfPlayers; i++)
            playersManager.GetPlayer(i).GetComponent<PlayerStatsManager>().SetStats();

        if(_newScene != currentSceneName)
        {
            SceneManager.LoadScene(_newScene);
        }
    }

    public string GetSceneName() { return currentSceneName; }
}
