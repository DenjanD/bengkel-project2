namespace Proyek2_Bengkel.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int ServiceCategoryId { get; set; }
        public ServiceCategory? ServiceCategory { get; set; }
        public string? Complaint { get; set; }
        public string? VehicleName { get; set; }
        public string? VehiclePlate { get; set; }
        public DateTime? ServiceDate { get; set; }
        public int ServiceCost { get; set; }
    }
}
