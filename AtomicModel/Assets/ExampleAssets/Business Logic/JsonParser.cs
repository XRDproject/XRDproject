using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;


namespace ExampleAssets.Business_Logic
{
    public class JsonParser
    {
      private  ElementList _elementList;
        public JsonParser()
        {
            string jsonString = Resources.Load<TextAsset>("PeriodicTableJSON").text;
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
     
    }
}
