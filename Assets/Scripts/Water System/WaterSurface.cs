using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class WaterSurface : MonoBehaviour
{
    public WaterNoiseData noiseData; 
    public int width = 10;
    public int length = 10;
    public int resolution = 10;

    private Mesh mesh;

    private void Start()
    {
        GenerateMesh();
    }

    private void Update()
    {
        UpdateMesh();
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        int[] triangles = new int[resolution * resolution * 6];

        // Generate vertices and triangles
        for (int z = 0, i = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++, i++)
            {
                float xPos = (float)x / resolution * width;
                float zPos = (float)z / resolution * length;
                vertices[i] = new Vector3(xPos, 0, zPos);
            }
        }

        for (int z = 0, vert = 0, tris = 0; z < resolution; z++, vert++)
        {
            for (int x = 0; x < resolution; x++, vert++, tris += 6)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + resolution + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + resolution + 1;
                triangles[tris + 5] = vert + resolution + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void UpdateMesh()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);   
            vertices[i].y = noiseData.GetHeightAtPosition(worldPos);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(width / 2, 0, length / 2), new Vector3(width, 0, length));
    }
}
