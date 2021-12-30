namespace Proyek2_Bengkel.Models
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public Sale? Sale { get; set; }
        public int SparePartId { get; set; }
        public SparePart? SparePart { get; set; }
        public int Qty { get; set; }
    }
}
