using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    const float INIT_UMBRELLA_TIME = 3.0f;
    const float UMBRELLA_LERP_SPEED = 0.1f;

    bool umbrellaActivated, umbrellaActive;
    float umbrellaTimer = INIT_UMBRELLA_TIME;
    [SerializeField] GameObject umbrellaGameObject;
    Vector3 originalPos, originalScale;

    // Start is called before the first frame update

    private void Start()
    {
        umbrellaGameObject.SetActive(false);
        originalPos = umbrellaGameObject.transform.localPosition;
        originalScale = umbrellaGameObject.transform.localScale;
    }

    private void Update()
    {
        if(umbrellaActivated)
        {
            if (!umbrellaActive)
            {
                umbrellaActive = true;
                //umbrellaGameObject.SetActive(true);
                StartCoroutine(Appear());
            }
            umbrellaTimer -= Time.deltaTime;

            if(umbrellaTimer <= 0)
            {
                umbrellaTimer = INIT_UMBRELLA_TIME;
                umbrellaActive = umbrellaActivated = false;
                StartCoroutine(Dissappear());
                //umbrellaActivated = false;
                //umbrellaGameObject.SetActive(false);
                //umbrellaGameObject.transform.localPosition = originalPos;
                //umbrellaGameObject.transform.localScale = originalScale;
            }
        }
    }

    public void ActivateUmbrella(Quaternion direction, bool moveUmbrellaPivot)
    {
        umbrellaGameObject.transform.localRotation = direction;

        AudioManager.Instance.Play_SFX("Umbrella_SFX");

        if (moveUmbrellaPivot)
            umbrellaGameObject.transform.localPosition -= new Vector3(0, 0, 1);
        
        umbrellaActivated = true;
    }


    [ContextMenu("SetUmbrellaModifier")]
    public void SetUmbrellaModifier()
    {
        RandomModifierGet.ModifierTypes modType = RandomModifierGet.ModifierTypes.UMBRELLA;
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
        umbrellaGameObject.SetActive(true);
        float timer = 0, maxTime = UMBRELLA_LERP_SPEED;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            umbrellaGameObject.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer / maxTime);
        }
    }
    IEnumerator Dissappear()
    {
        float timer = 0, maxTime = UMBRELLA_LERP_SPEED;
        while(timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            umbrellaGameObject.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, timer / maxTime);
        }
        //umbrellaActivated = false;
        umbrellaGameObject.transform.localPosition = originalPos;
        umbrellaGameObject.transform.localScale = originalScale;
        umbrellaGameObject.SetActive(false);
    }

}
