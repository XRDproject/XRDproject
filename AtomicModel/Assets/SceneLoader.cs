
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
