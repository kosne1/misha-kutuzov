﻿namespace Reports.DAL.Models.Tasks;

public class TaskModel
{
    public Guid Id { get; }

    public string Name { get; set; }

    public TaskCondition Condition { get; set; } = TaskCondition.Open;

    public List<string> Comments { get; set; } = new List<string>();

    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime LastModified { get; set; }

    public TaskModel()
    {
    }

    public TaskModel(Guid id, string name)
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