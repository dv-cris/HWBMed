using Microsoft.AspNetCore.Identity;

namespace HWBMed.Models
{
    public class Employee : IdentityUser
    {
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdProfile { get; set; }
        public Profile Profile { get; set; }
    }
}
