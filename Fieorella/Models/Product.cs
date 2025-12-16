using Fieorella.Models.Base;

namespace Fieorella.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
     
        public List<ProductCategory>productCategories { get; set; }

    }
}
