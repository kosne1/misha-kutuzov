using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Configurations;

namespace BackupsExtra.Entities
{
    public class BackupSystem
    {
        private readonly List<BackupJob> _backupJobs;

        public BackupSystem()
        {
            _backupJobs = new List<BackupJob>();
        }

        public IReadOnlyCollection<BackupJob> BackupJobs => _backupJobs;

        public IConfigurator Configurator { get; private set; }

        public BackupJob AddBackupJob()
        {
            var backupJob = new BackupJob();
            _backupJobs.Add(backupJob);
            return backupJob;
        }

        public void SetConfigurator(IConfigurator configurator)
        {
            Configurator = configurator;
        }

        public void SaveConfigurations()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                Configurator.SaveConfiguration(backupJob);
            }
        }

        public void LoadConfigurations(List<string> backupPaths)
        {
            foreach (BackupJob backupJob in backupPaths.Select(backupJobPath =>
                Configurator.LoadConfiguration(backupJobPath)))
            {
                _backupJobs.Add(backupJob);
            }
        }
    }
}