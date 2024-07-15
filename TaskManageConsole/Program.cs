using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManager.DbContext;
using TaskManager.Models;

DbContextOptions<TaskManagementDbContext> options = new DbContextOptionsBuilder<TaskManagementDbContext>()
  .UseSqlServer("Server=DESKTOP-LA9385J;Database=TaskManager;Integrated Security=True;")
  .Options;


using (var context = new TaskManagementDbContext(options))
{
    //creates db if not exists
    context.Database.EnsureCreated();
    var task = new Ticket
    {
        Title = "Task 1",
        Description = "Task 1 Description",
        EmployeeId = 1,
        Status = TicketStatus.Pending,
        StartDate = DateTime.Now
    };
    context.Tickets.Add(task);
    context.SaveChanges();
    Console.WriteLine("Task added successfully");
    Console.ReadLine();
}