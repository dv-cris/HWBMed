namespace HWBMed.Models.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Birthday { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NIF { get; set; }
        public string? UtenteNumber { get; set; }
        public string Email { get; set; }
        public bool ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public bool ContactPhone { get; set; }
    }
}
