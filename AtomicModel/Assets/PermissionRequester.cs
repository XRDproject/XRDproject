using UnityEngine;
using UnityEngine.Android;

public class PermissionRequester : MonoBehaviour
{
    void Start()
    {
        RequestPermissions();
    }

    void RequestPermissions()
    {
        // Check and request Camera permission
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            CheckPermissions();
        }
    }

    void CheckPermissions()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            ShowPermissionDeniedMessage("Camera");
        }
    }

    void ShowPermissionDeniedMessage(string permissionName)
    {
        string message = $"{permissionName} permission is required for the app to function properly.";
        Debug.Log(message);

        // Assuming you have a reference to the PermissionMessageUI component
        PermissionMessageUI permissionMessageUI = FindObjectOfType<PermissionMessageUI>();
        if (permissionMessageUI != null)
        {
            permissionMessageUI.ShowMessage(message);
        }
    }
}
