using System.ComponentModel.DataAnnotations;

namespace HWBMed.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Name { get; set; }
        [Display(Name = "Discount")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public decimal Discount { get; set; }
    }
}
