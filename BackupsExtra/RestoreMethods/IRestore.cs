using Backups.Entities;

namespace BackupsExtra.RestoreManagers
{
    public interface IRestore
    {
        public void Restore(BackupJob backupJob, RestorePoint restorePoint);
    }
}