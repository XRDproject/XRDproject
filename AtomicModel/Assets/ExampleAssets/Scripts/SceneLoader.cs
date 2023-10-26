using System;
using ExampleAssets.Business_Logic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    JsonParser jsonParser;
    ModelLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        jsonParser = new JsonParser();
        loader = GetComponent<ModelLoader>();

        //Element element = jsonParser.GetElementByName("Hydrogen");
        //Debug.Log(element);
        //ModelLoader loader = GetComponent<ModelLoader>();
        //Debug.Log(element.BohrModel3D);
        //loader.DownloadFile(element.BohrModel3D);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadModel(Image image)
    {
        var Name = image.sprite.name[(image.sprite.name.LastIndexOf('-') + 1)..];
        Debug.Log(Name);
        Element element = jsonParser.GetElementByName(Name);
        loader.DownloadFile(element.BohrModel3D);
        Debug.Log(element.BohrModel3D);
    }
}