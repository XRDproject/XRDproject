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
    GameObject wrapper;
    [SerializeField]
    SceneLoader sceneLoader;
    Image _image;
    new TextMeshPro name;
    TextMeshPro category;
    TextMeshPro appearance;
    TextMeshPro atomicMass;
    TextMeshPro electronConfiguration;
    TextMeshPro meltingPoint;
    TextMeshPro boilingPoint;
    TextMeshPro density;
    TextMeshPro discoveredBy;
    TextMeshPro description;

    public GameObject prefabToLoad;
    private GameObject instantiatedPrefab;
     public void LoadDescription(Image image)
     { 
         _image.sprite = image.sprite;
        name.SetText(sceneLoader.element.Name);
        category.SetText(sceneLoader.element.Category);
        appearance.SetText(sceneLoader.element.Appearance);
        atomicMass.SetText(sceneLoader.element.AtomicMass.ToString(cultureInfo));
        electronConfiguration.SetText(sceneLoader.element.ElectronConfiguration);
        if (sceneLoader.element.Melt is null or 0)
        {
            meltingPoint.SetText("unknown");
        }
        else {meltingPoint.SetText(sceneLoader.element.Melt.ToString());}
        boilingPoint.SetText(sceneLoader.element.Boil.ToString());
        density.SetText(sceneLoader.element.Density.ToString());
        discoveredBy.SetText(sceneLoader.element.DiscoveredBy);
        description.SetText(sceneLoader.element.Summary);
        if (instantiatedPrefab != null)
        {
            instantiatedPrefab.SetActive(true);
        }
        centerGameObject(instantiatedPrefab, Camera.main);
    }
    void centerGameObject(GameObject gameOBJToCenter, Camera cameraToCenterOBjectTo, float zOffset = 2.1f)
    {
        gameOBJToCenter.transform.position = cameraToCenterOBjectTo.ViewportToWorldPoint(new Vector3(7f, 3f, cameraToCenterOBjectTo.nearClipPlane + zOffset));
    }
    void Start()
    {   cultureInfo = new CultureInfo("en-US");
        instantiatedPrefab = Instantiate(prefabToLoad, new Vector3(0, 0, 0), Quaternion.identity);
        Canvas canvas = instantiatedPrefab.GetComponentInChildren<Canvas>();

        if (canvas != null)
        {
            Camera eventCamera = Camera.main; 
            canvas.worldCamera = eventCamera;
            _image = canvas.transform.Find("Background/Image").GetComponent<Image>();
            name = canvas.transform.Find("Background/Name").GetComponent<TextMeshPro>();
            category = canvas.transform.Find("Background/Category").GetComponent<TextMeshPro>();
            appearance = canvas.transform.Find("Background/Appearance").GetComponent<TextMeshPro>();
            atomicMass = canvas.transform.Find("Background/Atomic Mass").GetComponent<TextMeshPro>();
            electronConfiguration = canvas.transform.Find("Background/Electron Configuration").GetComponent<TextMeshPro>();
            meltingPoint = canvas.transform.Find("Background/Melting Point").GetComponent<TextMeshPro>();
            boilingPoint = canvas.transform.Find("Background/Boiling Point").GetComponent<TextMeshPro>();
            density = canvas.transform.Find("Background/Density").GetComponent<TextMeshPro>();
            discoveredBy = canvas.transform.Find("Background/Discovered By").GetComponent<TextMeshPro>();
            description = canvas.transform.Find("Background/Summary").GetComponent<TextMeshPro>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
