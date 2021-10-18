using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Repositories
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public DirectoryInfo DirectoryInfo { get; }

        public void SaveStorages(List<Storage> storages, int restorePointNumber)
        {
            DirectoryInfo saveDir = CreateRestorePointDirectory(restorePointNumber);
            foreach (Storage storage in storages)
            {
                storage.FileInfo.MoveTo(Path.Join(saveDir.FullName, $@"\{storage.FileInfo.Name}"));
            }
        }

        private DirectoryInfo CreateRestorePointDirectory(int restorePointNumber)
        {
            var restorePointDir =
                new DirectoryInfo(Path.Combine(DirectoryInfo.FullName, restorePointNumber.ToString()));
            if (!restorePointDir.Exists)
            {
                restorePointDir.Create();
            }

            return restorePointDir;
        }
    }
}