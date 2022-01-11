using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideDemoScript : MonoBehaviour
{
    [SerializeField] float dirForce = 70.0f;
    [SerializeField] float timer = 2.0f;

    Rigidbody rb;
    bool gliderAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !gliderAvailable)
        {
            gliderAvailable = true;
            rb.useGravity = false;
            StartCoroutine(GliderCoroutine());
        }
        if (Input.GetKey(KeyCode.Mouse0) && gliderAvailable)
        {
            Vector3 dir = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.AddForce(dir * dirForce, ForceMode.Acceleration);
        }

    }


    IEnumerator GliderCoroutine()
    {
        yield return new WaitForSeconds(timer);
        rb.useGravity = true;
        gliderAvailable = false;

    }


}
