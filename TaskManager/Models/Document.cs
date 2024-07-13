namespace TaskManager.Models
{
    public class Document
    {
        public int? Id { get; set; }
        public string FilePath { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
