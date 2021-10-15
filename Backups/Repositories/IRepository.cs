using System.IO;

namespace Backups.Repositories
{
    public interface IRepository
    {
        public DirectoryInfo CreateRestorePointDirectory(int restorePointNumber);
        public int GetAmountOfCreatedRestorePoints();
    }
}