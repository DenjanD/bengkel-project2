using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyek2_Bengkel.Models
{
    public class SparePart
    {
        public int Id { get; set; }
        public int PartCategoryId { get; set; }
        public PartCategory? PartCategory { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
    }
}
