namespace Reports.DAL.Models.Employees;

public class TeamLeadModel : SupervisorModel
{
    public TeamLeadModel()
    {
    }

    public TeamLeadModel(Guid id, string name) : base(id, name)
    {
    }
}