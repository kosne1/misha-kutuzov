using System.Collections.Generic;
using System.IO;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects;
        private readonly IRepository _repository;
        private IStorageAlgorithm _storageAlgorithm;

        public BackupJob(string name)
        {
            if (!IsNameValid(name)) throw new BackupsException("Can't create Backup Job with empty name");

            Name = name;
            _repository = new ComputerRepository(Name);
            _jobObjects = new List<JobObject>();
        }

        public int RestorePointsCounter { get; private set; } = 0;

        public string Name { get; }

        public void SetStorageAlgorithm(IStorageAlgorithm storageAlgorithm)
        {
            _storageAlgorithm = storageAlgorithm;
        }

        public void AddFiles(params string[] filesPaths)
        {
            foreach (string filePath in filesPaths)
            {
                JobObject foundJobObject = _jobObjects.Find(j => j.FilePath == filePath);
                if (foundJobObject != null) continue;

                string filename = Path.GetFileName(filePath);
                var jobObject = new JobObject(filePath, filename);
                _jobObjects.Add(jobObject);
            }
        }

        public void CreateRestorePoint()
        {
            var restorePoint = new RestorePoint();
            string restorePointDir = _repository.CreateRestorePointDirectory(++RestorePointsCounter);

            List<Storage> storages = _storageAlgorithm.Store(_jobObjects, restorePointDir);

            foreach (Storage storage in storages)
            {
                restorePoint.AddStorage(storage);
            }
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}