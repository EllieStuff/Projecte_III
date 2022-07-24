using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spatula : MonoBehaviour
{
    const float INIT_SPATULA_TIME = 5.0f;
    const float SPATULA_LERP_SPEED = 0.1f;

    bool spatulaActivated, spatulaActive, spatulaIgnited;
    float spatulaTimer = INIT_SPATULA_TIME;
    [SerializeField] GameObject spatulaGameObject;
    Vector3 originalPos, originalScale;

    // Start is called before the first frame update

    private void Start()
    {
        spatulaGameObject.SetActive(false);
        originalPos = spatulaGameObject.transform.localPosition;
        originalScale = spatulaGameObject.transform.localScale;
    }

    private void Update()
    {
        if(spatulaActivated)
        {
            if (!spatulaActive)
            {
                spatulaActive = true;
                //umbrellaGameObject.SetActive(true);
                StartCoroutine(Appear());
            }
            spatulaTimer -= Time.deltaTime;

            if(spatulaTimer <= 0)
            {
                spatulaTimer = INIT_SPATULA_TIME;
                spatulaActive = spatulaActivated = false;
                StartCoroutine(Dissappear());
                //umbrellaActivated = false;
                //umbrellaGameObject.SetActive(false);
                //umbrellaGameObject.transform.localPosition = originalPos;
                //umbrellaGameObject.transform.localScale = originalScale;
            }
            else if(spatulaTimer <= 1) 
            {
                spatulaGameObject.transform.rotation = Quaternion.Lerp(spatulaGameObject.transform.rotation, new Quaternion(0, 0, 0, 90), Time.deltaTime * 0.075f);
                if(!spatulaIgnited)
                {
                    spatulaGameObject.GetComponent<SpatulaIgnition>().ThrowPlayer();
                    spatulaIgnited = true;
                }
            }
        }
    }

    public void ActivateUmbrella(Quaternion direction, bool moveUmbrellaPivot)
    {
        spatulaGameObject.transform.localRotation = direction;

        AudioManager.Instance.Play_SFX("Umbrella_SFX");

        if (moveUmbrellaPivot)
            spatulaGameObject.transform.localPosition -= new Vector3(0, 0, 1);

        spatulaActivated = true;
    }


    [ContextMenu("SetSpatulaModifier")]
    public void SetSpatulaModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.SPATULA;
        RandomModifierGet modGetter = GetComponent<RandomModifierGet>();
        modGetter.ResetModifiers();
        modGetter.SetModifier(modType);

        try
        {
            int playerId = GetComponentInParent<PlayerData>().id;
            GameObject.Find("HUD").GetComponentInChildren<PlayersHUDManager>().GetPlayerHUD(playerId).SetModifierImage((int)modType);
        }
        catch { Debug.LogError("PlayersHUD not found"); }
    }

    IEnumerator Appear()
    {
        spatulaGameObject.SetActive(true);
        float timer = 0, maxTime = SPATULA_LERP_SPEED;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            spatulaGameObject.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer / maxTime);
        }
    }
    IEnumerator Dissappear()
    {
        float timer = 0, maxTime = SPATULA_LERP_SPEED;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            spatulaGameObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, timer / maxTime);
        }
        //umbrellaActivated = false;
        spatulaGameObject.transform.localPosition = originalPos;
        spatulaGameObject.transform.localScale = originalScale;
        spatulaIgnited = false;
        spatulaGameObject.SetActive(false);
    }

}
