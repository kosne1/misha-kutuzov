using Reports.DAL.Models.Tasks;
using Reports.Server.Database;

namespace Reports.Server.Services;

public class TaskService : ITaskService
{
    private readonly ReportsDatabaseContext _context;

    public TaskService(ReportsDatabaseContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<TaskModel> GetAll()
    {
        return _context.Tasks.ToList();
    }

    public async Task<TaskModel> Create(string description, Guid employeeId)
    {
        var task = new TaskModel(Guid.NewGuid(), description, employeeId);
        task.LastModified = DateTime.Now;
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public TaskModel FindById(Guid id)
    {
        var taskFromDb = _context.Tasks.FirstOrDefault(i => i.Id == id);
        return taskFromDb ?? null;
    }

    public TaskModel FindByCreationTime(DateTime creationTime)
    {
        var taskFromDb = _context.Tasks.FirstOrDefault(i => i.CreationTime == creationTime);
        return taskFromDb ?? null;
    }

    public TaskModel FindByLastModifiedTime(DateTime modificationTime)
    {
        var taskFromDb = _context.Tasks.FirstOrDefault(i => i.LastModified == modificationTime);
        return taskFromDb ?? null;
    }

    public IReadOnlyCollection<TaskModel> GetEmployeeTasks(Guid employeeId)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == employeeId);
        return employeeFromDb.Tasks;
    }

    public IReadOnlyCollection<TaskModel> GetTasksModifiedByEmployee(Guid employeeId)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == employeeId);
        var tasks = GetAll();

        return tasks.Where(task => task.WasModifiedBy.Contains(employeeId)).ToList();
    }

    public void Delete(Guid id)
    {
        var taskFromDb = FindById(id);
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == taskFromDb.EmployeeId);
        _context.Tasks.Remove(taskFromDb);
        employeeFromDb.Tasks.Remove(taskFromDb);
        _context.SaveChangesAsync();
    }

    public TaskModel UpdateCondition(Guid id, TaskCondition newCondition)
    {
        var taskFromDb = FindById(id);
        taskFromDb.Condition = newCondition;
        taskFromDb.LastModified = DateTime.Now;
        taskFromDb.WasModifiedBy.Add(taskFromDb.EmployeeId);
        _context.SaveChangesAsync();
        return taskFromDb;
    }

    public TaskModel UpdateDescription(Guid id, string description)
    {
        var taskFromDb = FindById(id);
        taskFromDb.Description = description;
        taskFromDb.LastModified = DateTime.Now;
        taskFromDb.WasModifiedBy.Add(taskFromDb.EmployeeId);
        _context.SaveChangesAsync();
        return taskFromDb;
    }

    public TaskModel AddComment(Guid id, string comment)
    {
        var taskFromDb = FindById(id);
        taskFromDb.Comments.Add(comment);
        taskFromDb.LastModified = DateTime.Now;
        taskFromDb.WasModifiedBy.Add(taskFromDb.EmployeeId);
        _context.SaveChangesAsync();
        return taskFromDb;
    }

    public TaskModel SetEmployee(Guid taskId, Guid employeeId)
    {
        var taskFromDb = FindById(taskId);
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == employeeId);

        taskFromDb.EmployeeId = employeeFromDb.Id;
        taskFromDb.LastModified = DateTime.Now;
        employeeFromDb.Tasks.Add(taskFromDb);
        _context.SaveChangesAsync();
        return taskFromDb;
    }
}