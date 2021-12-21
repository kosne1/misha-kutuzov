using Reports.DAL.Models.Tasks;

namespace Reports.Server.Services;

public interface ITaskService
{
    IReadOnlyCollection<TaskModel> GetAll();
    Task<TaskModel> Create(string name);

    TaskModel FindById(Guid id);
    TaskModel FindByCreationTime(DateTime creationTime);
    TaskModel FindByLastModifiedTime(DateTime modificationTime);
    void Delete(Guid id);

    TaskModel UpdateCondition(Guid id, TaskCondition newCondition);
    TaskModel AddComment(Guid id, string comment);
}