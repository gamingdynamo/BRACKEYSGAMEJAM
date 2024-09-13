using UnityEngine;

public class TaskPointer : MonoBehaviour
{

    [SerializeField] private RectTransform arrowDirection;
    [SerializeField] private RectTransform arrowUp;
    [SerializeField] private RectTransform arrowDown;
    [SerializeField] float directionOffset = 90;
    void Update()
    {
        var active = TaskSGT.Instance.GetTaskByState(Task.State.ACTIVE);

        if (active.Count == 0)
            return;

        Task current = active[0];
        Vector3 playerPos = FirstPersonController.Instance.transform.position;
        Vector3 destination = current.destination;

        // Berechne die Richtung von Spieler zu Ziel in Weltkoordinaten
        Vector3 worldDirection = destination - playerPos;
        float heightDistance = worldDirection.y;
        worldDirection.Normalize();

        // Transformiere die Welt-Richtung in den lokalen Raum des Spielers
        Vector3 localDirection = FirstPersonController.Instance.GetCamera().transform.InverseTransformDirection(worldDirection);

        // Da wir eine Topdown-Sicht haben, verwenden wir nur die X- und Z-Komponente
        Vector2 localDirection2D = new Vector2(localDirection.x, localDirection.z);

        // Berechne den Winkel, den der Pfeil anzeigen soll relativ zur Spieler-Ausrichtung
        float angle = Mathf.Atan2(localDirection2D.y, localDirection2D.x) * Mathf.Rad2Deg + directionOffset;

        // Setze die Rotation des UI-Elements basierend auf dem Winkel (um die Z-Achse drehen)
        arrowDirection.rotation = Quaternion.Euler(0, 0, angle);
        arrowUp.gameObject.SetActive(heightDistance > 2);
        arrowDown.gameObject.SetActive(heightDistance < -2);

        // Optional: Debug Ray für die Richtung im 3D-Raum
        Debug.DrawRay(playerPos, worldDirection, Color.red);
    }
}