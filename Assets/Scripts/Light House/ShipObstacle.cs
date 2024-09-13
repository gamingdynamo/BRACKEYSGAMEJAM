using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipNav ship = other.GetComponent<ShipNav>();

        if (!ship)
            return;

        ShipNav.shipsCrashed.Add(ship);
        ship.collisionEvent?.Invoke();

    }
}
