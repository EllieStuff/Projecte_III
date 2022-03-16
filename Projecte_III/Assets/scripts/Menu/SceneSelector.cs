using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    [SerializeField] int mapPos;
    [SerializeField] int mapQuantity;
    [SerializeField] string[] mapNames;
    PlayerInputs inputs;
    float timerPress;
    Vector3 newPos;

    void Start()
    {
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputs>();
        newPos = transform.position;
    }

    void Update()
    {
        if (timerPress <= 0 && (inputs.Right || inputs.Left))
            timerPress = 1;
        else if (timerPress > 0)
            timerPress -= Time.deltaTime;

        if (inputs.Right && mapPos < mapQuantity - 1)
        {
            mapPos++;
            newPos = new Vector3(newPos.x -25.28f, newPos.y, newPos.z);

        }
        else if (inputs.Left && mapPos > 0)
        {
            mapPos--;
            newPos = new Vector3(newPos.x + 25.28f, newPos.y, newPos.z);
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2);
    }

    public void loadScene()
    {
        SceneManager.LoadScene(mapNames[mapPos]);
    }
}
