using System.IO;
using System.Linq;
using Backups.Entities;
using BackupsExtra.RestoreManagers;

namespace BackupsExtra.RestoreMethods
{
    public class OriginalLocationRestore : IRestore
    {
        public void Restore(BackupJob backupJob, RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                JobObject jobObject = backupJob.JobObjects.First(j => j.FilePath.Name == storage.FileInfo.Name);

                if (File.Exists(jobObject.FilePath.FullName))
                    File.Delete(jobObject.FilePath.FullName);

                string restorePath = jobObject.FilePath.DirectoryName;
            }
        }
    }
}