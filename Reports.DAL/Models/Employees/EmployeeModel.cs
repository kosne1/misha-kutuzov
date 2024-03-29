﻿using Reports.DAL.Models.Tasks;

namespace Reports.DAL.Models.Employees;

public class EmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TaskModel> Tasks { get; set; } = new();
    public Weekly Weekly { get; set; }

    public EmployeeModel()
    {
    }

    public EmployeeModel(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id), "Id is invalid");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Name is invalid");
        }

        Id = id;
        Name = name;
    }
}