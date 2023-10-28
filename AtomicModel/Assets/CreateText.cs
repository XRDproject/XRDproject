using System;
using ExampleAssets.Business_Logic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    CultureInfo cultureInfo;
    JsonParser jsonParser;
    ModelLoader loader;
    GameObject wrapper;
    [SerializeField]
    SceneLoader sceneLoader;
    [SerializeField]
    new TextMeshPro name;
    [SerializeField]
    TextMeshPro category;
    [SerializeField]
    TextMeshPro appearance;
    [SerializeField]
    TextMeshPro atomicMass;
    [SerializeField]
    TextMeshPro electronConfiguration;
    [SerializeField]
    TextMeshPro meltingPoint;
    [SerializeField]
    TextMeshPro boilingPoint;
    [SerializeField]
    TextMeshPro density;
    [SerializeField]
    TextMeshPro discoveredBy;
    [SerializeField]
    TextMeshPro description;


     public void LoadDescription()
    {
        Debug.Log(sceneLoader.element.Summary);
        name.SetText(sceneLoader.element.Name);
        category.SetText(sceneLoader.element.Category);
        appearance.SetText(sceneLoader.element.Appearance);
        atomicMass.SetText(sceneLoader.element.AtomicMass.ToString(cultureInfo));
        electronConfiguration.SetText(sceneLoader.element.ElectronConfiguration);
        meltingPoint.SetText(sceneLoader.element.Melt.ToString());
        boilingPoint.SetText(sceneLoader.element.Boil.ToString());
        density.SetText(sceneLoader.element.Density.ToString());
        discoveredBy.SetText(sceneLoader.element.DiscoveredBy);
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
    {   cultureInfo = new CultureInfo("en-US");
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
