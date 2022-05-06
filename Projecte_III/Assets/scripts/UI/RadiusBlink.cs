using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusBlink : MonoBehaviour
{
    [SerializeField] bool blinkEffect;
    [SerializeField] SpriteRenderer blinkSprite;
    [SerializeField] SpriteRenderer finalColorSprite;

    private float timer, currentTime;

    private Color finalColor, initialColor;

    [SerializeField] Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = currentColor = blinkSprite.color;
        finalColor = finalColorSprite.color;

        currentTime = 0.0f;

        initialColor.a = currentColor.a = 0.2f;
        finalColor.a = 0.5f;


    }

    // Update is called once per frame
    void Update()
    {
        if (!blinkEffect)
        {
            blinkSprite.color = initialColor;
            Destroy(this);
            Debug.Log("I don't blink");
            return;
        }
        currentColor = Color.Lerp(initialColor, finalColor, currentTime / timer);

        blinkSprite.color = currentColor;


        currentTime += Time.deltaTime;
    }

    public void SetBlink(bool _blink, float _time = 0.0f)
    {
        blinkEffect = _blink;

        timer = _time;
    }
}
