using TaskManager.Models;

namespace TaskManager.Services.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<Ticket>> GetAllTasksAsync();
        Task<IEnumerable<Ticket>> GetTaskByEmpIdAsync(int empId);
        Task<IEnumerable<Ticket>> GetTaskByTeamIdAsync(int empId,int teamId);
        Task<List<Ticket>> GetTasksReportAsync(int empId,DateTime startDate, DateTime endDate);
        Task<Ticket> GetTaskByIdAsync(int id);
        Task<Document> AttachDocumentAsync(int taskId, IFormFile file);
        Task<Note> AddNoteAsync(int ticketId, string content);
        Task<Ticket> CompleteTicketAsync(int ticketId);
        Task AddTaskAsync(Ticket ticket);
        Task UpdateTaskAsync(Ticket ticket);
    }
}
