using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMask : MonoBehaviour
{
    MeshRenderer mesh;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.material.renderQueue = 4000;

        sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.material.renderQueue = 4000;

        Destroy(this);
    }
}
