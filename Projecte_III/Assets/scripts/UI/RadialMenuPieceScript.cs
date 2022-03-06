using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuPieceScript : MonoBehaviour
{
    public Image backGround;
    public Image icon;
    public float iconRotDiff;
    
    Color targetBgColor, initColor;
    float lerpBgColorTime = 0.1f;
    float delayTime;    // ToDo: Fer que sigui igual al del modifier i usar un slider per mostrar el temps d'espera en el background


    // Start is called before the first frame update
    void Start()
    {
        targetBgColor = initColor = backGround.color;
        //iconPosDiff = icon.transform.position - iconCenter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReinitColor()
    {
        backGround.color = targetBgColor = initColor;
    }

    public void LerpBgColor(Color _color)
    {
        if(_color != targetBgColor)
        {
            targetBgColor = _color;
            StartCoroutine(LerpBgColorCoroutine(_color));
        }
    }

    IEnumerator LerpBgColorCoroutine(Color _color)
    {
        float timer = 0;
        Color tmpInitColor = backGround.color;
        while(timer < lerpBgColorTime)
        {
            backGround.color = Color.Lerp(tmpInitColor, _color, timer / lerpBgColorTime);

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        backGround.color = _color;

    }

}
