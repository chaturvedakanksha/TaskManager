using Microsoft.EntityFrameworkCore;
using TaskManager.DbContext;
using TaskManager.Models;
using TaskManager.Repos.Interface;

namespace TaskManager.Repos.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;

        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {

            return await _context.Tickets.Include(t => t.Notes).Include(t => t.Documents).ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetByEmpIdAsync(int empId)
        {

            return await _context.Tickets.Where(t => t.EmployeeId == empId && t.Employee.Role == Role.Employee).ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetByTeamIdAsync(int teamId)
        {
            return await _context.Tickets
            .Include(t => t.Notes)
            .Include(t => t.Documents)
            .Include(t => t.Employee)
            .Where(t => t.Employee.TeamId == teamId)
            .ToListAsync();
        }
        public async Task<List<Ticket>> GetTasksByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Tickets
                .Include(t => t.Notes)
                .Include(t => t.Documents)
                .Include(t => t.Employee)
                .Where(t => t.StartDate >= startDate && t.StartDate <= endDate)
                .ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _context.Tickets.Include(t => t.Notes).Include(t => t.Documents)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskDocumentAsync(Document taskDocument)
        {
            await _context.Documents.AddAsync(taskDocument);
        }
        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
        }
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
