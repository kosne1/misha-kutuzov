﻿using Reports.DAL.Models;

namespace Reports.Server.Services;

public interface IWeeklyService
{
    Task<Weekly> Create(string description);
    IReadOnlyCollection<Weekly> GetAll();
    IReadOnlyCollection<Weekly> GetSubordinatesWeeklies(Guid supervisorId);
    Weekly AddTask(Guid weeklyId, Guid taskId);
    Weekly UpdateDescription(Guid id, string description);
    Weekly UpdateCondition(Guid id, bool condition);
}