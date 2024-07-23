using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonColorAnalyzer : MonoBehaviour
{
    public Image backgroundImage; // Reference to the background image
    private List<Button> buttons;

    void Start()
    {
        // Find all buttons in the ScrollView
        buttons = new List<Button>(GetComponentsInChildren<Button>());

        foreach (Button button in buttons)
        {
            // Add a listener to each button
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        // Get the color of the clicked button's image
        Image buttonImage = clickedButton.GetComponent<Image>();
        if (buttonImage != null && buttonImage.sprite != null && buttonImage.sprite.texture != null)
        {
            Texture2D buttonTexture = buttonImage.sprite.texture;

            // Ensure the texture is readable
            if (buttonTexture.isReadable)
            {
                Color averageColor = GetAverageColor(buttonTexture);

                // Change the background image color to match the average color
                backgroundImage.color = averageColor;
            }
            else
            {
                Debug.LogWarning("Texture is not readable. Please enable the Read/Write option in the texture import settings.");
            }
        }
    }

    Color GetAverageColor(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        float r = 0, g = 0, b = 0, a = 0.4f; //set some transparency

        foreach (Color pixel in pixels)
        {
            r += pixel.r;
            g += pixel.g;
            b += pixel.b;
        }

        float totalPixels = pixels.Length;
        return new Color(r / totalPixels, g / totalPixels, b / totalPixels, a);
    }
}
