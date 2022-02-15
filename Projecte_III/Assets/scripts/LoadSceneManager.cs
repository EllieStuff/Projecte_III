using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] string currentSceneName;

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string _newScene)
    {
        if(_newScene != currentSceneName)
        {
            SceneManager.LoadScene(_newScene);
        }
    }

    internal static void LoadScene(string v)
    {
        throw new NotImplementedException();
    }
}
