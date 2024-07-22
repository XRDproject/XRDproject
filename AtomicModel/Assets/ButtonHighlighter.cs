using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonHighlighter : MonoBehaviour
{

    public Color highlightedColor = new Color(1f, 1f, 1f, 0.5f); // Semi-transparent color
    public Color defaultColor = Color.white; // Default color of the button
    private List<Button> buttons;
    private Button highlightedButton = null;

    void Start()
    {
        // Find all buttons in the ScrollView
        buttons = new List<Button>(GetComponentsInChildren<Button>());

        foreach (Button button in buttons)
        {
            // Store the default color
            button.GetComponent<Image>().color = defaultColor;

            // Add a listener to each button
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        if (highlightedButton != null)
        {
            // Restore the default color of the previously highlighted button
            highlightedButton.GetComponent<Image>().color = defaultColor;
        }

        // Change the color of the clicked button to the highlighted color
        clickedButton.GetComponent<Image>().color = highlightedColor;

        // Set the clicked button as the highlighted button
        highlightedButton = clickedButton;
    }




    //public GameObject framePrefab; // Reference to the frame prefab
    //private List<Button> buttons;
    //private GameObject currentFrame;
    //private Button highlightedButton = null;

    //void Start()
    //{
    //    // Find all buttons in the ScrollView
    //    buttons = new List<Button>(GetComponentsInChildren<Button>());

    //    foreach (Button button in buttons)
    //    {
    //        // Add a listener to each button
    //        button.onClick.AddListener(() => OnButtonClick(button));
    //    }

    //    // Instantiate the frame and set it to inactive
    //    currentFrame = Instantiate(framePrefab);
    //    currentFrame.SetActive(false);
    //}

    //void OnButtonClick(Button clickedButton)
    //{
    //    if (highlightedButton != null)
    //    {
    //        // Hide the frame around the previously highlighted button
    //        currentFrame.SetActive(false);
    //    }

    //    // Position the frame behind the clicked button
    //    RectTransform buttonRectTransform = clickedButton.GetComponent<RectTransform>();
    //    RectTransform frameRectTransform = currentFrame.GetComponent<RectTransform>();

    //    // Set the frame as a child of the clicked button
    //    frameRectTransform.SetParent(buttonRectTransform, false);

    //    // Set the frame's sibling index to 0 to ensure it is behind the button
    //    frameRectTransform.SetSiblingIndex(-1);

    //    // Set the size and position of the frame to match the button
    //    frameRectTransform.anchorMin = new Vector2(0, 0);
    //    frameRectTransform.anchorMax = new Vector2(1, 1);
    //    frameRectTransform.anchoredPosition = new Vector2(0, 0);
    //    frameRectTransform.sizeDelta = new Vector2(0, 0);

    //    // Activate the frame to make it visible
    //    currentFrame.SetActive(true);

    //    // Set the clicked button as the highlighted button
    //    highlightedButton = clickedButton;
    //}






}
