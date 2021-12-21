using Reports.DAL.Models.TodoItems;

namespace Reports.Server.Services;

public interface ITaskService
{
    IReadOnlyCollection<TaskModel> GetAll();
    Task<TaskModel> Create(string name);

    TaskModel FindById(Guid id);

    void Delete(Guid id);

    TaskModel Update(TaskModel entity);
}