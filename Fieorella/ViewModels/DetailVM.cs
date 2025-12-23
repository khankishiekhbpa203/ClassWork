using Fieorella.Models;

namespace Fieorella.ViewModels
{
    public class DetailVM
    {
        public Product Product { get; set; }
        public List<Product>RelatedProducts { get; set; }

    }
}
