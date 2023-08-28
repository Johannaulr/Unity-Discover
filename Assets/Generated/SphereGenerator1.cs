using UnityEngine;

public class SphereGenerator1 : MonoBehaviour
{
    [Tooltip("Radius of the sphere")]
    public float radius = 1f;

    [Tooltip("Number of latitude segments")]
    public int latitudeSegments = 10;

    [Tooltip("Number of longitude segments")]
    public int longitudeSegments = 10;

    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        GenerateSphere();
    }

    private void GenerateSphere()
    {
        Mesh mesh = new Mesh();

        int numVertices = (latitudeSegments + 1) * (longitudeSegments + 1);
        Vector3[] vertices = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[latitudeSegments * longitudeSegments * 6];

        int index = 0;
        for (int lat = 0; lat <= latitudeSegments; lat++)
        {
            float theta = lat * Mathf.PI / latitudeSegments;
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float phi = lon * 2f * Mathf.PI / longitudeSegments;
                float sinPhi = Mathf.Sin(phi);
                float cosPhi = Mathf.Cos(phi);

                float x = cosPhi * sinTheta;
                float y = cosTheta;
                float z = sinPhi * sinTheta;

                vertices[index] = new Vector3(x, y, z) * radius;
                uv[index] = new Vector2((float)lon / longitudeSegments, (float)lat / latitudeSegments);

                index++;
            }
        }

        index = 0;
        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int current = lat * (longitudeSegments + 1) + lon;
                int next = current + longitudeSegments + 1;

                triangles[index] = current;
                triangles[index + 1] = next + 1;
                triangles[index + 2] = current + 1;

                triangles[index + 3] = current;
                triangles[index + 4] = next;
                triangles[index + 5] = next + 1;

                index += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}