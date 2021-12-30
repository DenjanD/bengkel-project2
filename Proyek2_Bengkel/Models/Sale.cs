using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyek2_Bengkel.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int TellerId { get; set; }
        public Teller? Teller { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string? Description { get; set; }
        public int TotalCost { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
