namespace HWBMed.Models.ViewModel
{
    public class EmployeeCreateViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NIF { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProfileID {  get; set; }
        public int UserID {  get; set; }
    }
}
