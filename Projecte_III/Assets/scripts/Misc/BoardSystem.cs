using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    Vector3 startPos;
    bool hideBoard;

    private void Awake()
    {
        hideBoard = false;

        startPos = transform.localPosition;
    }

    void Update()
    {
        if(hideBoard)
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startPos.x, -11.7f, startPos.z), Time.deltaTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 2);
    }

    public void SwitchBoard(bool option)
    {
        hideBoard = option;
    }
}
