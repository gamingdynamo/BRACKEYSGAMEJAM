using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] UnityEvent onPreCounterStarted;
    [SerializeField] UnityEvent onWaveStarted;
    [SerializeField] UnityEvent onWavesEnded;

    [SerializeField] private List<Wave> waves = new List<Wave>();


    public void StartWaves(float precounter)
    {
        if (waves.Count == 0)
            return;

        StartCoroutine(StartWaves());

        IEnumerator StartWaves()
        {
            onPreCounterStarted?.Invoke();
            yield return new WaitForSeconds(precounter);

            for (int i = 0;  i < waves.Count; i++)
            {
                onWaveStarted?.Invoke();
                Wave wave = waves[i];
                foreach (GameObject go in wave.ships)
                    go.SetActive(true);
                yield return new WaitForSeconds(wave.duration);
            }

            while (true)
            {
                var saved = ShipNav.shipsSaved.Count;
                var crashed = ShipNav.shipsCrashed.Count;
                var all = ShipNav.allShips.Count;
                if (saved + crashed >= all)
                {
                    onWavesEnded?.Invoke();
                    break;
                }
                yield return null;
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public float duration;
    public List<GameObject> ships = new List<GameObject>();
}