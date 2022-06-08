using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.5f;
    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        render.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
