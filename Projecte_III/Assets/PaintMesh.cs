using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMesh : MonoBehaviour
{
    //El plus ultra nomes es per quan nomes s'ha de carregar molt poques vegades, plis xD
    public enum Quality { SUPER_LOW = 4, LOW = 15, NORMAL = 40, HIGH = 100, ULTRA = 200, PLUS_ULTRA = 1000 };

    public float viewDist = 4;
    public float fov = 360;
    public Quality quality;
    public bool collideWithWalls = true;
    public GameObject avatar;
    public GameObject[] hitPoints;

    Mesh mesh;
    Vector3 origin = Vector3.zero;

    int rayCount = 40;
    float angle = 0;
    float angleIncrease;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "ImageOnFloor";
        rayCount = (int)quality;
        angleIncrease = fov / rayCount;
        vertices = new Vector3[rayCount + 2]; //sumar 1 si no es completa cercle
        uv = new Vector2[vertices.Length];
        triangles = new int[rayCount * 3];


        //RefreshFOV();
        InitPainting();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, origin);
        Debug.Log("Pos: " + transform.position);
        Debug.Log("Origin: " + origin);
    }

    public void InitPainting()
    {
        RaycastHit hitHeight;
        if (Physics.Raycast(transform.position, -transform.up, out hitHeight, Mathf.Infinity))
        {
            origin = hitHeight.point;
            float maxLength = Vector3.Distance(transform.position, origin) * 2;
            Debug.Log("1 ray");

            vertices[0] = origin;
            int vertexIndex = 1;
            int trianglesIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Debug.Log("2 ray: " + i);
                Vector3 tmpVertex = origin + Utils.Vectors.GetVectorFromAngle(angle) * viewDist;

                RaycastHit hit;
                if(Physics.Raycast(transform.position, tmpVertex, out hit, 10000))
                {
                    vertices[vertexIndex] = hit.point;
                }


                //if (collideWithWalls)
                //{
                //    RaycastHit2D hits = Physics2D.Raycast(avatar.transform.position, tmpVertex - origin, viewDist, LayerMask.GetMask("Walls"));
                //    if (hits.collider != null && hits.collider.gameObject.CompareTag("Wall"))
                //        tmpVertex = hits.point - (Vector2)transform.position;
                //}
                //vertices[vertexIndex] = tmpVertex;
                //Debug.DrawRay(transform.position, vertex - origin);

                if (i > 0)
                {
                    triangles[trianglesIndex] = 0;
                    triangles[trianglesIndex + 1] = vertexIndex - 1;
                    triangles[trianglesIndex + 2] = vertexIndex;

                    trianglesIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;

            }


            /////
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;


        }

    }


    public void RefreshFOV()
    {
        transform.position = avatar.transform.position;

        vertices[0] = origin;
        int vertexIndex = 1;
        int trianglesIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + Utils.Vectors.GetVectorFromAngle(angle) * viewDist;
            if (collideWithWalls)
            {
                RaycastHit2D hits = Physics2D.Raycast(avatar.transform.position, vertex - origin, viewDist, LayerMask.GetMask("Walls"));
                if (hits.collider != null && hits.collider.gameObject.CompareTag("Wall"))
                    vertex = hits.point - (Vector2)transform.position;
            }
            vertices[vertexIndex] = vertex;
            //Debug.DrawRay(transform.position, vertex - origin);

            if (i > 0)
            {
                triangles[trianglesIndex] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                trianglesIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;

        }


        /////
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;



    }


}
