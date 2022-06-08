using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.material.renderQueue = 4000;

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.material.renderQueue = 4000;

        Destroy(this);
    }
}
