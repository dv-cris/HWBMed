namespace HWBMed.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal IVA { get; set; }
        public int IdArea { get; set; }
        public Area Area { get; set; }

    }
}
