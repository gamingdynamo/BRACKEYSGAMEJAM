using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.GraphicsBuffer;

public class ShipNav : MonoBehaviour
{
    public float speed =1f;
    public float rotationSpeed = 1f;
    public bool controled = false;
    private Vector3 destPoint;
    private Quaternion targetRotation;
    public float controlDuration = 3f;
    private float timer = 0f;
    public bool selected;
    public GameObject signal;
    private Light signalLight;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        signalLight = signal.GetComponent<Light>();
        signalLight.color = Color.black;
        controled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controled)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                bool hit = Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
                if (!hit)
                    return;

                destPoint = hitInfo.point;
                Debug.DrawRay(destPoint, Vector3.up * 5, Color.red, 2);
                destPoint = new Vector3(destPoint.x, 0, destPoint.z);
                destPoint = LerpByDistance(transform.position, destPoint, 30f);
                destPoint = destPoint - transform.position;
            }
        }
        if (!selected)
        {
            if (controled)
                signalLight.color = Color.green;
            else
                signalLight.color = Color.black;
        }

        if (selected)
        {
            signalLight.color = Color.red;
            timer += Time.deltaTime;
            if (timer >= controlDuration)
            {
                controled = !controled;
                
                timer = 0f;
            }

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

    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("selector"))
            {
                selected = true;

            }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("selector"))
        {
            selected = false;
            timer = 0f;
        }
        
    }
}
