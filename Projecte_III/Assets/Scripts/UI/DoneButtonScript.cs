using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButtonScript : MonoBehaviour
{
    [SerializeField] internal bool isActive = false;
    [SerializeField] int playerId;

    internal bool isReady = false;
    PlayersManager playersManager;
    PlayerInputs playerInputs;
    DoneButtonManager bttnManager;
    Image buttonImage;
    Color savedBttnImgColor;
    bool startPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        playersManager = GameObject.FindGameObjectWithTag("PlayersManager").GetComponent<PlayersManager>();
        playerInputs = playersManager.GetPlayer(playerId).GetComponent<PlayerInputs>();
        bttnManager = GetComponentInParent<DoneButtonManager>();
        buttonImage = GetComponent<Image>();
        savedBttnImgColor = buttonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && playersManager.gameMode == PlayersManager.GameModes.MULTI_LOCAL)
        {
            if (playerInputs.Start && !startPressed)
            {
                startPressed = true;
                SetReady();
            }
            else if (!playerInputs.Start && startPressed)
            {
                startPressed = false;
            }

        }

    }


    public void SetReadyByClick()
    {
        if (isActive)
        {
            if (playersManager.gameMode == PlayersManager.GameModes.MONO 
                || playerInputs.ControlData[0].deviceType == InputSystem.DeviceTypes.KEYBOARD)
            {
                SetReady();
            }
        }
    }
    void SetReady()
    {
        isReady = !isReady;
        if (isReady)
        {
            AudioManager.Instance.Play_SFX("Engine_Ignition_SFX 1");
            StartCoroutine(LerpBttnImgColor(bttnManager.selectedBttnImgColor));
        }
        else
            StartCoroutine(LerpBttnImgColor(savedBttnImgColor));
    }


    public IEnumerator LerpBttnImgColor(Color _targetColor)
    {
        Color initColor = buttonImage.color;
        float timer = 0, maxTime = 0.05f;
        while (timer < maxTime)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            buttonImage.color = Color.Lerp(initColor, _targetColor, timer / maxTime);
        }
    }
}
