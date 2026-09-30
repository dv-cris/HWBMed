using System.ComponentModel.DataAnnotations.Schema;

namespace HWBMed.Models
{
    public class EmployeeArea
    {
        public int Id { get; set; }
        public string IdEmployee { get; set; }
        public Employee Employee { get; set; }
        public int IdArea { get; set; }
        public Area Area { get; set; }
    }
}
