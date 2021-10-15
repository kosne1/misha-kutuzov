using System.IO;

namespace Backups.Repositories
{
    public class ComputerRepository : IRepository
    {
        private readonly string _repositoryName;

        public ComputerRepository(string name)
        {
            const string dirName = @"D:\backups";
            CreateDirectory(dirName);

            _repositoryName = $@"D:\backups\{name}";
            CreateDirectory(_repositoryName);
        }

        public string CreateRestorePointDirectory(int restorePointNumber)
        {
            string restorePointDir = _repositoryName + $@"\{restorePointNumber.ToString()}";
            CreateDirectory(restorePointDir);
            return restorePointDir;
        }

        public int GetAmountOfCreatedRestorePoints()
        {
            var dirInfo = new DirectoryInfo(_repositoryName);
            return dirInfo.GetDirectories().Length;
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