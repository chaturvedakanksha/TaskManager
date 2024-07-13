using TaskManager.Models;

namespace TaskManager.Repos.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Role> GetEmployeeRoleAsync(int empId);
        Task<IEnumerable<Ticket>> GetByEmpIdAsync(int empId);
        Task<IEnumerable<Ticket>> GetByTeamIdAsync(int empId,int teamId);
        Task<List<Ticket>> GetTasksByDateRangeAsync(int empId,DateTime startDate, DateTime endDate);
        Task<Ticket> GetByIdAsync(int id);
        Task AddTaskDocumentAsync(Document taskDocument);
        Task AddNoteAsync(Note note);
        Task UpdateTicketAsync(Ticket ticket);
        Task SaveChangesAsync();
        Task UpdateAsync(Ticket ticket);
        Task AddAsync(Ticket ticket);  
    }
}
