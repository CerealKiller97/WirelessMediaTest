using System.Collections.Generic;

namespace Models
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}