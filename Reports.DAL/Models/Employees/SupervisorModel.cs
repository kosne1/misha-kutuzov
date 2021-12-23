namespace Reports.DAL.Models.Employees;

public class SupervisorModel : EmployeeModel
{
    public List<EmployeeModel> Subordinates { get; set; } = new();

    public SupervisorModel()
    {
    }

    public SupervisorModel(Guid id, string name) : base(id, name)
    {
    }
}