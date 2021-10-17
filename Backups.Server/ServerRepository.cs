using System.IO;
using Backups.Repositories;

namespace Backups.Server
{
    public class ServerRepository : IRepository
    {
        private readonly DirectoryInfo _repositoryDir;

        public ServerRepository(string name)
        {
            var backupsRoot = new DirectoryInfo(@"D:\backups");
            if (!backupsRoot.Exists)
            {
                backupsRoot.Create();
            }

            var backupDir = new DirectoryInfo($@"D:\backups\{name}");
            if (!backupDir.Exists)
            {
                backupDir.Create();
            }

            _repositoryDir = backupDir;
        }

        public DirectoryInfo CreateRestorePointDirectory(int restorePointNumber)
        {
            var restorePointDir = new DirectoryInfo(_repositoryDir.FullName + $@"\{restorePointNumber.ToString()}");
            if (!restorePointDir.Exists)
            {
                restorePointDir.Create();
            }

            return restorePointDir;
        }

        public int GetAmountOfCreatedRestorePoints()
        {
            return _repositoryDir.GetDirectories().Length;
        }
    }
}