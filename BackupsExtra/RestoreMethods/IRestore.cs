using Backups.Entities;

namespace BackupsExtra.RestoreMethods
{
    public interface IRestore
    {
        public void Restore(BackupJob backupJob, RestorePoint restorePoint, string path);
    }
}