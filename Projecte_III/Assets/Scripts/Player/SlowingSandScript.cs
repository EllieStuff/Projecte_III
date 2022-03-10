using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SlowingSandScript : MonoBehaviour
{
    public static bool CheckIfOnSand(Transform playerTransform)
    {
        Color ObjectiveColor = new Vector4(0.8396226f, 0.5925145f, 0.1623798f, 1);
        RaycastHit hit;
        if (!Physics.Raycast(playerTransform.position, Vector3.down, out hit, 10))
            return false;

        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
            return false;

        Debug.DrawRay(playerTransform.position, Vector3.down * hit.distance, Color.red);

        Mesh mesh = meshCollider.sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Color[] colors = mesh.colors;
        Debug.Log(colors.Length);

            //if (colors[hit.triangleIndex] == ObjectiveColor)
                //return true;
        
        return false;
    }
}
