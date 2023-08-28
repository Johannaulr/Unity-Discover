using UnityEngine;

public class SphereGenerator3 : MonoBehaviour
{
    [Range(0f, 1f)]
    [Tooltip("Percentage of the sphere's mesh to be rendered")]
    public float x = 1f;

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

    private void GenerateSphere()
    {
        int resolution = 100; // Number of vertices per circle
        int numCircles = 50; // Number of circles along the vertical axis

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(resolution + 1) * numCircles + 2];
        int[] triangles = new int[resolution * numCircles * 6];

        vertices[0] = Vector3.up * 0.5f;
        for (int circleIndex = 0; circleIndex <= numCircles; circleIndex++)
        {
            float v = (float)circleIndex / numCircles;
            float latitude = (v - 0.5f) * Mathf.PI;
            float sinLatitude = Mathf.Sin(latitude);
            float cosLatitude = Mathf.Cos(latitude);

            for (int i = 0; i <= resolution; i++)
            {
                float u = (float)i / resolution;
                float longitude = u * 2f * Mathf.PI;
                float sinLongitude = Mathf.Sin(longitude);
                float cosLongitude = Mathf.Cos(longitude);

                int vertexIndex = i + circleIndex * (resolution + 1) + 1;
                vertices[vertexIndex] = new Vector3(sinLatitude * cosLongitude, cosLatitude, sinLatitude * sinLongitude) * 0.5f;
            }
        }

        int triangleIndex = 0;
        for (int circleIndex = 0; circleIndex < numCircles; circleIndex++)
        {
            for (int i = 0; i < resolution; i++)
            {
                int vertexIndex = i + circleIndex * (resolution + 1) + 1;

                if (x == 1f || Random.value <= x)
                {
                    triangles[triangleIndex] = vertexIndex;
                    triangles[triangleIndex + 1] = vertexIndex + resolution + 1;
                    triangles[triangleIndex + 2] = vertexIndex + resolution + 2;

                    triangles[triangleIndex + 3] = vertexIndex;
                    triangles[triangleIndex + 4] = vertexIndex + resolution + 2;
                    triangles[triangleIndex + 5] = vertexIndex + 1;

                    triangleIndex += 6;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
        meshRenderer.enabled = true;
    }
}