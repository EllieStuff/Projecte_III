using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelector : MonoBehaviour
{
    [SerializeField] int mapPos;
    [SerializeField] int mapQuantity;
    [SerializeField] string[] mapNames;
    [SerializeField] Button doneButton;

    float timerPress;
    Vector3 newPos;
    InputSystem inputSystem;
    public PlayerInputs menuPlayerInputs;

    void Start()
    {
        inputSystem = InputSystem.Instance;

        newPos = transform.position;
        //inputSystem.transform.name = "destroy";
        //inputSystem.transform.tag = "SceneSelector";
    }

    void Update()
    {
        if (timerPress <= 0 && (menuPlayerInputs.Right || menuPlayerInputs.Left))
            timerPress = 1;
        else if (timerPress > 0)
            timerPress -= Time.deltaTime;

        if (menuPlayerInputs.Right && mapPos < mapQuantity - 1)
        {
            mapPos++;
            newPos = new Vector3(newPos.x -25.28f, newPos.y, newPos.z);

        }
        else if (menuPlayerInputs.Left && mapPos > 0)
        {
            mapPos--;
            newPos = new Vector3(newPos.x + 25.28f, newPos.y, newPos.z);
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);

        if (mapNames[mapPos] != "" && !doneButton.interactable)
            doneButton.interactable = true;
        else if(mapNames[mapPos] == "" && doneButton.interactable)
            doneButton.interactable = false;
    }

    public void loadScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Building Scene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("Building Scene"))
        {
            LoadSceneManager.Instance.newScene = mapNames[mapPos];
            Destroy(gameObject);
        }
    }
}
