using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonColorAnalyzerWithSound : MonoBehaviour
{
    public Image backgroundImage; // Reference to the background image
    public Image sliderImage; // Reference to the slider image
    public Image handleImage; // Reference to the slider handle image
    public Image handleImage2; // Reference to the slider handle image
    public Image expandButton; // Reference to the expand button image
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip clickSound; // Reference to the AudioClip for the button click sound
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
        // Play the click sound
        PlayClickSound();

        // Get the color of the clicked button's image
        Image buttonImage = clickedButton.GetComponent<Image>();
        if (buttonImage != null && buttonImage.sprite != null && buttonImage.sprite.texture != null)
        {
            Texture2D buttonTexture = buttonImage.sprite.texture;

            // Ensure the texture is readable
            if (buttonTexture.isReadable)
            {
                Color averageColor = GetAverageColor(buttonTexture);
                Color averageColorNotTransparent = GetAverageColor(buttonTexture, transparent: false);

                // Change the background image color to match the average color
                backgroundImage.color = averageColor;
                sliderImage.color = averageColor;
                handleImage.color = averageColorNotTransparent;
                handleImage2.color = averageColorNotTransparent;
                expandButton.color = averageColorNotTransparent;
            }
            else
            {
                Debug.LogWarning("Texture is not readable. Please enable the Read/Write option in the texture import settings.");
            }
        }
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or ClickSound not assigned.");
        }
    }
    Color GetAverageColor(Texture2D texture, bool transparent = true)
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

        if (transparent)
        {
            return new Color(r / totalPixels, g / totalPixels, b / totalPixels, a);

        }
        else
        {
            return new Color(r / totalPixels, g / totalPixels, b / totalPixels);
        }

    }
}
