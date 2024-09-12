using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private UnityEvent onObjectSpawned;
    public void Spawn()
    {
        GameObject newObject = Instantiate(spawnObject);
        newObject.SetActive(true);
        Vector3 rndOffset = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        newObject.transform.position = transform.position + rndOffset;
        newObject.transform.rotation = transform.rotation;
        onObjectSpawned?.Invoke();
    }

}
