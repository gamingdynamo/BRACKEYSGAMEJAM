using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeScrip : MonoBehaviour
{
    public GameObject spikeGlow;
    // Start is called before the first frame update
    void Start()
    {
        spikeGlow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("selector"))
        {
            spikeGlow.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("selector"))
        {
            spikeGlow.SetActive(false);

        }
    }
}
