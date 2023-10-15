using System;
using ExampleAssets.Business_Logic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonParser jsonParser = new JsonParser();

        Element element = jsonParser.GetElementByName("Hydrogen");
        Debug.Log(element);
        ModelLoader loader = GetComponent<ModelLoader>();
        Debug.Log(element.BohrModel3D);
        loader.DownloadFile(element.BohrModel3D);
        //loader.DownloadFile("https://storage.googleapis.com/search-ar-edu/periodic-table/element_003_lithium/element_003_lithium.glb");
    }

    // Update is called once per frame
    void Update()
    {

    }
}