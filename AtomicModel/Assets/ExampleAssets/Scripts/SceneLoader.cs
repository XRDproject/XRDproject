using System;
using ExampleAssets.Business_Logic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //JsonParser jsonParser = new JsonParser();
        //Element hydrogen = jsonParser.GetElementByName("Hydrogen");
        //Debug.Log(hydrogen);
        ModelLoader loader = GetComponent<ModelLoader>();
        //Debug.Log(hydrogen.BohrModel3D.ToString());
        //loader.DownloadFile(hydrogen.BohrModel3D.ToString());
        loader.DownloadFile("https://storage.googleapis.com/search-ar-edu/periodic-table/element_003_lithium/element_003_lithium.glb");
    }

    // Update is called once per frame
    void Update()
    {

    }
}