using System;
using ExampleAssets.Business_Logic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    JsonParser jsonParser;
    ModelLoader loader;
    public Element element;
    // Start is called before the first frame update
    void Start()
    {
        jsonParser = new JsonParser();
        loader = GetComponent<ModelLoader>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadModel(Image image)
    {
        var Name = image.sprite.name[(image.sprite.name.LastIndexOf('-') + 1)..];
        Debug.Log(Name);
        element = jsonParser.GetElementByName(Name);
        loader.DownloadFile(element.BohrModel3D);
        Debug.Log(element.BohrModel3D);
    }
}