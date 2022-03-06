using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasisElevation : MonoBehaviour
{
    private float chasisElevationTimer;
    [SerializeField] private bool chasisElevation;

    public void ChasisElevationFunction(QuadControlSystem controls, bool chasisEnabled)
    {
        Transform chasisTransform = transform.GetChild(0);

        if ((controls.Quad.ChasisElevation || chasisEnabled) && !chasisElevation && chasisTransform.localPosition.y <= 0)
        {
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
