using Reports.DAL.Models.Tasks;

namespace Reports.DAL.Models;

public class Weekly
{
    public Guid Id { get; set; }
    public bool Finished { get; set; } = false;
    public string Description { get; set; }
    public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();

    public Weekly()
    {
    }

    public Weekly(Guid id, string description)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id is invalid");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description), "Name is invalid");
        }

        Id = id;
        Description = description;
    }
}