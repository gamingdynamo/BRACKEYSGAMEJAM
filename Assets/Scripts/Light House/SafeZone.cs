using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipNav ship = other.GetComponent<ShipNav>();

        if (!ship)
            return;

        ShipNav.shipsSaved.Add(ship);
        ship.savedEvent?.Invoke();
    }
}
