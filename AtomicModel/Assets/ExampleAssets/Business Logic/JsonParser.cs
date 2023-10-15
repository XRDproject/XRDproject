using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


namespace ExampleAssets.Business_Logic
{
    public class JsonParser
    {
      private  ElementList _elementList;
        public JsonParser()
        {
            string jsonString = LoadJsonFile("./ExampleAssets/Business Logic/PeriodicTableJSON.json");
             _elementList = JsonConvert.DeserializeObject<ElementList>(jsonString);
        }

        public Element GetElementByName(string elementName)
        {
            return _elementList.elements.FirstOrDefault(elementToFind => elementToFind.Name == elementName);
        }
        public Element[] GetAllElements()
        {
            return _elementList.elements;
        }
        private string LoadJsonFile(string relativePath)
        {
            string filePath = System.IO.Path.Combine(Application.dataPath, relativePath);

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    return System.IO.File.ReadAllText(filePath);
                }
                else
                {
                    Debug.LogError($"File not found at path: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading JSON file: {ex.Message}");
            }

            return null;
        }
    }
}
