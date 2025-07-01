using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private TextMeshProUGUI _engineText;

    public void SetFuelValue(float value)
    {
        var percentValue = (value * 100).ToString("F1");
        _fuelText.text = $"Fuel: {percentValue}%";
        _slider.value = value;
    }

    public void SetEngineStatus(bool isRunning)
    {
        var engineStatus = isRunning ? "Running" : "Stopped";
        _engineText.text = $"Engine: {engineStatus}";
    }
}
