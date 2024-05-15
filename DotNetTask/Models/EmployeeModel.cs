namespace DotNetTask.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Nationality { get; set; }
        public string? Residence { get; set; }
        public string EmployeeId { get; set; }
        public DateTime Birthday { get; set; }
        public string? Gender { get; set; }
    }
}
