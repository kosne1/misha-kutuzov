using Reports.DAL.Models.TodoItems;
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

    public async Task<TaskModel> Create(string name)
    {
        var task = new TaskModel(Guid.NewGuid(), name);
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public TaskModel FindById(Guid id)
    {
        var taskFromDb = _context.Tasks.FirstOrDefault(i => i.Id == id);
        return taskFromDb ?? null;
    }

    public void Delete(Guid id)
    {
        var taskFromDb = FindById(id);
        _context.Tasks.Remove(taskFromDb);
        _context.SaveChangesAsync();
    }

    public TaskModel Update(TaskModel entity)
    {
        var taskFromDb = FindById(entity.Id);
        taskFromDb.Name = entity.Name;
        taskFromDb.Comments = entity.Comments;
        taskFromDb.Condition = entity.Condition;
        _context.SaveChangesAsync();
        return taskFromDb;
    }
}