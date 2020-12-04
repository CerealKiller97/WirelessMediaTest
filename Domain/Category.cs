using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Category : BaseEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}