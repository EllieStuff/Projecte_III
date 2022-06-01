using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUInstantiate : MonoBehaviour
{
    public int instances;
    public Mesh mesh;
    public Material[] materials;
    private List<List<Matrix4x4>> batches = new List<List<Matrix4x4>>();

    float timer = 0.5f;
    bool created = false;

    private void RenderBatches()
    {
        foreach (var batch in batches)
        {
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                Graphics.DrawMeshInstanced(mesh, i, materials[i], batch);
            }
        }
    }

    private void Update()
    {
        if (timer <= 0 && !created)
        {
            _Start();
            created = true;
        }
        else if (timer > 0)
            timer -= Time.deltaTime;

        if(created)
        RenderBatches();
    }

    private void Start()
    {
        instances = 1;
        MeshFilter _mesh = GetComponent<MeshFilter>();
        MeshRenderer _renderer = GetComponent<MeshRenderer>();
        mesh = _mesh.sharedMesh;
        materials = _renderer.sharedMaterials;
        Destroy(_mesh);
        Destroy(_renderer);
    }

    private void _Start()
    {
        int addedMatrices = 0;
        batches.Add(new List<Matrix4x4>());

        for (int i = 0; i < instances; i++)
        {
            if (addedMatrices < 1000)
            {
                batches[batches.Count - 1].Add(Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale));
                addedMatrices += 1;
            }
            else
            {
                batches.Add(new List<Matrix4x4>());
                addedMatrices = 0;
            }
        }
    }
}
