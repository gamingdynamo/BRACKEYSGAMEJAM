using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private int saved;


    private void OnTriggerEnter(Collider other)
    {
        ShipNav ship = other.GetComponent<ShipNav>();

        if (!ship)
            return;

        ShipNav.shipsSaved.Add(ship);

        ship.gameObject.SetActive(false);
    }
}
