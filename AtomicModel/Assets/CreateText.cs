using ExampleAssets.Business_Logic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    JsonParser jsonParser;
    ModelLoader loader;
    GameObject wrapper;
    [SerializeField]
    SceneLoader sceneLoader;
    [SerializeField]
     TextMeshPro description;


    public void LoadDescription()
    {
        Debug.Log(sceneLoader.element.Summary);
        description.SetText(sceneLoader.element.Summary);
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
