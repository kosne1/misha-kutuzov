using System.IO;

namespace Backups.Repositories
{
    public class ComputerRepository : IRepository
    {
        private readonly DirectoryInfo _repositoryDir;

        public ComputerRepository(string name)
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

        public ComputerRepository(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            _repositoryDir = directoryInfo;
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