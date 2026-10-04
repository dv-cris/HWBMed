using Microsoft.AspNetCore.Identity;

namespace HWBMed.Models
{
    public class Employee : IdentityUser
    {
        public int IdUser { get; set; }
        public User User { get; set; }
        public string IdProfile { get; set; }
        public Profile Profile { get; set; }
        public ICollection<EmployeeArea> EmployeeAreas { get; set; } = new List<EmployeeArea>();
    }
}
