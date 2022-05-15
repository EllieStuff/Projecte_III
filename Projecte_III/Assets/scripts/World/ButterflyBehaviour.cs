using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyBehaviour : MonoBehaviour
{
    const float MARGIN = 1.0f;
    const float CAMERA_MARGIN = 0.5f;

    [SerializeField] Transform butterflyModel;
    [SerializeField] Transform target;
    [Space]
    [SerializeField] Utils.MinMaxFloat butterflySpeedMult;
    [SerializeField] float butterflyBaseSpeed;
    [SerializeField] float butterflyBaseAngularSpeed;
    [Space]
    [SerializeField] Utils.MinMaxFloat changeTargetLocRate;
    [SerializeField] Utils.MinMaxVec3 minMaxTargetLocMargin;

    Rigidbody rb;
    Transform camera;
    Quaternion initModelRot;
    float actualSpeed, actualAngularSpeed;
    bool disableMove = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = butterflyModel.GetComponent<Rigidbody>();
        camera = Camera.main.transform;
        float speedMult = butterflySpeedMult.GetRndValue();
        actualSpeed = butterflyBaseSpeed * speedMult;
        actualAngularSpeed = butterflyBaseAngularSpeed * speedMult;

        StartCoroutine(ChangeButterflyTargetLocation());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disableMove) 
            return;

        butterflyModel.rotation = Quaternion.Lerp(butterflyModel.rotation, target.rotation, actualAngularSpeed * Time.deltaTime);
        rb.MovePosition(butterflyModel.position + butterflyModel.forward * actualSpeed * Time.deltaTime);

        RefreshTargetLocation();
    }

    void RefreshTargetLocation()
    {
        float modelToTarget_Dist = Vector3.Distance(butterflyModel.position, target.position);
        if (modelToTarget_Dist < MARGIN || Physics.Raycast(butterflyModel.position, butterflyModel.forward, MARGIN)
            || Vector3.Distance(butterflyModel.position, camera.position) < CAMERA_MARGIN)
        {
            int itCounter = 20;
            do
            {
                SetNewTargetLocation();
                itCounter--;
                //if (counter <= 0) Debug.LogError("Yeeeeh...");
            } while (Physics.Raycast(butterflyModel.position, target.position - butterflyModel.position, modelToTarget_Dist) && itCounter > 0);

            if (itCounter <= 0)
            {
                Debug.LogWarning("Too many iterations, move disabled");
                disableMove = true;
            }
        }
    }
    void SetNewTargetLocation()
    {
        target.position = butterflyModel.position + minMaxTargetLocMargin.GetRndValue();
        Vector3 targetMoveDir = (target.position - butterflyModel.position).normalized;
        target.rotation = Quaternion.LookRotation(targetMoveDir, target.up);
    }


    IEnumerator ChangeButterflyTargetLocation()
    {
        while (gameObject.activeSelf)
        {
            //target.position = SetNewTargetLocation();
            //Vector3 targetMoveDir = (target.position - butterflyModel.position).normalized;
            //target.rotation *= Quaternion.FromToRotation(target.forward, targetMoveDir);
            //initModelRot = butterflyModel.rotation;
            SetNewTargetLocation();
            yield return new WaitForSeconds(changeTargetLocRate.GetRndValue());
        }
        yield return new WaitForEndOfFrame();
    }

}
