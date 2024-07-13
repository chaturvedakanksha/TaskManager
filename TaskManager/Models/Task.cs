using System.Reflection.Metadata;

namespace TaskManager.Models
{
    public enum TicketStatus
    {
        Pending=1,
        InProgress=2,
        Completed=3
    }

    public class Ticket
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public TicketStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ICollection<Note>? Notes { get; set; }
        public ICollection<Document>? Documents { get; set; }
    }
}
