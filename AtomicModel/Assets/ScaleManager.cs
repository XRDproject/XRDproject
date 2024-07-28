using UnityEngine;
using UnityEngine.UI;

public class ScaleManager : MonoBehaviour
{
    public Slider scaleSlider; // Reference to the UI slider
    private GameObject currentModel; // Reference to the current model
    private Vector3 initialScale = new(2, 2, 2); // Initial scale of the model
    private float currentScaleValue = 1f; // Current slider value for scaling

    void Start()
    {
        if (scaleSlider == null)
        {
            Debug.LogError("Scale Slider is not assigned.");
            return;
        }

        // Add listener to the slider
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged);

        //// Initially hide the slider
        //SetScaleSlider(false);
    }

    public void SetModel(GameObject newModel)
    {
        // Update the current model
        currentModel = newModel;

        // Apply the current scale to the new model
        currentModel.transform.localScale = initialScale * currentScaleValue;

        // Show the slider
       SetScaleSlider(true);
    }

    public void SetScaleSlider(bool active)
    {
        scaleSlider.gameObject.SetActive(active);
    }

    void OnSliderValueChanged(float value)
    {
        currentScaleValue = value;

        if (currentModel != null)
        {
            // Scale the model based on the slider value
            currentModel.transform.localScale = initialScale * currentScaleValue;
        }
    }
}
