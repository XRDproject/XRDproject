using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;

public class ModelLoader : MonoBehaviour
{
    GameObject wrapper;
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/Files/";
        Debug.Log(filePath);
        wrapper = new GameObject
        {
            name = "Model"
        };
    }
    public void DownloadFile(string url)
    {
        Debug.Log("Try");

        string path = GetFilePath(url);
        if (File.Exists(path))
        {
            Debug.Log("Found file locally, loading...");
            LoadModel(path);
            return;
        }

        StartCoroutine(GetFileRequest(url, (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                // Log any errors that may happen
                Debug.Log($"{req.error} +_+_+_+: {req.downloadHandler.text}");
            }
            else
            {
                // Save the model into a new wrapper
                Debug.Log(path + "All good");
                LoadModel(path);
            }
        }));
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];
        return $"{filePath}{filename}";
    }

    void LoadModel(string path)
    {
        ResetWrapper();
        AnimationClip[] animationClips;
        var importSettings = new ImportSettings();
        importSettings.animationSettings.useLegacyClips = true;
        GameObject model = Importer.LoadFromFile(path, importSettings, out animationClips);
        centerGameObject(model, Camera.main);
        if (animationClips.Length > 0)
        {
            Animation animation = model.AddComponent<Animation>();
            animationClips[0].legacy = true;
            animation.AddClip(animationClips[0], animationClips[0].name);
            animation.clip = animation.GetClip(animationClips[0].name);
            animation.Play();
            animation.wrapMode = WrapMode.Loop;
        }
    }
    void centerGameObject(GameObject gameOBJToCenter, Camera cameraToCenterOBjectTo, float zOffset = 2.1f)
    {
        gameOBJToCenter.transform.position = cameraToCenterOBjectTo.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cameraToCenterOBjectTo.nearClipPlane + zOffset));
        gameOBJToCenter.transform.SetParent(wrapper.transform);
        gameOBJToCenter.transform.localScale = new Vector3(3, 3, 3);     

    }

    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            Debug.Log(GetFilePath(url) + ": " + req.result);
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            yield return req.SendWebRequest();
            callback(req);
        }
    }

    void ResetWrapper()
    {
        if (wrapper != null)
        {
            foreach (Transform trans in wrapper.transform)
            {
                Destroy(trans.gameObject);
            }
        }
    }
}