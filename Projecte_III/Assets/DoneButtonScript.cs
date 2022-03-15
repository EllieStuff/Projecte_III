using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButtonScript : MonoBehaviour
{
    [SerializeField] int playerId;

    internal bool isReady = false;
    DoneButtonManager bttnManager;
    Image buttonImage;
    Color savedBttnImgColor;

    // Start is called before the first frame update
    void Start()
    {
        bttnManager = GetComponentInParent<DoneButtonManager>();
        buttonImage = GetComponent<Image>();
        savedBttnImgColor = buttonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetReady()
    {
        isReady = !isReady;
        if (isReady)
            StartCoroutine(LerpBttnImgColor(bttnManager.selectedBttnImgColor));
        else
            StartCoroutine(LerpBttnImgColor(savedBttnImgColor));
        //if (isReady)
        //    buttonImage.color = selectedBttnImgColor;
        //else
        //    buttonImage.color = savedBttnImgColor;
    }


    public IEnumerator LerpBttnImgColor(Color _targetColor)
    {
        Color initColor = buttonImage.color;
        float timer = 0, maxTime = 0.05f;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            buttonImage.color = Color.Lerp(initColor, _targetColor, timer / maxTime);
        }
    }
}
