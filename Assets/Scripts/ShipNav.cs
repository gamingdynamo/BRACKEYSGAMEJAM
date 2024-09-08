using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShipNav : MonoBehaviour
{
    public float speed =1f;
    public float rotationSpeed = 1f;
    public bool controled = false;
    private Vector3 destPoint;
    private Quaternion targetRotation;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        controled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0))
            {
                destPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                destPoint = new Vector3(destPoint.x, 0,destPoint.z);
                destPoint = LerpByDistance(transform.position, destPoint, 30f);
                destPoint = destPoint - transform.position;
            }
        
        targetRotation= Quaternion.LookRotation(destPoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }
}
