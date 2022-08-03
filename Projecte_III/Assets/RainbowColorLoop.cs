using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowColorLoop : MonoBehaviour
{
    const float MAX_VALUE = 0.9f;

    public float colorSpeed = 5.0f;
    public float colorLimit = 180.0f;
    public Vector3Int startState = new Vector3Int(1, 0, 0);

    private Image image;
    private Vector3Int currState;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        currState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = colorSpeed * Time.deltaTime;
        image.color = new Color(
            image.color.r + speed * currState.x,
            image.color.g + speed * currState.y,
            image.color.b + speed * currState.z,
            image.color.a
        );

        Debug.Log("Rainbow Color: " + image.color);
        Debug.Log("Rainbow State: " + currState);


        if (image.color.r <= colorLimit)
        {
            //currState = new Vector3Int(0, 1, 0);
            currState.y = 1;
            //currState.x = 0;
        }
        if (image.color.g <= colorLimit)
        {
            //currState = new Vector3Int(0, 0, 1);
            currState.z = 1;
            //currState.y = 0;
        }
        if (image.color.b <= colorLimit)
        {
            //currState = new Vector3Int(1, 0, 0);
            currState.x = 1;
            //currState.z = 0;
        }

        if (image.color.r >= MAX_VALUE)
        {
            //currState = new Vector3Int(0, -1, 0);
            currState.y = -1;
            //currState.x = 0;
        }
        if (image.color.g >= MAX_VALUE)
        {
            //currState = new Vector3Int(0, 0, -1);
            currState.z = -1;
            //currState.y = 0;
        }
        if (image.color.b >= MAX_VALUE)
        {
            //currState = new Vector3Int(-1, 0, 0);
            currState.x = -1;
            //currState.z = 0;
        }
    }

}
