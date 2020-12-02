using System.Text.Json.Serialization;

namespace Models
{
    public class Product : BaseModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [JsonPropertyName("manufacturer_id")]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        [JsonPropertyName("vendor_id")]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}