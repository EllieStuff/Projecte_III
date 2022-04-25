using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitterBirdScript : MonoBehaviour
{
    const float RAY_MARGIN = 0.3f;
    public enum ShitType { AIM, DIARRHEA };

    public ShitType shitType = ShitType.AIM;
    [SerializeField] Vector3 moveDir = new Vector3(-1.0f, 0.0f, 0.3f);
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float size = 1.0f;
    [SerializeField] GameObject shitPrefab;
    [SerializeField] Transform shadow;
    [SerializeField] float scaleFactor = 0.1f;

    Rigidbody rb;
    float initY;
    Vector3 initShadowScale;
    bool inSameCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();

        //InitRndValues();
        InitValues(moveDir, moveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
        RaycastHit hit;
        Debug.DrawRay(rb.position, Vector3.zero, Color.red);
        if(Physics.Raycast(GetRaycastRay(), out hit))
        {
            if (shitType == ShitType.AIM && hit.transform.CompareTag("PlayerVehicle") && !inSameCollision)
            {
                inSameCollision = true;
                Instantiate(shitPrefab, transform.position, shitPrefab.transform.rotation);
            }
            else
            {
                if(inSameCollision) inSameCollision = false;
                shadow.position = GetCorrectedPosition(hit.point, -0.3f);
            }
        }
        if(initY != rb.position.y)
        {
            float scaleModifier = (initY - rb.position.y) * scaleFactor;
            if (scaleModifier <= -initShadowScale.x) 
                shadow.localScale = Vector3.zero;
            else 
                shadow.localScale = new Vector3(initShadowScale.x + scaleModifier, initShadowScale.y + scaleModifier, initShadowScale.z + scaleModifier);
        }
    }

    Vector3 GetCorrectedPosition(Vector3 _pos, float _margin)
    {
        return new Vector3(_pos.x, _pos.y - _margin, _pos.z);
    }
    Ray GetRaycastRay()
    {
        return new Ray(GetCorrectedPosition(rb.position, RAY_MARGIN), Vector3.down);
    }

    public void InitRndValues()
    {
        moveDir = new Vector3(Utils.Misc.RndPositivity(), Random.Range(0.0f, 0.5f), Random.Range(-0.7f, 0.7f));
        moveSpeed = Random.Range(6.0f, 10.0f);
        size = Random.Range(0.3f, 1.0f);

        GenericInit();
    }
    public void InitValues(Vector3 _moveDir, float _moveSpeed, float _size = 0.8f)
    {
        moveDir = _moveDir;
        moveSpeed = _moveSpeed;
        size = _size;

        GenericInit();
    }
    void GenericInit()
    {
        rb.transform.rotation *= Quaternion.FromToRotation(rb.transform.forward, moveDir);
        //shadow.rotation *= Quaternion.FromToRotation(shadow.forward, moveDir);
        //transform.rotation *= Quaternion.FromToRotation(-transform.up, Vector3.down);

        Vector3 newScale = rb.transform.localScale * size;
        rb.transform.localScale = shadow.localScale /*= transform.localScale*/ = newScale;

        initY = rb.position.y;
        initShadowScale = rb.transform.localScale;

        StopAllCoroutines();
        if (shitType == ShitType.DIARRHEA)
            StartCoroutine(DiarrheaCoroutine());
    }

    private IEnumerator DiarrheaCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 6.0f));

        int diarrheaShitAmount = Random.Range(3, 10), currDiarrheaAmount = 0;
        float diarrheaTimer = 0, diarrheaDelay = 0.3f;
        while(currDiarrheaAmount < diarrheaShitAmount)
        {
            if(diarrheaTimer >= diarrheaDelay)
            {
                diarrheaTimer = 0.0f;
                currDiarrheaAmount++;
                Instantiate(shitPrefab, transform.position, shitPrefab.transform.rotation);
                break;
            }
            yield return new WaitForEndOfFrame();
            diarrheaTimer += Time.deltaTime;
        }
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("PlayerVehicle"))
    //    {
    //        Instantiate(shitPrefab, transform.position, shitPrefab.transform.rotation);
    //    }
    //}
}
