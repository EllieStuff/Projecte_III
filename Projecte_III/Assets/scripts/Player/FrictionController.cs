using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionController : MonoBehaviour
{
    [System.Serializable]
    public struct Friction
    {
        public string name;
        public Vector3 frictionForce;
        public float angularDrag;
        public Friction(Vector3 _frictionForce, float _angularDrag, string _name) { frictionForce = _frictionForce; angularDrag = _angularDrag; name = _name; }
    }

    [SerializeField] Friction[] initFrictions;

    List<Friction> extraFrictions =  new List<Friction>();
    List<Friction> usedFrictions = new List<Friction>();
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (initFrictions.Length > 0)
        {
            for (int i = 0; i < initFrictions.Length; i++)
                usedFrictions.Add(initFrictions[i]);

        }

        InitExtraFrictions();

    }

    void InitExtraFrictions()
    {
        // Painting Friction
        extraFrictions.Add(new Friction(new Vector3(-800, 0, 0), 10, "Painting"));

        // Oil Friction
        extraFrictions.Add(new Friction(new Vector3(70, 0, 0), 0.5f, "Oil"));

    }



    // Update is called once per frame
    void Update()
    {
        if (usedFrictions.Count > 0 && rb.velocity.magnitude > 1.0f)
            AddFrictions();
    }


    void AddFrictions()
    {
        for (int i = 0; i < usedFrictions.Count; i++)
            AddFriction(usedFrictions[i].frictionForce);

        rb.angularDrag = GetAngularDrag();

    }
    void AddFriction(Vector3 _frictionForce)
    {
        // ToDo: tornar-ho a fer com al player, amb un sol float, que alla almenys funcionav

        //Vector3 frictionVec = transform.up * _frictionForce;
        Vector3 velocityDir = rb.velocity.normalized;
        //Vector3 relativeForward = transform.TransformDirection(transform.forward);
        Vector3 localFrictionForce = transform.InverseTransformDirection(_frictionForce);
        //Vector3 velFrictionVec = new Vector3(relativeForward.x * _frictionForce.x, relativeForward.y * _frictionForce.y, relativeForward.z * _frictionForce.z);
        Vector3 velFrictionVec = new Vector3(velocityDir.x * localFrictionForce.x, velocityDir.y * localFrictionForce.y, velocityDir.z * localFrictionForce.z);
        rb.AddForce(velFrictionVec, ForceMode.Force);
        //Vector3 angularFrictionVec = -rb.angularVelocity.normalized * _frictionForce;
        //rb.AddForce(angularFrictionVec, ForceMode.Force);
    }
    float GetAngularDrag()
    {
        float totalDrag = 0;
        for (int i = 0; i < usedFrictions.Count; i++)
            totalDrag += usedFrictions[i].angularDrag;

        return (totalDrag / (float)usedFrictions.Count);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            int fricId = Utils.Misc.FindFrictionIdByName(extraFrictions, "Painting");
            if (fricId >= 0)
            {
                usedFrictions.Add(extraFrictions[fricId]);
                Debug.Log("found");
            }
        }
        if (other.CompareTag("Oil"))
        {
            int fricId = Utils.Misc.FindFrictionIdByName(extraFrictions, "Oil");
            if (fricId >= 0) usedFrictions.Add(extraFrictions[fricId]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            int fricId = Utils.Misc.FindFrictionIdByName(usedFrictions, "Painting");
            if (fricId >= 0) usedFrictions.RemoveAt(fricId);
        }
        if (other.CompareTag("Oil"))
        {
            int fricId = Utils.Misc.FindFrictionIdByName(usedFrictions, "Oil");
            if (fricId >= 0) usedFrictions.RemoveAt(fricId);
        }
    }

}
