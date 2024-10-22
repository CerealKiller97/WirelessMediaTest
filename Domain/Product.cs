using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Product : BaseEntity
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("categories")] public virtual List<Category> Categories { get; set; }
        [JsonPropertyName("manufacturer_id")] public int ManufacturerId { get; set; }
        [JsonPropertyName("manufacturer")] public virtual Manufacturer Manufacturer { get; set; }
        [JsonPropertyName("vendor_id")] public int VendorId { get; set; }
        [JsonPropertyName("vendor")] public virtual Vendor Vendor { get; set; }
        [JsonPropertyName("price")] public decimal Price { get; set; }
    }
}