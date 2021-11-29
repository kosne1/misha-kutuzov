using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;

namespace BackupsExtra.RestoreMethods
{
    public class ZipOriginalLocationRestore : IRestore
    {
        public void Restore(BackupJob backupJob, RestorePoint restorePoint, string path)
        {
            Extract(restorePoint);
            var directory = new DirectoryInfo("temp");
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                JobObject jobObject = backupJob.JobObjects.First(j => j.FilePath.Name == file.Name);
                var oldFile = new FileInfo(jobObject.FilePath.FullName);
                if (oldFile.Exists)
                {
                    file.Replace(file.FullName, oldFile.FullName);
                }
                else
                {
                    file.MoveTo(oldFile.FullName);
                }
            }
        }

        private void Extract(RestorePoint restorePoint)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                ZipFile.ExtractToDirectory(storage.FileInfo.FullName, "temp");
            }
        }
    }
}