using System.Collections.Generic;
using Backups.Archivers;
using Backups.Repositories;

namespace Backups.Entities
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;
        private readonly List<RestorePoint> _restorePoints;

        public BackupJob()
        {
            _jobObjects = new List<JobObject>();
            _restorePoints = new List<RestorePoint>();
        }

        public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
        public IRepository Repository { get; set; }
        public IArchiver Archiver { get; set; }

        public void AddJobObject(JobObject jobObject)
        {
            if (!_jobObjects.Contains(jobObject)) _jobObjects.Add(jobObject);
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (_jobObjects.Contains(jobObject)) _jobObjects.Remove(jobObject);
        }

        public void CreateRestorePoint()
        {
            var restorePoint = new RestorePoint();

            List<Storage> storages = Archiver.Archive(_jobObjects, Repository.DirectoryInfo);

            Repository.SaveStorages(storages, _restorePoints.Count);

            foreach (Storage storage in storages)
            {
                restorePoint.AddStorage(storage);
            }

            _restorePoints.Add(restorePoint);
        }
    }
}