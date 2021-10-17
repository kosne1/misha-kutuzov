using System.Collections.Generic;
using System.IO;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;

        public BackupJob()
        {
            Backup = new Backup();
            _jobObjects = new List<JobObject>();
        }

        public int RestorePointsCounter { get; private set; }
        public Backup Backup { get; }
        public IStorageAlgorithm StorageAlgorithm { get; set; }
        public IRepository Repository { get; set; }

        public void AddFiles(params string[] filesPaths)
        {
            foreach (string filePath in filesPaths)
            {
                JobObject foundJobObject = _jobObjects.Find(j => j.FilePath == filePath);
                if (foundJobObject != null) continue;

                var jobObject = new JobObject(filePath);
                _jobObjects.Add(jobObject);
            }
        }

        public void CreateRestorePoint()
        {
            var restorePoint = new RestorePoint();
            RestorePointsCounter = Repository.GetAmountOfCreatedRestorePoints();
            DirectoryInfo restorePointDir = Repository.CreateRestorePointDirectory(++RestorePointsCounter);

            List<Storage> storages = StorageAlgorithm.Store(_jobObjects, restorePointDir);

            foreach (Storage storage in storages)
            {
                restorePoint.AddStorage(storage);
            }

            Backup.AddRestorePoint(restorePoint);
        }
    }
}