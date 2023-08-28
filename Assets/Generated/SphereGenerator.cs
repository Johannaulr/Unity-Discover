using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("The value of x that determines the number of vertices in the sphere mesh.")]
    public float x;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        GenerateSphere();
    }

    private void Update()
    {
        GenerateSphere();
    }

    private void GenerateSphere()
    {
        int numVertices = Mathf.RoundToInt(x * 100);
        int numTriangles = numVertices * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles];

        for (int i = 0; i < numVertices; i++)
        {
            float angle = i * Mathf.PI * 2f / numVertices;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            vertices[i] = new Vector3(x, y, 0f);
        }

        for (int i = 0; i < numVertices - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        triangles[(numVertices - 1) * 3] = 0;
        triangles[(numVertices - 1) * 3 + 1] = numVertices;
        triangles[(numVertices - 1) * 3 + 2] = 1;

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
	    mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}