using System.ComponentModel.DataAnnotations;

namespace Proyek2_Bengkel.Models
{
    public class Teller
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
