using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    enum SoundType { OST, SFX };
    [SerializeField] SoundType soundType = SoundType.OST;
    [SerializeField] string audioName;
    public AK.Wwise.Event SFX;
    public AK.Wwise.Event OST;


    // Start is called before the first frame update
    void Start()
    {

        switch (soundType)
        {
            case SoundType.OST:
                OST.Post(gameObject);

                break;

            case SoundType.SFX:
                SFX.Post(gameObject);

                break;

            default:
                break;
        }

    }


}
