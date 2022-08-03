using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseParsecRoomManager : MonoBehaviour
{
    public InputField shortLinkUrl;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("LastShortUrl", "null") != "null") shortLinkUrl.text = PlayerPrefs.GetString("LastShortUrl");
        else gameObject.SetActive(false);
    }

}
