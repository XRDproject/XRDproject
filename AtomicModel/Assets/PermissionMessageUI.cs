using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PermissionMessageUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public void ShowMessage(string message)
    {
        messageText.SetText(message);
        gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
    }
}