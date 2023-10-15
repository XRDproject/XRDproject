using System;
using ExampleAssets.Business_Logic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonParser jsonParser = new JsonParser();
        Element hydrogen = jsonParser.GetElementByName("Hydrogen");
        Debug.Log(jsonParser.GetElementByName("Hydrogen"));
        ModelLoader loader = new ModelLoader();
        Debug.Log(hydrogen.BohrModel3D.ToString());
        loader.DownloadFile(hydrogen.BohrModel3D.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}