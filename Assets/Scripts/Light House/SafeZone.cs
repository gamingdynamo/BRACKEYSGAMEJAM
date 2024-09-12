using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private int saved;

    [SerializeField] private List<ShipNav> savedShips;

    private void OnTriggerEnter(Collider other)
    {
        ShipNav ship = other.GetComponent<ShipNav>();

        if (!ship)
            return;

        savedShips.Add(ship);

        ship.gameObject.SetActive(false);
    }
}
