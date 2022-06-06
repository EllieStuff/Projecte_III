using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfRoomCreated : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParsecStreamFull streamer = GetComponent<ParsecStreamFull>();
        bool roomCreated = PlayerPrefs.GetString("RoomCreated", "false") == "true";
        streamer.enabled = roomCreated;
        this.enabled = false;
    }

}
