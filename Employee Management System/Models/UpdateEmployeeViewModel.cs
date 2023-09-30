namespace Employee_Management_System.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
