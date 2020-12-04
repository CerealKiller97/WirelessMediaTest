using System.Collections.Generic;
using System.Text.Json.Serialization;
using Models;

namespace Transfer
{
    public class JsonProductDto : ProductDto
    {
        [JsonPropertyName("categories")] public virtual List<Category> Categories { get; set; }
        [JsonPropertyName("manufacturer")] public virtual Manufacturer Manufacturer { get; set; }
        [JsonPropertyName("vendor")] public virtual Vendor Vendor { get; set; }
        [JsonPropertyName("price")] public decimal Price { get; set; }
    }
}