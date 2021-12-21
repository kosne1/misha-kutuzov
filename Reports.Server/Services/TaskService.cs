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

    public void Delete(Guid id)
    {
        var taskFromDb = FindById(id);
        _context.Tasks.Remove(taskFromDb);
        _context.SaveChangesAsync();
    }

    public TaskModel UpdateCondition(Guid id, TaskCondition newCondition)
    {
        var taskFromDb = FindById(id);
        taskFromDb.Condition = newCondition;
        taskFromDb.LastModified = DateTime.Now;
        _context.SaveChangesAsync();
        return taskFromDb;
    }

    public TaskModel AddComment(Guid id, string comment)
    {
        var taskFromDb = FindById(id);
        taskFromDb.Comments.Add(comment);
        taskFromDb.LastModified = DateTime.Now;
        _context.SaveChangesAsync();
        return taskFromDb;
    }
}