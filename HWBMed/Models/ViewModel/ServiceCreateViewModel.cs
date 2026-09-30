namespace HWBMed.Models.ViewModel
{
    public class ServiceCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal IVA { get; set; }
        public int IdArea { get; set; }
        public string? Area { get; set; }
    }
}
