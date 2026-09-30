namespace HWBMed.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeArea> EmployeeAreas { get; set; } = new List<EmployeeArea>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
