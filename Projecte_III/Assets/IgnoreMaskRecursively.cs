using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMaskRecursively : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //IgnoreMask_Standart(transform);
        IgnoreMask_Recursive(transform);

        Destroy(this);
    }

    void IgnoreMask_Standart(Transform _currTransform)
    {
        MeshRenderer mesh = _currTransform.GetComponent<MeshRenderer>();
        if (mesh != null) mesh.material.renderQueue = 4000;
        SpriteRenderer sprite = _currTransform.GetComponent<SpriteRenderer>();
        if (sprite != null) sprite.material.renderQueue = 4000;
    }
    void IgnoreMask_Recursive(Transform _currTransform)
    {
        IgnoreMask_Standart(_currTransform);
        for(int i = 0; i < _currTransform.childCount; i++)
        {
            IgnoreMask_Recursive(_currTransform.GetChild(i));
        }
    }

}
