using System.Collections.Generic;
using Backups.Entities;

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
    }
}