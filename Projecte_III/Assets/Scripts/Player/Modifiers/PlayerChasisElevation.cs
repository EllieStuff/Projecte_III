using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChasisElevation : MonoBehaviour
{
    float chasisElevationTimer;
    [SerializeField] private bool chasisElevation;
    PlayerInputs inputs;
    bool chasisEnabled;
    bool hasChasis;

    public void Init(bool _active)
    {
        hasChasis = _active;
    }

    public void Activate()
    {
        if (hasChasis)
            chasisEnabled = true;
    }

    private void Start()
    {
        inputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        if (hasChasis)
            ChasisUpdate();
    }

    public void ChasisUpdate()
    {
        Transform chasisTransform = transform.GetChild(0);

        if ((inputs.ShootAny || chasisEnabled) && !chasisElevation && chasisTransform.localPosition.y <= 0)
        {
            chasisEnabled = false;
            chasisElevation = true;
            chasisElevationTimer = 2;
        }
        else
            chasisEnabled = false;

        if (chasisElevation)
        {
            if (chasisElevationTimer > 0)
                chasisElevationTimer -= Time.deltaTime;
            else
                chasisElevation = false;

            if (chasisTransform.localPosition.y <= 2)
                chasisTransform.localPosition = new Vector3(chasisTransform.localPosition.x, chasisTransform.localPosition.y + 0.05f, chasisTransform.localPosition.z);
        }
        else
        {
            if (chasisTransform.localPosition.y > -0.5f)
                chasisTransform.localPosition = new Vector3(chasisTransform.localPosition.x, chasisTransform.localPosition.y - 0.05f, chasisTransform.localPosition.z);
        }
    }

}
