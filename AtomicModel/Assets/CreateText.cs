using ExampleAssets.Business_Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    JsonParser jsonParser;
    ModelLoader loader;
    GameObject wrapper;

    public void LoadDescription(Image image)
    {
        var Name = image.sprite.name[(image.sprite.name.LastIndexOf('-') + 1)..];
        Debug.Log(Name);
        Element element = jsonParser.GetElementByName(Name);
       // loader.DownloadFile(.element.Summary);
        Debug.Log(element.Summary);
        //centerGameObject(model, Camera.main);
    }
    void centerGameObject(GameObject gameOBJToCenter, Camera cameraToCenterOBjectTo, float zOffset = 2.1f)
    {
        gameOBJToCenter.transform.position = cameraToCenterOBjectTo.ViewportToWorldPoint(new Vector3(-3f, 2f, cameraToCenterOBjectTo.nearClipPlane + zOffset));
        gameOBJToCenter.transform.SetParent(wrapper.transform);
        gameOBJToCenter.transform.localScale = new Vector3(3, 3, 3);

    }
    // Start is called before the first frame update
    void Start()
    {
        jsonParser = new JsonParser();
        loader = GetComponent<ModelLoader>();
        wrapper = new GameObject
        {
            name = "Description"
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
