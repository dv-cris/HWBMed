using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HWBMed.Models
{
    public class Profile :IdentityRole
    {

        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public override string Name { get; set; }
        [Display(Name = "Discount")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public decimal Discount { get; set; }
        public Employee? Employee { get; set; }
    }
}
