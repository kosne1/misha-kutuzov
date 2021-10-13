using System.IO;
using Backups.Tools;

namespace Backups.Repositories
{
    public class ComputerRepository : IRepository
    {
        private readonly string _repositoryName;

        public ComputerRepository(string name)
        {
            CreateBackupsRootDirectory();

            _repositoryName = $@"D:\backups\{name}";
        }

        public string CreateRestorePointDirectory(int restorePointNumber)
        {
            string restorePointDir = _repositoryName + $@"\{restorePointNumber.ToString()}";
            CreateDirectory(restorePointDir);
            return restorePointDir;
        }

        private void CreateBackupsRootDirectory()
        {
            const string dirName = @"D:\backups";
            CreateDirectory(dirName);
        }

        private void CreateDirectory(string dirName)
        {
            var dirInfo = new DirectoryInfo(dirName);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
    }
}