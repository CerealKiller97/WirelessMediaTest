using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Transfer
{
    public class ProductDto
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("price")] public decimal Price { get; set; }
    }
}