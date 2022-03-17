using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LerpTextAlphaLoop : MonoBehaviour
{
    [SerializeField] float newAlpha, lerpTime, lerpFrequency, lerpFreqMargin;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpTextAlpha());
    }

    IEnumerator LerpTextAlpha()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        //float initAlpha = text.color.a;
        Color initColor = text.color;
        Color targetColor = new Color(text.color.r, text.color.g, text.color.b, newAlpha);

        yield return new WaitForSeconds(lerpFrequency + lerpFreqMargin);
        while (gameObject.activeInHierarchy)
        {
            float timer = 0;
            while(timer < lerpTime)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                text.color = Color.Lerp(initColor, targetColor, timer / lerpTime);
            }
            yield return new WaitForSeconds(lerpFrequency);
            //initAlpha = initColor.a;
            Color tmpColor = targetColor;
            targetColor = initColor;
            initColor = tmpColor;
        }

    }

}
