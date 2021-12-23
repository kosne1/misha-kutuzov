using Reports.DAL.Models.Tasks;

namespace Reports.Server.Services;

public interface ITaskService
{
    IReadOnlyCollection<TaskModel> GetAll();
    Task<TaskModel> Create(string description, Guid employeeId);
    TaskModel FindById(Guid id);
    TaskModel FindByCreationTime(DateTime creationTime);
    TaskModel FindByLastModifiedTime(DateTime modificationTime);
    IReadOnlyCollection<TaskModel> GetEmployeeTasks(Guid employeeId);
    IReadOnlyCollection<TaskModel> GetTasksModifiedByEmployee(Guid employeeId);
    void Delete(Guid id);
    TaskModel UpdateCondition(Guid id, TaskCondition newCondition);
    TaskModel UpdateDescription(Guid id, string description);
    TaskModel AddComment(Guid id, string comment);
    TaskModel SetEmployee(Guid taskId, Guid employeeId);
}