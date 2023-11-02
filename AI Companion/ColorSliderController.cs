using UnityEngine;
using UnityEngine.UI;

public class ColorSliderController : MonoBehaviour
{
    public Slider slider; // Reference to the Slider component in the scene
    public Material targetMaterial; // Reference to the material you want to change

    private void Start()
    {
        // Add a listener to the slider to detect value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        GameObject room = GameObject.Find("Room");
       // targetMaterial = FindObjectOfType<Material>();
    }

    private void OnSliderValueChanged(float value)
    {
        // Calculate the color based on the slider value (0-1)
        Color newColor = new Color(value, 1f - value, 0f); // Example: R=sliderValue, G=1-sliderValue, B=0

        // Apply the new color to the material
        targetMaterial.color = newColor;
    }
}
