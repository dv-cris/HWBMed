using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HWBMed.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; }
        public string Local { get; set; }
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        public string NIF { get; set; }
        public string? UtenteNumber { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public bool ContactPhone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public bool ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
