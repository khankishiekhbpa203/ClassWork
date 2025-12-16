using Fieorella.Models.Base;

namespace Fieorella.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<ProductCategory> productCategories { get; set; }
    }
}
