namespace Proyek2_Bengkel.Models
{
    public class ServiceDetail
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public int SparePartId { get; set; }
        public SparePart? SparePart { get; set; }
    }
}
