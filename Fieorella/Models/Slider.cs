using Fieorella.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fieorella.Models
{
    public class Slider:BaseEntity
    {
        public string? ImageURL { get; set; }

        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
