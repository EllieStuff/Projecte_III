using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlideDemoScript : MonoBehaviour
{
    [SerializeField] float dirForce = 70.0f;
    [SerializeField] float timer = 2.0f;

    public QuadControls controls;

    Rigidbody rb;
    bool gliderAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        controls = new QuadControls();
        controls.Enable();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Quad.UseActualGadget.ReadValue<float>() > 0 && !gliderAvailable)
        {
            gliderAvailable = true;
            rb.useGravity = false;
            StartCoroutine(GliderCoroutine());
        }
        if (controls.Quad.UseActualGadget.ReadValue<float>() > 0 && gliderAvailable)
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
