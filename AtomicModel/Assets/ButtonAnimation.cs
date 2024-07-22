using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonAnimation : MonoBehaviour
{
    private Button button;
    private Vector3 originalScale;
    public float animationDuration = 0.1f;
    public float scaleFactor = 0.8f;

    void Start()
    {
        button = GetComponent<Button>();
        originalScale = button.transform.localScale;
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        StartCoroutine(AnimateButton());
    }

    IEnumerator AnimateButton()
    {
        // Shrink the button
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            button.transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleFactor, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        button.transform.localScale = originalScale * scaleFactor;

        // Wait for a moment
        yield return new WaitForSeconds(0.1f);

        // Expand the button back to original size
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            button.transform.localScale = Vector3.Lerp(originalScale * scaleFactor, originalScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        button.transform.localScale = originalScale;
    }
}
