using System.Collections;
using System.Collections.Generic;
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>().SetStats();

        if(_newScene != currentSceneName)
        {
            SceneManager.LoadScene(_newScene);
        }
    }
}
