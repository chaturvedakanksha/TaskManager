using TaskManager.Models;

namespace TaskManager.Repos.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetByEmpIdAsync(int empId);
        Task<IEnumerable<Ticket>> GetByTeamIdAsync(int teamId);
        Task<List<Ticket>> GetTasksByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<Ticket> GetByIdAsync(int id);
        Task AddTaskDocumentAsync(Document taskDocument);
        Task AddNoteAsync(Note note);
        Task UpdateTicketAsync(Ticket ticket);
        Task SaveChangesAsync();
        Task UpdateAsync(Ticket ticket);
        Task AddAsync(Ticket ticket);  
    }
}
