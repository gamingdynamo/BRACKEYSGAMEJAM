using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LighthouseEngine : MonoBehaviour
{
    [SerializeField] private LighthouseController controller;
    [SerializeField] private TextMeshProUGUI fuelDisplay;

    [Header("Settings")]
    [SerializeField] private int startingFuel = 0;
    [SerializeField] private int maxFuel = 100;
    [SerializeField] private int usagePerSecond = 1;
    [SerializeField] private int fillAmountPerFuelCan = 35;
    [SerializeField] private float tick = 1;

    [Header("Events")]
    [SerializeField] private UnityEvent onRefueledEvent;
    [SerializeField] private UnityEvent onEmptyEvent;

    [Header("Debug")]
    [SerializeField] private int fuelAmount = 100;

    private bool isEmpty = false;
    

    private InteractableComponent interactable => GetComponent<InteractableComponent>();

    private void Start()
    {
        fuelAmount = Mathf.Clamp(startingFuel, 0, maxFuel);

        StartCoroutine(FuelTick());
    }

    IEnumerator FuelTick()
    {
        while (true)
        {
            
            fuelAmount = Mathf.Max(fuelAmount - usagePerSecond, 0);

            if (fuelAmount <= 0 && !isEmpty)
            {
                isEmpty = true;
                onEmptyEvent?.Invoke();
            }

            UpdateFuelDisplay();
            yield return new WaitForSeconds(tick);

        }
    }

    private void UpdateFuelDisplay() => fuelDisplay.text = $"Fuel: {fuelAmount} / {maxFuel}";

    public void Refuel()
    {
        if (fillAmountPerFuelCan <= 0)
            return;
        isEmpty = false;
        fuelAmount = Mathf.Min(fuelAmount + fillAmountPerFuelCan, maxFuel);
        onRefueledEvent?.Invoke();
        UpdateFuelDisplay();
    }
}
