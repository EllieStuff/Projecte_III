using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersHUD : MonoBehaviour
{
    [SerializeField] Sprite[] possibleModifiers;

    Image modifier;
    Transform[] lives;

    // Start is called before the first frame update
    void Start()
    {
        modifier = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        modifier.gameObject.SetActive(false);

        lives = new Transform[transform.GetChild(0).childCount];
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i] = transform.GetChild(0).GetChild(i);
        }
    }

    public void UpdateLives(int currentLives)
    {
        if(lives[currentLives-1].gameObject.activeSelf)
        {
            lives[currentLives - 1].gameObject.SetActive(false);
        }
        else
        {
            lives[currentLives - 1].gameObject.SetActive(true);
        }
    }

    public void RollModifiers(int _modifierIdx)
    {
        Debug.Log("Eyyyy, I'm rolling");

        IEnumerator roll = RollModifier(_modifierIdx);
        StartCoroutine(roll);
    }

    IEnumerator RollModifier(int _modifierIdx)
    {
        int timesShown = 0;
        int initial = _modifierIdx + Random.Range(0, possibleModifiers.Length);
        if (initial >= possibleModifiers.Length) initial -= possibleModifiers.Length;
        int currentSprite = initial + 1;
        while(timesShown < 3)
        {
            if (currentSprite >= possibleModifiers.Length) currentSprite = 0;
            if (currentSprite == initial)
                timesShown++;

            modifier.sprite = possibleModifiers[currentSprite];

            if(!modifier.gameObject.activeSelf)
                modifier.gameObject.SetActive(true);

            currentSprite++;
            yield return new WaitForSeconds(0.2f);
        }

        modifier.sprite = possibleModifiers[_modifierIdx];

        yield return 0;
    }

    public void ClearModifiers()
    {
        modifier.gameObject.SetActive(false);
    }
}
