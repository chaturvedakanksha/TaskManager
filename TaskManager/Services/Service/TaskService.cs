using TaskManager.Models;
using TaskManager.Repos.Interface;
using TaskManager.Services.Interface;

namespace TaskManager.Services.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IWebHostEnvironment _environment;

        public TaskService(ITaskRepository taskRepository, IWebHostEnvironment environment)
        {
            _taskRepository = taskRepository;
            _environment = environment;
        }

        public async Task<IEnumerable<Ticket>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Ticket>> GetTaskByEmpIdAsync(int empId)
        {
            return await _taskRepository.GetByEmpIdAsync(empId);
        }
        public async Task<IEnumerable<Ticket>> GetTaskByTeamIdAsync(int empId, int teamId)
        {
            return await _taskRepository.GetByTeamIdAsync(empId,teamId);
        }
        public async Task<List<Ticket>> GetTasksReportAsync(int empId, DateTime startDate, DateTime endDate)
        {
            return await _taskRepository.GetTasksByDateRangeAsync(empId,startDate, endDate);
        }

        public async Task<Ticket> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<Document> AttachDocumentAsync(int taskId, IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            
            if(!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var taskDocument = new Document
            {
                FilePath = filePath,
                TicketId = taskId
            };

            await _taskRepository.AddTaskDocumentAsync(taskDocument);
            await _taskRepository.SaveChangesAsync();

            return taskDocument;
        }

        public async Task<Note> AddNoteAsync(int ticketId, string content)
        {
            var ticket = await _taskRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new ArgumentException("Invalid ticket ID");
            }

            var note = new Note
            {
                Content = content,
                TicketId = ticketId
            };

            await _taskRepository.AddNoteAsync(note);
            await _taskRepository.SaveChangesAsync();

            return note;
        }
        public async Task<Ticket> CompleteTicketAsync(int ticketId)
        {
            var ticket = await _taskRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                throw new ArgumentException("Invalid ticket ID");
            }

            ticket.Status = TicketStatus.Completed;
            ticket.CompletionDate = DateTime.UtcNow;

            await _taskRepository.UpdateTicketAsync(ticket);

            return ticket;
        }

        public async Task AddTaskAsync(Ticket ticket)
        {
            await _taskRepository.AddAsync(ticket);
        }

        public async Task UpdateTaskAsync(Ticket ticket)
        {
            await _taskRepository.UpdateAsync(ticket);
        }
    }
}
