using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private WaterNoiseData noiseData; 
    [Range(0.01f, 1), SerializeField] private float rotationInfluence = 0.5f;

    private void Update()
    {
        float waterHeight = noiseData.GetHeightAtPosition(transform.position);
        Vector3 waterNormal = noiseData.GetNormalAtPosition(transform.position);
        waterNormal = Vector3.Lerp(Vector3.up, waterNormal, rotationInfluence);

        Vector3 targetPosition = new Vector3(transform.position.x, waterHeight, transform.position.z);

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.Cross(transform.right,waterNormal), waterNormal);

        targetRotation = Quaternion.Euler (
            targetRotation.eulerAngles.x,
            transform.eulerAngles.y,
            targetRotation.eulerAngles.z
            );

        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}