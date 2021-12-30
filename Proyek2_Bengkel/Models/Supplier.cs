using System.ComponentModel.DataAnnotations;

namespace Proyek2_Bengkel.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
