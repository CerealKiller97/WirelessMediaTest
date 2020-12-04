using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Manufacturer : BaseEntity
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}