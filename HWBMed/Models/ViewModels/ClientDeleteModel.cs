using System.ComponentModel.DataAnnotations;

namespace HWBMed.Models.ViewModels
{
    public class ClientDeleteModel
    {
        public int Id { get; set; }
        [Display(Name = "NIF")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string NIF { get; set; }
        [Display(Name = "Utente number")]
        public string? UtenteNumber { get; set; }
    }
}
