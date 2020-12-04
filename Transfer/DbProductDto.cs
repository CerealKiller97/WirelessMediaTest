using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Transfer
{
    public class DbProductDto : ProductDto
    {
        [JsonPropertyName("manufacturer_id")] public int ManufacturerId { get; set; }
        [JsonPropertyName("categories")] public List<int> CategoryIds { get; set; } = new List<int>();
    }
}