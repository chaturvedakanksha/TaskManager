using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {

        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }
        
        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {

            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("GetAllTasksByEmployeeId/{empId}")]
        public async Task<IActionResult> GetAllTasksByEmployeeId(int empId)
        {

            var tasks = await _taskService.GetTaskByEmpIdAsync(empId);
            return Ok(tasks);
        }
        
        [HttpGet("GetAllTasksByTeamId/{teamId}")]
        public async Task<IActionResult> GetAllTasksByTeamId(int empId,int teamId)
        {

            var tasks = await _taskService.GetTaskByTeamIdAsync(empId,teamId);
            return Ok(tasks);
        }
        
        [HttpGet("GetTasksReport/{empId}")]
        public async Task<IActionResult> GetTasksReportAsync(int empId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date must be before end date.");
            }

            var tasks = await _taskService.GetTasksReportAsync(empId,startDate, endDate);
            return Ok(tasks);
        }

        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost("UploadDocument/{taskId}")]
        public async Task<IActionResult> UploadDocument(int taskId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var taskDocument = await _taskService.AttachDocumentAsync(taskId, file);
            return Ok(taskDocument);
        }

        [HttpPost("AddNote/{taskId}")]
        public async Task<IActionResult> AddNote(int taskId, [FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("Note content cannot be empty");
            }

            try
            {
                var note = await _taskService.AddNoteAsync(taskId, content);
                return Ok(note);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPost("CompleteTicketAsync/{ticketId}")]
        public async Task<IActionResult> CompleteTicketAsync(int ticketId)
        {
            try
            {
                var ticket = await _taskService.CompleteTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpPut("UpdateTask/{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }
            await _taskService.UpdateTaskAsync(ticket);
            return NoContent();
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] Ticket ticket)
        {
            await _taskService.AddTaskAsync(ticket);
            return CreatedAtAction(nameof(GetTaskById), new { id = ticket.Id }, ticket);
        }
    }
}
