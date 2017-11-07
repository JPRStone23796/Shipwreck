using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecileMeshArc : MonoBehaviour {

    /// <summary>
    /// https://www.youtube.com/watch?v=TXHK1nPUOBE&t=387s
    /// </summary>


    Mesh mesh;
    public float meshWidth;
    public float velocity;
    public float angle;
    public int resolution;
    public Vector3 ProjectileLand;
    float gravity;
    float radianAngle;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        gravity = Mathf.Abs(Physics.gravity.y);
        ProjectileLand = Vector3.zero;
        
    }

    void FixedUpdate()
    {
        if (ProjectileLand != Vector3.zero)
        {
            MakeArcMesh(CalculateArcArray());

        }

        if (ProjectileLand == Vector3.zero)
        {
            mesh.Clear();
        }

    }


    


    void MakeArcMesh( Vector3[] arcVerts)
    {
        mesh.Clear();
        Vector3[] verts = new Vector3[(resolution+1)* 2];
        int[] triangles = new int[resolution * 6 * 2];

        for(int i = 0; i<=resolution;i++)
        {
            verts[i * 2] = new Vector3(meshWidth * 0.5f,arcVerts[i].y, arcVerts[i].x);
            verts[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            if(i!=resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;


                triangles[i * 12 + 6 ] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;

            }

            mesh.vertices = verts;
            mesh.triangles = triangles;
            
        }
        transform.LookAt(ProjectileLand);
    }

        Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = ((ProjectileLand - transform.position).magnitude) * 0.8f;

        velocity = (maxDistance * gravity) / Mathf.Sin(2 * radianAngle);
        velocity = Mathf.Sqrt(velocity);

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);

        }

        return arcArray;

    }


    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y, 0);
    }
}
