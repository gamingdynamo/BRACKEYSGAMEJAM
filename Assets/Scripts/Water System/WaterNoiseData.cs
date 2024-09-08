using UnityEngine;

[CreateAssetMenu(fileName = "WaterNoiseData", menuName = "ScriptableObjects/WaterNoiseData", order = 1)]
public class WaterNoiseData : ScriptableObject
{
    // Parameters for the first noise layer
    [Header ("Wave Layer 1")]
    public float frequency1 = 0.1f;
    public float amplitude1 = 1.0f;
    public Vector2 direction1 = new Vector2(1, 0); // Direction for the first noise layer

    // Parameters for the second noise layer
    [Header("Wave Layer 2")]
    public float frequency2 = 0.2f;
    public float amplitude2 = 0.5f;
    public Vector2 direction2 = new Vector2(0, 1); // Direction for the second noise layer

    [Header("Overall Modifier")]
    public float frequency = 0.2f;
    public float amplitude = 0.2f;
    public float speed = 0.2f;



    // Method to calculate the height at a given position with two noise layers
    public float GetHeightAtPosition(Vector3 position)
    {
        // First noise layer
        float height1 = GetNoiseHeight(position, frequency1 * frequency, amplitude1 * amplitude, direction1 * speed);

        // Second noise layer
        float height2 = GetNoiseHeight(position, frequency2 * frequency, amplitude2 * amplitude, direction2 * speed);

        // Combine the heights from both layers
        return height1 + height2;
    }

    // Method to calculate noise height for a given position and parameters
    private float GetNoiseHeight(Vector3 position, float frequency, float amplitude, Vector2 direction)
    {
        float xCoord = position.x * frequency + direction.x * Time.time;
        float zCoord = position.z * frequency + direction.y * Time.time;

        return Mathf.PerlinNoise(xCoord, zCoord) * amplitude;
    }

    // Method to get the normal direction at a given position
    public Vector3 GetNormalAtPosition(Vector3 position)
    {
        float delta = 0.1f; // Small offset to calculate the gradient

        // Get heights at surrounding points
        float heightCenter = GetHeightAtPosition(position);
        float heightX1 = GetHeightAtPosition(position + new Vector3(delta, 0, 0));
        float heightX2 = GetHeightAtPosition(position - new Vector3(delta, 0, 0));
        float heightZ1 = GetHeightAtPosition(position + new Vector3(0, 0, delta));
        float heightZ2 = GetHeightAtPosition(position - new Vector3(0, 0, delta));

        // Calculate gradients in the X and Z directions
        Vector3 gradientX = new Vector3(delta, heightX1 - heightX2, 0);
        Vector3 gradientZ = new Vector3(0, heightZ1 - heightZ2, delta);

        // Compute the normal vector using the cross product of the gradients
        Vector3 normal = -Vector3.Cross(gradientX, gradientZ).normalized;

        return normal;
    }
}
