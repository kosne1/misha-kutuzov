using System;
using System.Collections.Generic;
using Backups.Archivers;
using Backups.Logger;
using Backups.Repositories;

namespace Backups.Entities
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;
        private readonly List<RestorePoint> _restorePoints;
        private readonly ILogger _logger;
        private IArchiver _archiver;

        public BackupJob(ILogger logger)
        {
            _jobObjects = new List<JobObject>();
            _restorePoints = new List<RestorePoint>();
            _logger = logger;
        }

        public List<RestorePoint> RestorePoints => _restorePoints;
        public IReadOnlyCollection<JobObject> JobObjects => _jobObjects;
        public IRepository Repository { get; private set; }

        public void AddJobObject(JobObject jobObject)
        {
            if (!_jobObjects.Contains(jobObject)) _jobObjects.Add(jobObject);
            _logger.LogInfo($"Added job object {jobObject.FilePath.FullName}");
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (_jobObjects.Contains(jobObject)) _jobObjects.Remove(jobObject);
            _logger.LogInfo($"Removed job object {jobObject.FilePath.FullName}");
        }

        public RestorePoint CreateRestorePoint(DateTime creationTime)
        {
            var restorePoint = new RestorePoint(creationTime);

            List<Storage> storages = _archiver.Archive(_jobObjects, Repository.DirectoryInfo);

            Repository.SaveStorages(storages, _restorePoints.Count);

            foreach (Storage storage in storages)
            {
                restorePoint.AddStorage(storage);
            }

            _restorePoints.Add(restorePoint);
            _logger.LogInfo($"Created Restore point");
            foreach (Storage storage in storages)
            {
                _logger.LogInfo($"Created storage at {storage.FileInfo.FullName}");
            }

            return restorePoint;
        }

        public void SetRepository(IRepository repository)
        {
            Repository = repository;
            _logger.LogInfo($"Repository was set to {repository.DirectoryInfo.FullName}");
        }

        public void SetArchiver(IArchiver archiver)
        {
            _archiver = archiver;
            _logger.LogInfo($"Archiver was set to {archiver}");
        }
    }
}