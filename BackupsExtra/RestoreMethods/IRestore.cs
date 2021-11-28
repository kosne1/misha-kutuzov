using Backups.Entities;

namespace BackupsExtra.RestoreMethods
{
    public interface IRestore
    {
        public void Restore(RestorePoint restorePoint, string path);
    }
}