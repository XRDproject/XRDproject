using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExampleAssets.Business_Logic
{
    public class Element
    {
        public string Name { get; set; }
        [CanBeNull] public string Appearance { get; set; }
        public double AtomicMass { get; set; }
        public double? Boil { get; set; }
        public string Category { get; set; }
        public double? Density { get; set; }
        public string DiscoveredBy { get; set; }
        public double? Melt { get; set; }
        public double? MolarHeat { get; set; }
        [CanBeNull] public string NamedBy { get; set; }
        public int Number { get; set; }
        public int Period { get; set; }
        public int Group { get; set; }
        public string Phase { get; set; }
        public string Source { get; set; }
        public string BohrModelImage { get; set; }
        [JsonProperty("bohr_model_3d")]
        public string BohrModel3D { get; set; }
        [CanBeNull] public string SpectralImg { get; set; }
        public string Summary { get; set; }
        public string Symbol { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int Wxpos { get; set; }
        public int Wypos { get; set; }
        public int[] Shells { get; set; }
        public string ElectronConfiguration { get; set; }
        public string ElectronConfigurationSemantic { get; set; }
        public double? ElectronAffinity { get; set; }
        public double? ElectronegativityPauling { get; set; }
        public decimal[] IonizationEnergies { get; set; }
        public string CpkHex { get; set; }
        public ImageData Image { get; set; }
        public string Block { get; set; }
    }

    public class ImageData
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Attribution { get; set; }
    }

    public class ElementList
    {
        public Element[] elements { get; set; }
    }
}