using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InactiveScreensManager : MonoBehaviour
{
   /* PlayersManager playersManager;
    PlayerInputs currPlayerInputs;
    DoneButtonManager doneBttnManager;
    int playersInited = 0;

    // Start is called before the first frame update
    void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
        doneBttnManager = GameObject.FindGameObjectWithTag("DoneBttnManager").GetComponent<DoneButtonManager>();

        if (playersManager.gameMode == PlayersManager.GameModes.MONO)
            doneBttnManager.buttonsActive = playersInited = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playersInited < playersManager.numOfPlayers && currPlayerInputs.Inited())
        {
            StartCoroutine(DisappearBlackScreen(playersInited));
            playersInited++;
            currPlayerInputs = playersManager.GetPlayer(playersInited).GetComponent<PlayerInputs>();
        }
    }

    IEnumerator DisappearBlackScreen(int _idx)
    {
        Image blackScreen = transform.GetChild(_idx).GetComponent<Image>();
        TextMeshProUGUI text = blackScreen.GetComponentInChildren<TextMeshProUGUI>();
        Color initScreenColor = blackScreen.color, initTextColor = text.color;

        float timer = 0, lerpTime = 0.1f;
        while (timer < lerpTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            blackScreen.color = Color.Lerp(initScreenColor, Color.clear, timer / lerpTime);
            text.color = Color.Lerp(initTextColor, Color.clear, timer / lerpTime);
        }
        blackScreen.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1.0f);
        doneBttnManager.buttonsActive = playersInited;
        doneBttnManager.GetButton(_idx).isActive = true;
    }
   */
}
