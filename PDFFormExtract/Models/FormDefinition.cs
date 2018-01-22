using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PDFFormExtract.Models
{
    public class FormDefinition
    {
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "confidenceThreshold")]
        public double ConfidenceThreshold { get; set; }

        [JsonProperty(PropertyName = "fileName")]
        public String FileName { get; set; }

        [JsonProperty(PropertyName = "width")]
        public float Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float Height { get; set; }

        [JsonProperty(PropertyName = "padding")]
        public int Padding { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public List<Page> Pages { get; set; }
    }

    public class Page
    {
        [JsonProperty(PropertyName = "imageFileName")]
        public string ImageFileName { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<FieldDefinition> Fields { get; set; }

        [JsonProperty(PropertyName = "width")]
        public float Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float Height { get; set; }
    }

    public class FieldDefinition
    {
        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public String DisplayName { get; set; }

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "dimension")]
        public String Dimension { get; set; }

        [JsonProperty(PropertyName = "selected")]
        public bool Selected { get; set; }

        [JsonProperty(PropertyName = "validationRegex")]
        public string ValidationRegex { get; set; }
    }
}
