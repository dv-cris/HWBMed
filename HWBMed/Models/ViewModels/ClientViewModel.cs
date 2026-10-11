using System.ComponentModel.DataAnnotations;

namespace HWBMed.Models.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Name { get; set; }
        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public DateOnly Birthday { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Address { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string City { get; set; }
        [Display(Name = "Postal code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string PostalCode { get; set; }
        [Display(Name = "NIF")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string NIF { get; set; }
        [Display(Name = "Utente number")]
        public string? UtenteNumber { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Email { get; set; }
        [Display(Name = "Contact by e-mail")]
        public bool ContactEmail { get; set; }
        [Display(Name = "Phone number")]
        [Phone]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Contact by phone")]
        public bool ContactPhone { get; set; }
    }
}
