using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private uint maxSpawnAmount;

    [SerializeField] private UnityEvent onObjectSpawned;

    private uint currentSpawnAmount = 0;
    public void Spawn()
    {
        if (currentSpawnAmount >= maxSpawnAmount)
            return;

        GameObject newObject = Instantiate(spawnObject);
        newObject.SetActive(true);
        Vector3 rndOffset = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        newObject.transform.position = transform.position + rndOffset;
        newObject.transform.rotation = transform.rotation;
        currentSpawnAmount++;
        onObjectSpawned?.Invoke();
    }

}
