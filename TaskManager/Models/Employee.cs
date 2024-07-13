namespace TaskManager.Models
{
    public enum Role 
    {
        Employee = 1,
        Manager = 2, 
        Admin = 3
    }
    public class Employee
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
