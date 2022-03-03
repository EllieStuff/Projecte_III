using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuPieceScript : MonoBehaviour
{
    public Image backGround;
    public Image icon;
    
    Color targetBgColor, initColor;
    float lerpBgColorTime = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        targetBgColor = initColor = backGround.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LerpBgColor(Color _color)
    {
        if(_color != targetBgColor)
        {
            targetBgColor = _color;
            StartCoroutine(LerpBgColorCoroutine(_color));
        }
    }
    public void ReinitColor()
    {
        backGround.color = initColor;
    }

    IEnumerator LerpBgColorCoroutine(Color _color)
    {
        float timer = 0;
        Color initColor = backGround.color;
        while(timer < lerpBgColorTime)
        {
            backGround.color = Color.Lerp(initColor, _color, timer / lerpBgColorTime);

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        backGround.color = _color;

    }

}
