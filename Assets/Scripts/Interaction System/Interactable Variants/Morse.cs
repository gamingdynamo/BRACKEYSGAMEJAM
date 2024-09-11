using UnityEngine;
using UnityEngine.Events;

public class Morse : MonoBehaviour
{
    string morseCode = string.Empty;
    [SerializeField] private UnityEvent onFuelOrderEvent;

    [SerializeField] private string fuelCode = "0010110";
    public void ShortSignal()
    {
        morseCode += "0";
        CheckCode();
    }
    public void LongSignal()
    {
        morseCode += "1";
        CheckCode();
    }

    public void CheckCode()
    {
        if (morseCode.Contains(fuelCode))
        {
            onFuelOrderEvent?.Invoke();
            morseCode = string.Empty;
        }
    }
}
