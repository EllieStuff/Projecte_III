using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisplacementScript : MonoBehaviour
{
    float movementPosition;

    // Start is called before the first frame update
    void Start()
    {
        movementPosition = 0.5f;
        if (gameObject.name.Contains("Butterfly"))
            movementPosition = Random.Range(0.05f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, movementPosition);

        if (transform.position.z >= 500)
            Destroy(gameObject);
    }
}
