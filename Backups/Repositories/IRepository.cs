namespace Backups.Repositories
{
    public interface IRepository
    {
        public string CreateRestorePointDirectory(int restorePointNumber);
        public int GetAmountOfCreatedRestorePoints();
    }
}