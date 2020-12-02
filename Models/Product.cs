namespace Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        
        public decimal Price { get; set; }
    }
}