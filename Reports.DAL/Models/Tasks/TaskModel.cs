namespace Reports.DAL.Models.Tasks;

public class TaskModel
{
    public Guid Id { get; }

    public string Description { get; set; }

    public TaskCondition Condition { get; set; } = TaskCondition.Open;

    public List<string> Comments { get; set; } = new List<string>();

    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; }
    public SortedSet<Guid> WasModifiedBy { get; set; } = new SortedSet<Guid>();
    public Guid EmployeeId { get; set; }

    public TaskModel()
    {
    }

    public TaskModel(Guid id, string description, Guid employeeId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id is invalid");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description), "Name is invalid");
        }

        if (employeeId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(employeeId), "Id is invalid");
        }

        Id = id;
        Description = description;
        EmployeeId = employeeId;
    }
}