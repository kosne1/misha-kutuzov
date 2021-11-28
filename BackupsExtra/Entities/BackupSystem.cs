using System.Collections.Generic;
using Backups.Entities;
using Backups.Logger;

namespace BackupsExtra.Entities
{
    public class BackupSystem
    {
        private List<BackupJob> _backupJobs;

        public BackupSystem()
        {
            _backupJobs = new List<BackupJob>();
        }

        public IReadOnlyCollection<BackupJob> BackupJobs => _backupJobs;

        public BackupJob AddBackupJob(ILogger logger)
        {
            var backupJob = new BackupJob(logger);
            _backupJobs.Add(backupJob);
            return backupJob;
        }
    }
}