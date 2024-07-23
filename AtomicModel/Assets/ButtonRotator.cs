using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonRotator : MonoBehaviour
{
    private List<RectTransform> buttonTransforms;
    private DeviceOrientation lastOrientation;

    void Start()
    {
        // Find all buttons in the ScrollView and store their RectTransforms
        buttonTransforms = new List<RectTransform>();
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            buttonTransforms.Add(button.GetComponent<RectTransform>());
        }

        // Initialize the last orientation
        lastOrientation = Input.deviceOrientation;
    }

    void Update()
    {
        // Check if the device orientation has changed
        DeviceOrientation currentOrientation = Input.deviceOrientation;
        if (currentOrientation != lastOrientation)
        {
            // Update the rotation of the buttons based on the current orientation
            UpdateButtonRotation(currentOrientation);
            lastOrientation = currentOrientation;
        }
    }

    void UpdateButtonRotation(DeviceOrientation orientation)
    {
        float rotationAngle = 0f;
        switch (orientation)
        {
            case DeviceOrientation.Portrait:
                rotationAngle = 0f;
                break;
            case DeviceOrientation.LandscapeLeft:
                rotationAngle = -90f;
                break;
            case DeviceOrientation.LandscapeRight:
                rotationAngle = 90f;
                break;
            case DeviceOrientation.PortraitUpsideDown:
                rotationAngle = 180f;
                break;
            case DeviceOrientation.FaceUp:
            case DeviceOrientation.FaceDown:
            case DeviceOrientation.Unknown:
            default:
                // Do nothing or set a default rotation if desired
                return;
        }

        foreach (RectTransform rectTransform in buttonTransforms)
        {
            rectTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}
